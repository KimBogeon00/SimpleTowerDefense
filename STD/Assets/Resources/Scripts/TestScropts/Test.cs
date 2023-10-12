using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject ttt;
    public GameObject t;
    public GameObject Target;
    public GameObject[] Obj = new GameObject[10];
    public RotateToMouseScript rotateToMouses;
    Vector3 m_vecMouseDownPos;

    public GameObject dasd;
    public GameObject dasdpos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        // 마우스 클릭 시
        if (Input.GetMouseButtonDown(0))
#else
        // 터치 시
        if (Input.touchCount > 0)
#endif
        {

#if UNITY_EDITOR
            m_vecMouseDownPos = Input.mousePosition;
#else
            m_vecMouseDownPos = Input.GetTouch(0).position;
            if(Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif
            // 카메라에서 스크린에 마우스 클릭 위치를 통과하는 광선을 반환합니다.
            Ray ray = Camera.main.ScreenPointToRay(m_vecMouseDownPos);
            RaycastHit hit;

            // 광선으로 충돌된 collider를 hit에 넣습니다.
            if (Physics.Raycast(ray, out hit))
            {
                // 어떤 오브젝트인지 로그를 찍습니다.
                if (hit.collider.CompareTag("Node"))
                {
                    Debug.Log("a");
                    GameManager.gmInstance.gmCurSelectNodeIndex = hit.collider.transform.parent.GetComponent<Node>().ndNodeIndex;
                    GameManager.gmInstance.GmCreateSelctSprite();
                    if (!ttt.activeSelf && hit.collider.transform.parent.GetComponent<Node>().ndCurTower)
                    {
                        ttt.SetActive(true);
                    }
                }
                // // 오브젝트 별로 코드를 작성할 수 있습니다.
                // if (hit.collider.name == "Cube")
                //     Debug.Log("Cube Hit");
                // else if (hit.collider.name == "Capsule")
                //     Debug.Log("Capsule Hit");
                // else if (hit.collider.name == "Sphere")
                //     Debug.Log("Sphere Hit");
                // else if (hit.collider.name == "Cylinder")
                //     Debug.Log("Cylinder Hit");
            }

        }

    }

    public void A()
    {
        GameObject a = Instantiate(Obj[Random.Range(0, 10)], t.transform.position, Quaternion.identity) as GameObject;
        a.gameObject.GetComponent<ProjectileMoveScript>().SetTargets(Target, Target.GetComponent<Monster>());
    }

    public void ba()
    {
        Instantiate(dasd, dasdpos.transform.position, Quaternion.identity);
    }
}
