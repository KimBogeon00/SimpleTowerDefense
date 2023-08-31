using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    /// <summary> 몬스터가 이동할 웨이포인트 저장하기 위한 변수. </summary>
    [Header(" [ Int ]")]
    public int[] mobWayPointSave;

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


    // Start is called before the first frame update
    void Start()
    {
        mobMapWayPointParent = GameObject.FindWithTag("MapWayPointParent"); // MapWayPointParent 테그를 찾아서 mobMapWayPointParent 에 대입.
        mobCurWayPoint = 0; // 웨이포인트 0번 부터 시작.
        mobCurHp = mobHp; // hp 초기화
        mobHpSlider.maxValue = mobHp;


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

    /// <summary> 몬스터가 웨이포인트에 도착했을 때 바로 다음 웨이포인트로 이동하지 않고 딜레이를 주기 위한 코루틴. </summary>
    IEnumerator MonsterMoveWait(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        mobCurWayPoint += 1;
        mobMoveCheck = false;
    }
}
