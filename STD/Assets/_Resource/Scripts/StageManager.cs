using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager smInstance;
    [Header(" [ Int ]")]

    [SerializeField] int[] smMonsterColorCount = new int[5];
    public int smCurStageIndex;

    public int smMonsterTotalCount;
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
    public bool smStageEnd;

    [Space(20f)]
    [Header(" [ GameObject ]")]
    public GameObject smStageData;
    public GameObject[] smMonsterList = new GameObject[10];
    public GameObject smSpawnPoint;

    List<int> smIdentityIndex = new List<int>();
    List<int> smMonsterSpawnColor = new List<int>();
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
            if (smMonsterTotalCount == smMonsterKillCount)
            {
                smStageEnd = true;
                smCurStageIndex += 1;
                smIdentityIndex.Clear();
                smMonsterKillCount = 0;
                Invoke("StageStart", 5.0f);
            }
        }

    }
    public void StageStart()
    {
        StageDataLoad();
        smStageCheck = false;
        smMonsterTotalCount = 0;
        for (int i = 0; i < 5; i++)
        {
            smMonsterColorCount[i] = ((int)((MonsterStudy.msInstance.msColorWeight[i] * 0.01) * smMonsterCount));
            smMonsterTotalCount += smMonsterColorCount[i];
        }
        StartCoroutine(MonsterSpawn(0.8f));
    }

    public void StageDataLoad()
    {
        (smMonsterHp, smMonsterSpeed, smMonsterCount, smSpawnMonster, smWeightI, smWeightII, smWeightIII) = smStageData.GetComponent<StageData>().GetData(smCurStageIndex);

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

    IEnumerator MonsterSpawn(float spawndelay)
    {
        int[] val = new int[5];
        for (int i = 0; i < 5; i++)
        {
            val[i] = smMonsterColorCount[i];
        }
        while (val[0] != 0 || val[1] != 0 || val[2] != 0 || val[3] != 0 || val[4] != 0)
        {
            if (val[0] != 0)
            {
                GameObject monsterR = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
                monsterR.GetComponent<Monster>().mobHp = smMonsterHp;
                monsterR.GetComponent<Monster>().mobSpeed = smMonsterSpeed;
                monsterR.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
                monsterR.GetComponent<Monster>().mobType = 0;
                monsterR.GetComponent<Monster>().mobColor = 0;
                val[0] -= 1;
                yield return new WaitForSecondsRealtime(spawndelay);
            }
            if (val[1] != 0)
            {
                GameObject monsterG = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
                monsterG.GetComponent<Monster>().mobHp = smMonsterHp;
                monsterG.GetComponent<Monster>().mobSpeed = smMonsterSpeed;
                monsterG.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
                monsterG.GetComponent<Monster>().mobType = 0;
                monsterG.GetComponent<Monster>().mobColor = 1;
                val[1] -= 1;
                yield return new WaitForSecondsRealtime(spawndelay);
            }
            if (val[2] != 0)
            {
                GameObject monsterB = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
                monsterB.GetComponent<Monster>().mobHp = smMonsterHp;
                monsterB.GetComponent<Monster>().mobSpeed = smMonsterSpeed;
                monsterB.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
                monsterB.GetComponent<Monster>().mobType = 0;
                monsterB.GetComponent<Monster>().mobColor = 2;
                val[2] -= 1;
                yield return new WaitForSecondsRealtime(spawndelay);
            }
            if (val[3] != 0)
            {
                GameObject monsterO = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
                monsterO.GetComponent<Monster>().mobHp = smMonsterHp;
                monsterO.GetComponent<Monster>().mobSpeed = smMonsterSpeed;
                monsterO.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
                monsterO.GetComponent<Monster>().mobType = 0;
                monsterO.GetComponent<Monster>().mobColor = 3;
                val[3] -= 1;
                yield return new WaitForSecondsRealtime(spawndelay);
            }
            if (val[4] != 0)
            {
                GameObject monsterP = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
                monsterP.GetComponent<Monster>().mobHp = smMonsterHp;
                monsterP.GetComponent<Monster>().mobSpeed = smMonsterSpeed;
                monsterP.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
                monsterP.GetComponent<Monster>().mobType = 0;
                monsterP.GetComponent<Monster>().mobColor = 4;
                val[4] -= 1;
                yield return new WaitForSecondsRealtime(spawndelay);
            }


        }
        // for (int j = 0; j < 5; j++)
        // {
        //     for (int i = 0; i < smMonsterColorCount[j]; i++) // A 타입 ( 노말 몬스터 . )
        //     {
        //         GameObject monsterI = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        //         monsterI.GetComponent<Monster>().mobHp = smMonsterHp;
        //         monsterI.GetComponent<Monster>().mobSpeed = smMonsterSpeed;
        //         monsterI.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
        //         monsterI.GetComponent<Monster>().mobType = 0;
        //         monsterI.GetComponent<Monster>().mobColor = j;
        //         yield return new WaitForSecondsRealtime(spawndelay);
        //     }
        // }


        // for (int j = 0; j < count_2; j++) // B 타입 ( 속도 증가 체력 감소 몬스터 . )
        // {
        //     GameObject monsterII = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        //     monsterII.GetComponent<Monster>().mobHp = smMonsterHp * 0.8f;
        //     monsterII.GetComponent<Monster>().mobSpeed = smMonsterSpeed * 1.5f;
        //     monsterII.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
        //     monsterII.GetComponent<Monster>().mobType = 1;
        //     yield return new WaitForSecondsRealtime(spawndelay);
        // }

        // for (int k = 0; k < count_3; k++) // C 타입 ( 속도 감소 체력 증가 몬스터 . )
        // {
        //     GameObject monsterIII = Instantiate(smMonsterList[smSpawnMonster[0]], smSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        //     monsterIII.GetComponent<Monster>().mobHp = smMonsterHp * 2.0f;
        //     monsterIII.GetComponent<Monster>().mobSpeed = smMonsterSpeed * 0.8f;
        //     monsterIII.GetComponent<Monster>().mobIdentityIndex = smIdentityIndex;
        //     monsterIII.GetComponent<Monster>().mobType = 2;
        //     yield return new WaitForSecondsRealtime(spawndelay);
        // }

        smStageCheck = true;
        Debug.Log("Done");
    }



}
