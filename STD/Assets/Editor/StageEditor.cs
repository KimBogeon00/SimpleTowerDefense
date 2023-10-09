using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.IO;

[System.Serializable]
public class EditorStageData // JSON에 저장할 데이터 형식.
{
    public int sdStageIndex;
    public int sdBossCheck;
    public float sdMonsterHp;
    public float sdMonsterSpeed;
    public int sdMonsterCount;
    public int sdSpawnmonster;
    public int sdageWeightI;
    public int sdageWeightII;
    public int sdageWeightIII;
}

public class StageEditor : EditorWindow
{

    int seGetStageIndex;
    int seStageIndex;
    int seBossCheck;
    float seMonsterHp;
    float seMonsterSpeed;
    int seMonsterCount;
    int seSpawnmonster;
    int seageWeightI;
    int seageWeightII;
    int seageWeightIII;
    // JsonUtility 는 객체 안에 다른 객체를 가진 배열처럼 2단 이상 구성되면 클래스로 가져오지 못하기 때문에.
    // 위 데이터 형식을 List로 만들어 주는 class를 하나 더 생성한다.
    public class EditorStageDataList
    {
        public List<EditorStageData> stages = new List<EditorStageData>();
    }
    public EditorStageData editData = new EditorStageData();
    public EditorStageDataList editDataList = new EditorStageDataList();


    // 저장경로, pc에서 수정한 데이터가 타겟 플랫폼인 모바일에 바로 적용되게 하기 위해서.
    // Resources 폴더로 저장되게 해준다.
    string path = Path.Combine(Application.dataPath + "/Resources/", "StageData.json");

    // Menu에 추가해준다 위치는 Tools 아래 StageEditor 라는 이름으로.
    [MenuItem("Tools/StageEditor")]
    public static void ShowMyEditor() // 윈도우 생성
    {
        // This method is called when the user selects the menu item in the Editor
        EditorWindow wnd = GetWindow<StageEditor>();
        wnd.titleContent = new GUIContent("StageEditor");
    }


