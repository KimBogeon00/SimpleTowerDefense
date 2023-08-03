using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    public GameObject[] tmWayPoints = new GameObject[21];
    public int[] tmWayPointIndex;
    public float speed;
    public int tmCurPoint;

    public bool a;
    // Start is called before the first frame update
    void Start()
    {
        tmCurPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, tmWayPoints[tmWayPointIndex[tmCurPoint]].transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == tmWayPoints[tmWayPointIndex[tmCurPoint]])
        {
            if (tmWayPointIndex.Length > tmCurPoint)
            {
                if (a == false)
                {
                    StartCoroutine("Wait");
                    a = true;
                }
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        tmCurPoint += 1;
        a = false;
    }


}
