using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header(" [ Int ]")]
    public int twrLevel = 1; // 타워 레벨
    public int twrUpgradeLevel = 1; // 타워 업그레이드 레벨
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
    public float twrCurExp; // 타워 경험치 현재값.
    public float twrMaxExp; // 타워 해당 레벨 경험치 최대값
    public float[] twrNeedExp = new float[11]; // 타워 레벨 경험치 요구량.
    public float twrBuyGold; // 타워 구입 비용
    public float twrUpgradeGold; // 타워 업그레이드 비용
    public float twrSellGold; // 타워 판매 비용

    [Space(20f)]
    [Header(" [ Bool ]")]
    [SerializeField] bool twrAtkCoolTimeCheck; // 타워 공격 쿨타임 체크.
    bool twrMoveCheck = false; // 타워 위아래 움직임을 위한 변수.

    [Space(20f)]
    [Header(" [ GameObject ]")]
    [SerializeField] GameObject twrAtkPoint; // 타워 공격이 나가는지점.
    public GameObject twrCloseTarget; // 타워에서 가장 가까운 적.
    public GameObject[] twrBullet; // 타워가 발사할수있는 총알.
    public GameObject twrRangeEffect;
    // [Space(20f)]
    [Header(" [ Others ]")]
    public LayerMask twrLayermask;

    [Tooltip("0 : Darkness, 1 : Flame, 2 : Heart, 3 : Ice, 4 : Leaves, 5 : Light, 6 : Lightning, 7 : Moon, 8 : Soil, 9 : Sun, 10 : Water, 11 : Wind")]
    public List<int> twrIdentity = new List<int>(); // 타워 속성
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 11; i++)
        {
            twrNeedExp[i] = i * 100;
        }
        twrCurRange = twrRange;
        twrCurAtkCoolTime = twrAtkCoolTime;
        twrCurAtk = twrAtk;
        twrMaxExp = twrNeedExp[1];
        twrAtkCoolTimeCheck = false;
        InvokeRepeating("SearchMonster", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
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

        if (twrCurExp >= twrMaxExp && twrLevel < 10)
        {
            twrLevel += 1;
            twrCurExp = 0;
            twrMaxExp = twrNeedExp[twrLevel];
            TowerUpdate();
        }

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
        if (twrCloseTarget != null)
        {
            GameObject twrbullets = Instantiate(twrBullet[bulletnum], twrAtkPoint.transform.position, Quaternion.identity) as GameObject;
            twrbullets.GetComponent<Bullet>().SetTargets(twrCloseTarget, twrCloseTarget.GetComponent<Monster>());
            twrbullets.GetComponent<Bullet>().bulletAtk = twrCurAtk;
            twrbullets.GetComponent<Bullet>().identity = twrIdentity;
            twrbullets.GetComponent<Bullet>().tower = this.gameObject;
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

    public void TowerUpgrade()
    {
        twrUpgradeLevel += 1;
        TowerUpdate();
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
        twrCurAtk = Mathf.Floor((twrAtk * (1 + ((twrUpgradeLevel - 1) * 0.1f))) * (1 + ((twrLevel - 1) * 0.1f)) * 100) / 100;
        twrCurRange = Mathf.Floor((twrRange * (1 + ((twrUpgradeLevel - 1) * 0.05f))) * (1 + ((twrLevel - 1) * 0.05f)) * 100) / 100;
        twrCurAtkCoolTime = Mathf.Floor((twrAtkCoolTime * (1 - ((twrUpgradeLevel - 1) * 0.05f))) * (1 - ((twrLevel - 1) * 0.05f)) * 100) / 100;
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
}