    void OnGUI() // 윈도우 UI를 그린다.
    {
        // Stage Data Set이라는 라벨 아래로 각각의 스테이지 데이터를 받을 수 있게 만들어 줬다.
        GUILayout.Label("Stage Data Set", EditorStyles.boldLabel);
        GUILayout.Space(10f);
        seStageIndex = EditorGUILayout.IntField("seStageIndex", seStageIndex);
        GUILayout.Space(5f);
        seBossCheck = EditorGUILayout.IntField("seBossCheck", seBossCheck);
        GUILayout.Space(5f);
        seMonsterHp = EditorGUILayout.FloatField("seMonsterHp", seMonsterHp);
        GUILayout.Space(5f);
        seMonsterSpeed = EditorGUILayout.FloatField("seMonsterSpeed", seMonsterSpeed);
        GUILayout.Space(5f);
        seMonsterCount = EditorGUILayout.IntField("seMonsterCount", seMonsterCount);
        GUILayout.Space(5f);
        seSpawnmonster = EditorGUILayout.IntField("seSpawnMonster", seSpawnmonster);
        GUILayout.Space(5f);
        seageWeightI = EditorGUILayout.IntField("seMonsterWeightI", seageWeightI);
        GUILayout.Space(5f);
        seageWeightII = EditorGUILayout.IntField("seMonsterWeightII", seageWeightII);
        GUILayout.Space(5f);
        seageWeightIII = EditorGUILayout.IntField("seMonsterWeightIII", seageWeightIII);
        GUILayout.Space(10f);

        // 버튼을 만들고 if문을 사용해서 버튼을 누를경우 해당 코드가 실행된다.
        if (GUILayout.Button("Make Stage"))
        {
            if (File.Exists(path))
            {
                bool containCheck = false; // INDEX 값이 있는지 체크 하기위한 변수.
                // 기존 파일 불러오기.
                TextAsset loadData = Resources.Load<TextAsset>("StageData");
                // // 기존파일 sd 변수에 저장.
                editDataList = JsonUtility.FromJson<EditorStageDataList>(loadData.ToString());

                // 입력된 인덱스 값이 있는지 체크
                // - 스테이지 데이터가 많지않기 때문에 for문을 사용하여 검사하였다.
                for (int i = 0; i < editDataList.stages.Count; i++)
                {
                    if (editDataList.stages[i].sdStageIndex == seStageIndex)
                    {
                        DataChange(i); // 인덱스 값이 있다면 바뀌는 데이터 저장.
                        containCheck = true;
                        break;
                    }
                }
                if (!containCheck) // 인덱스 값이 없다면 데이터 추가.
                {
                    DataAdd();
                }
                // Stage의 index 기준으로 List를 정렬한다.
                editDataList.stages.Sort((p1, p2) => p1.sdStageIndex.CompareTo(p2.sdStageIndex));
                string data = JsonUtility.ToJson(editDataList, true); // 수정 or 추가한 데이터를 str로 바꿈
                File.WriteAllText(path, data); // str 데이터를 json형식으로 저장
                // 데이터 폴더를 리프래쉬 해주지 않으면 입력된 데이터가 저장이 되지않는다.
                AssetDatabase.Refresh();
            }
            else // 초기 데이터가 없는 경우.
            {
                DataAdd();
                string data = JsonUtility.ToJson(editDataList, true);
                File.WriteAllText(path, data);
            }
        }


        if (File.Exists(path))
        {
            GUILayout.Space(30f);
            GUILayout.Label("Stage Data Get", EditorStyles.boldLabel);
            seGetStageIndex = EditorGUILayout.IntField("seGetStageIndex", seGetStageIndex);

            // 기존 파일 불러오기.
            TextAsset loadData = Resources.Load<TextAsset>("StageData");
            // // 기존파일 sd 변수에 저장.
            editDataList = JsonUtility.FromJson<EditorStageDataList>(loadData.ToString());
            EditorGUILayout.HelpBox("불러온 스테이지 Index : " + editDataList.stages[seGetStageIndex].sdStageIndex +
        "\n 보스 스테이지 확인 ( 0 or 1 ) : " + editDataList.stages[seGetStageIndex].sdBossCheck +
        "\n 몬스터 체력 : " + editDataList.stages[seGetStageIndex].sdMonsterHp +
        "\n 몬스터 속도 : " + editDataList.stages[seGetStageIndex].sdMonsterSpeed +
        "\n 예상 몬스터 스폰 숫자 : " + editDataList.stages[seGetStageIndex].sdMonsterCount, MessageType.Info);
        }
    }
    /// <summary> 기존 데이터가 없는경우 데이터를 추가한다. </summary>
    void DataAdd()
    {
        editData.sdStageIndex = seStageIndex;
        editData.sdBossCheck = seBossCheck;
        editData.sdMonsterHp = seMonsterHp;
        editData.sdMonsterSpeed = seMonsterSpeed;
        editData.sdMonsterCount = seMonsterCount;
        editData.sdSpawnmonster = seSpawnmonster;
        editDataList.stages.Add(editData);

        Debug.Log("Index : " + editData.sdStageIndex + " 스테이지 데이터가 추가 되었습니다. ");
    }
    /// <summary> 기존 데이터가 있는경우 데이터를 변경한다. </summary>
    /// <param name="idx"> 변경할 index 주소를 입력. </param>
    void DataChange(int idx)
    {
        editDataList.stages[idx].sdBossCheck = seBossCheck;
        editDataList.stages[idx].sdMonsterHp = seMonsterHp;
        editDataList.stages[idx].sdMonsterSpeed = seMonsterSpeed;
        editDataList.stages[idx].sdMonsterCount = seMonsterCount;
        editDataList.stages[idx].sdSpawnmonster = seSpawnmonster;

        Debug.Log("Index : " + editDataList.stages[idx].sdStageIndex + " 스테이지 데이터가 수정 되었습니다. ");
    }
}
