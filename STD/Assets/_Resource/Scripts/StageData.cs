using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary> 스테이지 데이터 클래스 </summary>
[System.Serializable]
public class Stage
{
    public int sdStageIndex;
    public int sdBossCheck;
    // 몬스터 Hp
    public float sdMonsterHp;
    public float sdMonsterSpeed;
    public int sdMonsterCount;
    // 어느 몬스터를 스폰할지, 스폰될 몬스터 index를 저장함.
    public int sdSpawnmonster;
    /*
    몬스터 속성시스템. 
    스테이지 별로 가중치 1 2 3 에 따라서
    몬스터마다 최소 0 개 최대 3개의 속성을 부여받음.
    */
    public int sdageWeightI;
    public int sdageWeightII;
    public int sdageWeightIII;
}
public class StageData : MonoBehaviour
{
    public class StageDataInfo
    {
        public List<Stage> stages = new List<Stage>();
    }

    public StageDataInfo st = new StageDataInfo();

    void Start()
    {
        // string loadData = File.ReadAllText(Application.persistentDataPath + "/stagedata");
        //기존파일 sd 변수에 저장.
        // st = JsonUtility.FromJson<StageDataInfo>(loadData);
        TextAsset loadData = Resources.Load<TextAsset>("StageData");
        st = JsonUtility.FromJson<StageDataInfo>(loadData.ToString());


        Debug.Log(loadData);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary> 스테이지에 대한 index값을 넣으면 해당 스테이지의 정보를 보내주는 함수. </summary>
    /// <param name="index"> 스테이지 index값 입력. </param>
    public (int, float, float, int, int, int, int, int) GetData(int index)
    {
        Debug.Log(st.stages.Count);
        return (
        st.stages[index].sdBossCheck,
        st.stages[index].sdMonsterHp,
        st.stages[index].sdMonsterSpeed,
        st.stages[index].sdMonsterCount,
        st.stages[index].sdSpawnmonster,
        st.stages[index].sdageWeightI,
        st.stages[index].sdageWeightII,
        st.stages[index].sdageWeightIII);
    }

}
