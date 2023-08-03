using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    /// <summary> 몬스터가 이동할 웨이포인트 저장하기 위한 변수. </summary>
    [Header(" [ Int ]")]
    public int[] mobWayPointSave;

    [SerializeField] int mobCurWayPoint;

    [Space(20f)]
    [Header(" [ Float ]")]


    public float mobHp;
    public float mobSpeed;

    [Space(20f)]
    [Header(" [ Bool ]")]

    [SerializeField] bool mobMoveCheck;

    /// <summary> 웨이포인트 부모를 저장하기 위한 변수. </summary>
    [Space(20f)]
    [Header(" [ GameObject ]")]
    [SerializeField] GameObject mobMapWayPointParent;


    /// <summary> 맵에 있는 웨이포인트를 저장하기 위한 변수. </summary>
    public GameObject[] mobWayPoints = new GameObject[21];

    [Space(20f)]
    [Header(" [ Others ]")]
    [SerializeField] Vector3 direction;
    [SerializeField] Quaternion rotation;


    // Start is called before the first frame update
    void Start()
    {
        mobMapWayPointParent = GameObject.FindWithTag("MapWayPointParent"); // MapWayPointParent 테그를 찾아서 mobMapWayPointParent 에 대입.
        mobCurWayPoint = 0;
        for (int i = 0; i < mobMapWayPointParent.transform.childCount; i++) // mobMapWayPointParent 의 자식 웨이포인트들을 모두 mobWayPoints에 저장한다.
        {
            mobWayPoints[i] = mobMapWayPointParent.transform.GetChild(i).gameObject;
        }

    }

    // Update is called once per frame

    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, mobWayPoints[mobWayPointSave[mobCurWayPoint]].transform.position, mobSpeed * Time.deltaTime);
        // Quaternion a = Quaternion.LookRotation(mobWayPoints[mobWayPointSave[mobCurWayPoint]].transform.position, transform.up);
        // this.transform.rotation = Quaternion.Slerp(transform.rotation, a, 0.2f);
        this.transform.rotation = Quaternion.LookRotation((mobWayPoints[mobWayPointSave[mobCurWayPoint]].transform.position - this.transform.position));
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == mobWayPoints[mobWayPointSave[mobCurWayPoint]])
        {
            if (mobWayPointSave.Length > mobCurWayPoint)
            {
                if (mobMoveCheck == false)
                {
                    StartCoroutine("MonsterMoveWait", 0.15f);
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
