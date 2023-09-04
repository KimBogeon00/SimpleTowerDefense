using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    /// <summary> 몬스터가 이동할 웨이포인트 저장하기 위한 변수. </summary>
    [Header(" [ Int ]")]
    public int[] mobWayPointSave;
    public int mobType;
    [SerializeField] int mobCurWayPoint;



    [Space(20f)]
    [Header(" [ Float ]")]


    public float mobHp;
    public float mobCurHp;
    public float mobSpeed;

    [Space(20f)]
    [Header(" [ Bool ]")]

    [SerializeField] bool mobMoveCheck;

    /// <summary> 웨이포인트 부모를 저장하기 위한 변수. </summary>
    [Space(20f)]
    [Header(" [ GameObject ]")]
    [SerializeField] GameObject mobMapWayPointParent;


    /// <summary> 맵에 있는 웨이포인트를 저장하기 위한 변수. </summary>
    public GameObject[] mobWayPoints = new GameObject[29];

    //[SerializeField] GameObject mobParent;

    [Space(20f)]
    [Header(" [ Others ]")]
    Vector3 direction;
    Quaternion rotation;
    [SerializeField] Vector3 mobLookPos; // 몬스터가 바라볼 좌표.
    [SerializeField] Slider mobHpSlider; // 몬스터 hp바 슬라이더.
    [SerializeField] Image mobHpSliderFillImage; // 몬스터 슬라이더 FILL 이미지.
    [SerializeField] Sprite[] mobSliderImage = new Sprite[3];
    [SerializeField] Image[] mobIdentityImage = new Image[3];
    [SerializeField] Sprite[] mobIdentityImageList = new Sprite[13];

    public List<int> mobIdentityIndex = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        mobMapWayPointParent = GameObject.FindWithTag("MapWayPointParent"); // MapWayPointParent 테그를 찾아서 mobMapWayPointParent 에 대입.
        mobCurWayPoint = 0; // 웨이포인트 0번 부터 시작.
        mobCurHp = mobHp; // hp 초기화
        mobHpSlider.maxValue = mobHp;

        mobHpSliderFillImage.sprite = mobSliderImage[mobType];

        mobIdentityImage[0].sprite = mobIdentityImageList[12];
        mobIdentityImage[1].sprite = mobIdentityImageList[12];
        mobIdentityImage[2].sprite = mobIdentityImageList[12];

        for (int i = 0; i < mobIdentityIndex.Count; i++)
        {
            mobIdentityImage[i].sprite = mobIdentityImageList[mobIdentityIndex[i]];
        }

        for (int i = 0; i < mobMapWayPointParent.transform.childCount; i++) // mobMapWayPointParent 의 자식 웨이포인트들을 모두 mobWayPoints에 저장한다.
        {
            mobWayPoints[i] = mobMapWayPointParent.transform.GetChild(i).gameObject;
        }

    }

    // Update is called once per frame

    void Update()
    {

        // 몬스터가 알아서 이동할수있게 해줌.
        this.transform.position = Vector3.MoveTowards(this.transform.position, mobWayPoints[mobWayPointSave[mobCurWayPoint]].transform.position, mobSpeed * Time.deltaTime);
        // 몬스터가 이동할때 목표물을 바라보면서 이동할수있게 해줌.
        mobLookPos = new Vector3((mobWayPoints[mobWayPointSave[mobCurWayPoint]].transform.position).x, this.transform.position.y, (mobWayPoints[mobWayPointSave[mobCurWayPoint]].transform.position).z);

        transform.LookAt(mobLookPos);
        mobHpSlider.value = mobCurHp;

        if (mobCurHp <= 0)
        {
            StageManager.smInstance.smMonsterKillCount += 1;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == mobWayPoints[mobWayPointSave[mobCurWayPoint]])
        {
            if (mobWayPointSave.Length > mobCurWayPoint)
            {
                if (mobMoveCheck == false)
                {
                    if (mobWayPointSave[mobCurWayPoint] == 28)
                    {
                        Destroy(this.gameObject);
                    }
                    StartCoroutine("MonsterMoveWait", 0.1f);
                    mobMoveCheck = true;
                }
            }
        }
    }

    public void RotateToMouse(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    public void MonsterHit(float atk, List<int> identity)
    {
        int identityCount = mobIdentityIndex.Count;
        int identitySameCount = 0;
        for (int i = 0; i < identityCount; i++)
        {
            if (identity.Contains(mobIdentityIndex[i]))
            {
                identitySameCount += 1;
            }
        }

        Debug.Log("--- 플레이어 아이덴티티와 몬스터 아이덴티티 겹치는 갯수 : " + identitySameCount);

        if (identitySameCount == 0)
        {
            switch (identityCount)
            {
                case 0:
                    mobCurHp -= atk;
                    Debug.Log("----- [ 1 ] 플레이어 일반 공격, : " + atk + " 몬스터 체력 : " + mobCurHp);
                    break;
                case 1:
                    mobCurHp -= Mathf.Floor((atk * 0.9f) * 100) / 100; // 데미지 10 % 감소
                    Debug.Log("----- [ 2 ] 데미지 감소 I , : " + Mathf.Floor((atk * 0.9f) * 100) / 100 + " 몬스터 체력 : " + mobCurHp);
                    break;
                case 2:
                    mobCurHp -= Mathf.Floor((atk * 0.75f) * 100) / 100; // 데미지 25 % 감소
                    Debug.Log("----- [ 3 ] 데미지 감소 II , : " + Mathf.Floor((atk * 0.75f) * 100) / 100 + " 몬스터 체력 : " + mobCurHp);
                    break;
                case 3:
                    mobCurHp -= Mathf.Floor((atk * 0.5f) * 100) / 100; // 데미지 50 % 감소
                    Debug.Log("----- [ 4 ] 데미지 감소 III , : " + Mathf.Floor((atk * 0.5f) * 100) / 100 + " 몬스터 체력 : " + mobCurHp);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (identitySameCount)
            {
                case 0:
                    mobCurHp -= atk;
                    Debug.Log("----- [ 1-1 ] 플레이어 일반 공격, : " + atk + " 몬스터 체력 : " + mobCurHp);
                    break;
                case 1:
                    mobCurHp -= Mathf.Floor((atk * 1.1f) * 100) / 100; // 데미지 10 % 감소
                    Debug.Log("----- [ 2 ] 데미지 증가 I , : " + Mathf.Floor((atk * 1.1f) * 100) / 100 + " 몬스터 체력 : " + mobCurHp);
                    break;
                case 2:
                    mobCurHp -= Mathf.Floor((atk * 1.2f) * 100) / 100; // 데미지 25 % 감소
                    Debug.Log("----- [ 3 ] 데미지 증가 II , : " + Mathf.Floor((atk * 1.2f) * 100) / 100 + " 몬스터 체력 : " + mobCurHp);
                    break;
                case 3:
                    mobCurHp -= Mathf.Floor((atk * 1.3f) * 100) / 100; // 데미지 50 % 감소
                    Debug.Log("----- [ 4 ] 데미지 증가 III , : " + Mathf.Floor((atk * 1.3f) * 100) / 100 + " 몬스터 체력 : " + mobCurHp);
                    break;
                default:
                    break;
            }
        }


    }

    /// <summary> 몬스터가 웨이포인트에 도착했을 때 바로 다음 웨이포인트로 이동하지 않고 딜레이를 주기 위한 코루틴. </summary>
    IEnumerator MonsterMoveWait(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        mobCurWayPoint += 1;
        mobMoveCheck = false;
    }
}
