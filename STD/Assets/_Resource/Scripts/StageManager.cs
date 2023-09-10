using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager smInstance;
    [Header(" [ Int ]")]
    public int smCurStageIndex;

    public int smMonsterCount;
    public int smMonsterCountTypeI;
    public int smMonsterCountTypeII;
    public int smMonsterCountTypeIII;
    public int[] smSpawnMonster = new int[5];
    public int smWeightI;
    public int smWeightII;
    public int smWeightIII;

    public int smMonsterKillCount;


    [Space(20f)]
    [Header(" [ Float ]")]
    public float smMonsterHp;
    public float smMonsterSpeed;

    public bool smStageCheck;
    [Space(20f)]
    [Header(" [ GameObject ]")]
    public GameObject smStageData;
    public GameObject[] smMonsterList = new GameObject[10];
    public GameObject smSpawnPoint;

    List<int> smIdentityIndex = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        if (smInstance == null)
        {
            smInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        smCurStageIndex = 1;
        smStageData = GameObject.FindWithTag("StageData");
        Invoke("StageStart", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (smStageCheck)
        {
            if (smMonsterCount == smMonsterKillCount)
            {
                smCurStageIndex += 1;
                smIdentityIndex.Clear();
                StageStart();
            }
        }

    }
    public void StageStart()
    {
        smMonsterKillCount = 0;
        StageDataLoad();
        smStageCheck = false;
        StartCoroutine(MonsterSpawn(smMonsterCountTypeI, smMonsterCountTypeII, smMonsterCountTypeIII, 0.8f));
    }

    public void StageDataLoad()
    {
        (smMonsterHp, smMonsterSpeed, smMonsterCount, smMonsterCountTypeI, smMonsterCountTypeII,
        smMonsterCountTypeIII, smSpawnMonster, smWeightI, smWeightII, smWeightIII) = smStageData.GetComponent<StageData>().GetData(smCurStageIndex);

        if (smWeightI >= Random.Range(0, 1000))
        {
            int rand = Random.Range(0, 12);
            while (true)
            {
                if (smIdentityIndex.Contains(rand))
                {
                    rand = Random.Range(0, 12);
                }
                else
                {
                    smIdentityIndex.Add(rand);
                    break;
                }
            }
        }
        if (smWeightII >= Random.Range(0, 1000))
        {
            int rand = Random.Range(0, 12);
            while (true)
            {
                if (smIdentityIndex.Contains(rand))
                {
                    rand = Random.Range(0, 12);
                }
                else
                {
                    smIdentityIndex.Add(rand);
                    break;
                }
            }
        }
        if (smWeightIII >= Random.Range(0, 1000))
        {
            int rand = Random.Range(0, 12);
            while (true)
            {
                if (smIdentityIndex.Contains(rand))
                {
                    rand = Random.Range(0, 12);
                }
                else
                {
                    smIdentityIndex.Add(rand);
                    break;
                }
            }
        }
    }

    IEnumerator MonsterSpawn(int count_1, int count_2, int count_3, float spawndelay)
    {
        for (int i = 0; i < count_1; i++) // A 타입 ( 노말 몬스터 . )
        {
            GameObject monsterI = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
            monsterI.GetComponent<Monster>().mobHp = smMonsterHp;
            monsterI.GetComponent<Monster>().mobSpeed = smMonsterSpeed;
            monsterI.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
            monsterI.GetComponent<Monster>().mobType = 0;
            yield return new WaitForSecondsRealtime(spawndelay);
        }

        for (int j = 0; j < count_2; j++) // B 타입 ( 속도 증가 체력 감소 몬스터 . )
        {
            GameObject monsterII = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
            monsterII.GetComponent<Monster>().mobHp = smMonsterHp * 0.8f;
            monsterII.GetComponent<Monster>().mobSpeed = smMonsterSpeed * 1.5f;
            monsterII.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
            monsterII.GetComponent<Monster>().mobType = 1;
            yield return new WaitForSecondsRealtime(spawndelay);
        }

        for (int k = 0; k < count_3; k++) // C 타입 ( 속도 감소 체력 증가 몬스터 . )
        {
            GameObject monsterIII = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
            monsterIII.GetComponent<Monster>().mobHp = smMonsterHp * 2.0f;
            monsterIII.GetComponent<Monster>().mobSpeed = smMonsterSpeed * 0.8f;
            monsterIII.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
            monsterIII.GetComponent<Monster>().mobType = 2;
            yield return new WaitForSecondsRealtime(spawndelay);
        }

        smStageCheck = true;
        Debug.Log("Done");

    }

}
