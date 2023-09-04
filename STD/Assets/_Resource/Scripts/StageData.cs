using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary> 스테이지 데이터 클래스 </summary>
[System.Serializable]
public class Stage
{
    public int stageIndex;

    // 몬스터 Hp
    public float stageMonsterHp;
    public float stageMonsterSpeed;
    public int stageMonsterCount;
    // Tpye 1 몬스터가 스폰될 숫자.
    public int stageMonsterCountTypeI;
    // Tpye 2 몬스터가 스폰될 숫자.
    public int stageMonsterCountTypeII;
    // Tpye 3 몬스터가 스폰될 숫자.
    public int stageMonsterCountTypeIII;
    // 어느 몬스터를 스폰할지, 스폰될 몬스터 index를 저장함.
    public int[] stageSpawnMonster = new int[5];
    /*
    몬스터 속성시스템. 
    스테이지 별로 가중치 1 2 3 에 따라서
    몬스터마다 최소 0 개 최대 3개의 속성을 부여받음.
    */
    public int stageWeightI;
    public int stageWeightII;
    public int stageWeightIII;
}
public class StageData : MonoBehaviour
{
    public class StageDataInfo
    {
        public Stage[] stages;
    }

    public StageDataInfo st;

    void Start()
    {
        TextAsset loadedJson = Resources.Load<TextAsset>("StageData");
        st = JsonUtility.FromJson<StageDataInfo>(loadedJson.ToString());


        Debug.Log(st.stages.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary> 스테이지에 대한 index값을 넣으면 해당 스테이지의 정보를 보내주는 함수. </summary>
    /// <param name="index"> 스테이지 index값 입력. </param>
    public (float, float, int, int, int, int, int[], int, int, int) GetData(int index)
    {
        return (st.stages[index].stageMonsterHp,
        st.stages[index].stageMonsterSpeed,
        st.stages[index].stageMonsterCount,
        st.stages[index].stageMonsterCountTypeI,
        st.stages[index].stageMonsterCountTypeII,
        st.stages[index].stageMonsterCountTypeIII,
        st.stages[index].stageSpawnMonster,
        st.stages[index].stageWeightI,
        st.stages[index].stageWeightII,
        st.stages[index].stageWeightIII);
    }

}
