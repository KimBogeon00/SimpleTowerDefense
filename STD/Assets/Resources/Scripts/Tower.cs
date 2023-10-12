using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header(" [ Int ]")]
    //public int twrLevel = 1; // 타워 레벨
    public int twrUpgradeLevel = 1; // 타워 업그레이드 레벨
    /// <summary> 
    /// <para> 0 Red / 1 Green / 2 Blue / 3 Oreange / 4 Purple </para>
    ///</summary>
    public int twrColor;
    public int twrType; // 0 노말 타워 , 1 레피드 타워 , 2 스나이퍼 타워
    /// <summary> 
    /// <para> [ 0 노말 타워 ] index 0 - : 더블샷 확률, </para>
    ///</summary>
    public int[] twrValueInt;
    /// <summary> 
    /// <para>0 : Darkness, </para>
    /// <para>1 : Flame, </para>
    /// <para>2 : Heart, </para>
    /// <para>3 : Ice, </para>
    /// <para>4 : Leaves, </para>
    /// <para>5 : Light, </para>
    /// <para>6 : Lightning, </para>
    /// <para>7 : Moon, </para>
    /// <para>8 : Soil, </para>
    /// <para>9 : Sun, </para>
    /// <para>10 : Water, </para>
    /// <para>11 : Wind. </para>
    ///</summary>

    [Space(20f)]
    [Header(" [ Float ]")]
    public float twrAtk; // 타워 공격력 기본값.
    public float twrCurAtk; // 타워 공격력 현재값.
    public float twrAtkCoolTime; // 타워 공격 쿨타임 기본값.
    public float twrCurAtkCoolTime; // 타워 공격 쿨타임 현재값.
    public float twrRange; // 타워 사거리 기본값.
    public float twrCurRange; // 타워 사거리 현재값.

    /// <summary> 
    /// <para> [ 2 스나이퍼 타워 ] index 0 - : 크리티컬 확률, </para>
    /// <para> [ 2 스나이퍼 타워 ] index 1 - : 크리티컬 데미지, </para>
    /// <para> [ 2 스나이퍼 타워 ] index 2 - : 체력 비례 뎀, </para>
    ///</summary>
    public float[] twrValueFloat;

    [Space(20f)]
    [Header(" [ Bool ]")]
    [SerializeField] bool twrAtkCoolTimeCheck; // 타워 공격 쿨타임 체크.
    public bool[] twrAbilityCheck = new bool[4];
    bool twrMoveCheck = false; // 타워 위아래 움직임을 위한 변수.

    [Space(20f)]
    [Header(" [ GameObject ]")]
    [SerializeField] GameObject[] twrAtkPoint; // 타워 공격이 나가는지점.
    public GameObject twrCloseTarget; // 타워에서 가장 가까운 적.
    public GameObject[] twrBullet; // 타워가 발사할수있는 총알.
    public GameObject twrRangeEffect;
    public GameObject twrBulletParent;

    // [Space(20f)]
    [Header(" [ Others ]")]

    public string[] twrValueFStr;
    public string[] twrValueIStr;
    public LayerMask twrLayermask;

    [Tooltip("0 : Darkness, 1 : Flame, 2 : Heart, 3 : Ice, 4 : Leaves, 5 : Light, 6 : Lightning, 7 : Moon, 8 : Soil, 9 : Sun, 10 : Water, 11 : Wind")]
    public List<int> twrIdentity = new List<int>(); // 타워 속성

    [SerializeField] Material[] twrColorMaterial = new Material[5];
    // Start is called before the first frame update
    void Start()
    {
        twrBulletParent = GameObject.Find("BulletParent");

        twrColor = Random.Range(0, 5);
        // if (twrType == 1)
        // {
        //     twrRange = twrRange * 0.75f;
        //     twrAtk = twrAtk * 0.1f;
        //     twrAtkCoolTime = twrAtkCoolTime * 0.5f;
        // }
        // else if (twrType == 2)
        // {
        //     twrRange = twrRange * 1.5f;
        //     twrAtk = twrAtk * 1.75f;
        //     twrAtkCoolTime = twrAtkCoolTime * 1.5f;
        // }
        twrCurRange = twrRange;
        twrCurAtkCoolTime = twrAtkCoolTime;
        twrCurAtk = twrAtk;

        twrAtkCoolTimeCheck = false;
        TowerUpdate();
        InvokeRepeating("SearchMonster", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<MeshRenderer>().material = twrColorMaterial[twrColor];
        if (twrCloseTarget != null)
        {
            Vector3 targetpos = new Vector3(twrCloseTarget.transform.position.x, this.transform.position.y, twrCloseTarget.transform.position.z);

            this.transform.LookAt(targetpos);
        }
        if (twrCloseTarget != null && twrAtkCoolTimeCheck == false)
        {
            twrAtkCoolTimeCheck = true;
            StartCoroutine(TowerAttackDelay(twrCurAtkCoolTime, 0));
        }

        // if (twrCurExp >= twrMaxExp && twrLevel < 10)
        // {
        //     twrLevel += 1;
        //     twrCurExp = 0;
        //     twrMaxExp = twrNeedExp[twrLevel];
        //     TowerUpdate();
        // }

        float val = twrCurRange / 6.6f;
        twrRangeEffect.transform.localScale = new Vector3(val, val, 1);
        // 타워를 위아래로 움직여준다.
        if (twrMoveCheck == false)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.02f, this.transform.position.z);
            if (this.transform.position.y >= 5.5f)
            {
                twrMoveCheck = true;
            }
        }

        if (twrMoveCheck == true)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.02f, this.transform.position.z);
            if (this.transform.position.y < 3)
            {
                twrMoveCheck = false;
            }
        }

    }
    /// <summary> 타워의 사거리를 표시해주기위한 함수. </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward.normalized);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, twrCurRange);
    }

    /// <summary> 가까운 적을 탐색하기위한 함수. </summary>
    void SearchMonster()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, twrCurRange, twrLayermask);
        if (cols.Length == 0)
        {
            twrCloseTarget = null;
        }
        GameObject shortestTarget = null;

        if (cols.Length > 0)
        {
            float shortestDistance = Mathf.Infinity;
            foreach (Collider collTarget in cols)
            {
                float distance = Vector3.SqrMagnitude(transform.position - collTarget.transform.position);
                if (shortestDistance > distance)
                {
                    shortestDistance = distance;
                    shortestTarget = collTarget.gameObject;
                }
            }
            twrCloseTarget = shortestTarget;
        }
    }

    /// <summary> 타워 공격 함수 </summary>
    /// <param name="bulletnum"> 어떤 총알을 사용할것인지 . ex) 0, 1, 2... </param>
    public void TowerAttack(int bulletnum)
    {
        switch (twrType)
        {
            case 0:
                if (twrCloseTarget != null)
                {
                    GameObject twrbullet0_0 = Instantiate(twrBullet[0], twrAtkPoint[0].transform.position, Quaternion.identity) as GameObject;
                    twrbullet0_0.GetComponent<Bullet>().SetTargets(twrCloseTarget, twrCloseTarget.GetComponent<Monster>());
                    twrbullet0_0.GetComponent<Bullet>().bulletAtk = twrCurAtk;
                    twrbullet0_0.GetComponent<Bullet>().identity = twrIdentity;
                    twrbullet0_0.GetComponent<Bullet>().tower = this.gameObject;
                    twrbullet0_0.transform.SetParent(twrBulletParent.transform);
                }
                if (twrValueInt[0] > Random.Range(0, 101) && twrCloseTarget != null)
                {
                    Debug.Log("더블샷 !!");
                    GameObject twrbullet0_1 = Instantiate(twrBullet[1], twrAtkPoint[1].transform.position, Quaternion.identity) as GameObject;
                    twrbullet0_1.GetComponent<Bullet>().SetTargets(twrCloseTarget, twrCloseTarget.GetComponent<Monster>());
                    twrbullet0_1.GetComponent<Bullet>().bulletAtk = twrCurAtk;
                    twrbullet0_1.GetComponent<Bullet>().identity = twrIdentity;
                    twrbullet0_1.GetComponent<Bullet>().tower = this.gameObject;
                    twrbullet0_1.transform.SetParent(twrBulletParent.transform);
                }
                break;
            case 1:
                if (twrCloseTarget != null)
                {
                    StartCoroutine(TowerAttackType1(bulletnum));
                }
                break;
            case 2:
                if (twrValueFloat[0] > Random.Range(0, 101) && twrCloseTarget != null)
                {
                    Debug.Log("크리티컬 !!");
                    GameObject twrbullet2_0 = Instantiate(twrBullet[Random.Range(0, 3)], twrAtkPoint[0].transform.position, Quaternion.identity) as GameObject;
                    twrbullet2_0.GetComponent<Bullet>().SetTargets(twrCloseTarget, twrCloseTarget.GetComponent<Monster>());
                    twrbullet2_0.GetComponent<Bullet>().bulletAtk = twrCurAtk * twrValueFloat[1];
                    twrbullet2_0.GetComponent<Bullet>().identity = twrIdentity;
                    twrbullet2_0.GetComponent<Bullet>().tower = this.gameObject;
                    twrbullet2_0.transform.SetParent(twrBulletParent.transform);
                }
                else if (twrCloseTarget != null)
                {
                    GameObject twrbullet2_1 = Instantiate(twrBullet[Random.Range(0, 3)], twrAtkPoint[0].transform.position, Quaternion.identity) as GameObject;
                    twrbullet2_1.GetComponent<Bullet>().SetTargets(twrCloseTarget, twrCloseTarget.GetComponent<Monster>());
                    twrbullet2_1.GetComponent<Bullet>().bulletAtk = twrCurAtk;
                    twrbullet2_1.GetComponent<Bullet>().identity = twrIdentity;
                    twrbullet2_1.GetComponent<Bullet>().tower = this.gameObject;
                    twrbullet2_1.transform.SetParent(twrBulletParent.transform);
                }
                break;
            default:
                break;
        }


    }

    public void NodeIdentityI()
    {

    }
    public void NodeIdentityII()
    {

    }
    public void NodeIdentityIII()
    {

    }
    public void NodeIdentityIV()
    {

    }

    public void TowerRangeEffectON()
    {
        twrRangeEffect.SetActive(true);
    }
    public void TowerRangeEffectOFF()
    {
        twrRangeEffect.SetActive(false);
    }

    public void TowerUpdate()
    {
        twrUpgradeLevel = TowerUpgrade.tuInstance.tuTowerColorUpgradeLevel[twrColor];
        int _2 = Mathf.FloorToInt(twrUpgradeLevel / 2);
        int _3 = Mathf.FloorToInt(twrUpgradeLevel / 3);
        int _5 = Mathf.FloorToInt(twrUpgradeLevel / 5);
        twrCurAtk = Mathf.Floor(twrAtk * (1 + ((twrUpgradeLevel - 1) * 0.1f)) * 100) / 100;
        twrCurRange = Mathf.Floor(twrRange * (1 + ((_3) * 0.05f)) * 100) / 100;

        switch (twrType)
        {
            case 0:
                if (twrUpgradeLevel < 151)
                {
                    twrValueInt[0] = _2 + 5; // 타워 더블샷 확률 . 기본 5% , 2레벨마다 1%씩 증가.
                }
                else if (twrUpgradeLevel >= 151)
                {
                    twrValueInt[0] = 80;
                }
                twrCurAtkCoolTime = Mathf.Floor(twrAtkCoolTime * Mathf.Pow(0.93f, _5) * 100) / 100;
                break;
            case 1:
                twrCurAtkCoolTime = Mathf.Floor(twrAtkCoolTime * Mathf.Pow(0.95f, _5) * 100) / 100;
                break;
            case 2:
                if (twrUpgradeLevel < 151)
                {
                    twrValueFloat[0] = 5 + twrUpgradeLevel * 0.5f;
                }
                else if (twrUpgradeLevel >= 151)
                {
                    twrValueFloat[0] = 80;
                }

                twrValueFloat[1] = 1.5f + (0.05f * twrUpgradeLevel);

                if (twrUpgradeLevel < 121)
                {
                    twrValueFloat[2] = 0.01f + (_3 * 0.001f);
                }
                else if (twrUpgradeLevel >= 121)
                {
                    twrValueFloat[2] = 0.05f;
                }
                twrCurAtkCoolTime = Mathf.Floor(twrAtkCoolTime * Mathf.Pow(0.97f, _5) * 100) / 100;
                break;
            default:
                break;
        }

    }


    /// <summary> 타워가 공격하기 위한 코루틴.. </summary>
    /// <param name="delay"> 타워의 공격속도 , 딜레이로 사용함. </param>
    /// <param name="bulletnum"> 어떤 총알을 사용할것인지 . ex) 0, 1, 2... </param>
    IEnumerator TowerAttackDelay(float delay, int bulletnum)
    {
        yield return new WaitForSecondsRealtime(delay);
        TowerAttack(bulletnum);
        twrAtkCoolTimeCheck = false;
    }

    IEnumerator TowerAttackType1(int bulletnum)
    {
        for (int i = 0; i < 6; i++)
        {
            if (twrType == 1)
            {
                if (twrCloseTarget != null)
                {
                    GameObject twrbullet1 = Instantiate(twrBullet[Random.Range(0, 2)], twrAtkPoint[i].transform.position, Quaternion.identity) as GameObject;
                    twrbullet1.GetComponent<Bullet>().SetTargets(twrCloseTarget, twrCloseTarget.GetComponent<Monster>());
                    twrbullet1.GetComponent<Bullet>().bulletAtk = twrCurAtk;
                    twrbullet1.GetComponent<Bullet>().identity = twrIdentity;
                    twrbullet1.GetComponent<Bullet>().tower = this.gameObject;
                    twrbullet1.transform.SetParent(twrBulletParent.transform);
                }
            }
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
