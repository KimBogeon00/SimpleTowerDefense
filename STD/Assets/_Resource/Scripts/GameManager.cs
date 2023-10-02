using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;


    /// <summary> 현재 선택중인 노드 인덱스 변수. </summary>
    [Header(" [ Int ]")]
    public int gmCurSelectNodeIndex;

    public int gmLastTowerIndex;

    public int gmGold;

    public float gmMobDeadTime;
    public float gmMobHpWeight;
    public float gmMobSpeedWeight;
    public float[] gmMobDeadTimes = new float[3];
    public float[] gmMobHpWeights = new float[3];
    public float[] gmMobSpeedWeights = new float[3];

    [SerializeField] float fps;
    [SerializeField] float ms;
    float deltaTime;
    [SerializeField] GameObject gmFPS;


    /// <summary> 노드 부모를 저장하기 위한 변수. </summary>
    [Space(20f)]
    [Header(" [ GameObject ]")]
    public GameObject gmMapNodesParent;
    /// <summary> 노드를 선택할때 생성되는 이미지를 저장하기위한 GameObject </summary>
    public GameObject gmNodeSpriteParent;
    /// <summary> 맵에 있는 노드를 저장하기 위한 변수. </summary>
    public GameObject[] gmMapNodes = new GameObject[100];
    public GameObject[] gmNodeType;
    /// <summary> 노드가 선택될때 생성될 이미지. </summary>
    [SerializeField] GameObject gmSelectSprite;
    [SerializeField] GameObject gmTowerUI;
    [SerializeField] GameObject gmTowerUIMananger;
    [SerializeField] GameObject gmLastNode;
    /// <summary> 노드 선택 이미지를 가로로 눕히기 위한 변수. </summary>
    [SerializeField] Quaternion gmSelectSpriteQuaternion;

    public RotateToMouseScript gmRotateToMouses;
    Vector3 gmVecMouseDownPos;


    private float Speed = 0.25f;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    public Camera camera;

    /// <summary> 생성한 노드 숫자를 저장하는 list 이다 중복 방지하기 위함. </summary>
    List<int> random = new List<int>();

    // Start is called before the first frame update
    void Start()
    {

        gmSelectSpriteQuaternion = Quaternion.Euler(90f, 0, 0);

        if (gmInstance == null)
        {
            gmInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        gmMapNodesParent = GameObject.FindWithTag("MapNodesParent"); // MapNodesParent 테그를 찾아서 gmMapNodesParent 에 대입.

        for (int i = 0; i < gmMapNodesParent.transform.childCount; i++) // mobMapWayPointParent 의 자식 웨이포인트들을 모두 mobWayPoints에 저장한다.
        {
            gmMapNodes[i] = gmMapNodesParent.transform.GetChild(i).gameObject;
        }

        GmCreateNodeNumber(8, 36);
        StartCoroutine(GmCreateNode(0, 0.25f));
        // for (int i = 0; i < random.Count; i++)
        // {
        //     GmCreateNodeTile(random[i], Random.Range(0, 4));
        // }
    }

    // Update is called once per frame
    void Update()
    {
        GmNodeSelectCheck();
        test22();
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps_();
    }

    void fps_()
    {
        fps = 1.0f / deltaTime;
        ms = deltaTime * 1000.0f;
        string text = string.Format("({1:0.} ms) {0:0.0} fps", fps, ms);
        if (fps > 50)
        {
            gmFPS.transform.GetComponent<TextMeshProUGUI>().text = "<color=#0000FF>" + text + "</color>";
        }
        else if (fps > 40)
        {
            gmFPS.transform.GetComponent<TextMeshProUGUI>().text = "<color=#9400D3>" + text + "</color>";
        }
        else if (fps > 30)
        {
            gmFPS.transform.GetComponent<TextMeshProUGUI>().text = "<color=#87CEFA>" + text + "</color>";
        }
        else
        {
            gmFPS.transform.GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>" + text + "</color>";
        }

    }

    public void WeightUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            if (gmMobDeadTimes[i] < gmMobDeadTime)
            {
                gmMobDeadTimes[i] = gmMobDeadTime;
                gmMobHpWeights[i] = gmMobHpWeight;
                gmMobSpeedWeights[i] = gmMobSpeedWeight;
                break;
            }
        }
    }

    /// <summary> 노드를 선택할때 선택 이미지를 생성하는 함수. </summary>
    public void GmCreateSelctSprite()
    {
        foreach (Transform sprite in gmNodeSpriteParent.transform) // gmNodeSpriteParent 에 이미지가 남아있다면 모두 삭제한다.
        {
            Destroy(sprite.gameObject);
        }

        // 노드가 선택되었다는 이미지를 생성한후 gmNodeSpriteParent에 넣는다.

        GameObject nodesprite = Instantiate(gmSelectSprite, gmMapNodes[gmCurSelectNodeIndex].transform.position, gmSelectSpriteQuaternion) as GameObject;
        nodesprite.transform.SetParent(gmNodeSpriteParent.transform);
    }

    /// <summary> 
    /// 노드를 생성하는 함수.
    ///</summary>
    /// <param name="nodeindex"> 노드 Index 값. </param>
    /// <param name="nodetype"> 노드 타일의 종류. </param>
    public void GmCreateNodeTile(int nodeindex, int nodetype)
    {
        Vector3 nodepos = new Vector3(gmMapNodes[nodeindex].transform.position.x, gmMapNodes[nodeindex].transform.position.y - 2f, gmMapNodes[nodeindex].transform.position.z);
        GameObject nodestile = Instantiate(gmNodeType[nodetype], nodepos, Quaternion.identity) as GameObject;
        nodestile.transform.SetParent(gmMapNodes[nodeindex].transform);
    }

    public void GmCreateNodeNumber(int nodecount, int maxnodecount)
    {
        for (int i = 0; i < nodecount; i++)
        {
            int num = Random.Range(0, maxnodecount);

            while (true)
            {
                if (random.Contains(num))
                {
                    num = Random.Range(0, maxnodecount);
                }
                else
                {
                    random.Add(num);
                    break;
                }
            }
        }
    }
    public void GmNodeSelectCheck()
    {

#if UNITY_EDITOR
        // 마우스 클릭 시
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
#else
        // 터치 시
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(0) == false)
#endif
        {

#if UNITY_EDITOR
            gmVecMouseDownPos = Input.mousePosition;
#else
            gmVecMouseDownPos = Input.GetTouch(0).position;
            if(Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif
            // 카메라에서 스크린에 마우스 클릭 위치를 통과하는 광선을 반환합니다.
            Ray ray = Camera.main.ScreenPointToRay(gmVecMouseDownPos);
            RaycastHit hit;

            // 광선으로 충돌된 collider를 hit에 넣습니다.
            if (Physics.Raycast(ray, out hit))
            {
                // 어떤 오브젝트인지 로그를 찍습니다.
                if (hit.collider.CompareTag("Node"))
                {
                    if (!gmLastNode)
                    {
                        gmLastNode = hit.collider.transform.parent.GetComponent<Node>().gameObject;
                    }

                    gmCurSelectNodeIndex = hit.collider.transform.parent.GetComponent<Node>().ndNodeIndex;
                    GameManager.gmInstance.GmCreateSelctSprite();

                    if (!gmTowerUI.activeSelf && (hit.collider.transform.parent.GetComponent<Node>().ndCurTower))
                    {
                        gmTowerUI.SetActive(true);
                        hit.collider.transform.parent.GetComponent<Node>().ndCurTower.GetComponent<Tower>().TowerRangeEffectON();
                        gmLastNode = hit.transform.gameObject;
                        gmTowerUIMananger.GetComponent<TowerUI>().TUTowerDataUpdate();
                    }
                    else if (gmTowerUI.activeSelf && (gmLastTowerIndex == gmCurSelectNodeIndex))
                    {
                        gmLastNode.GetComponentInParent<Node>().ndCurTower.GetComponent<Tower>().TowerRangeEffectOFF();
                        gmTowerUI.SetActive(false);
                    }

                    if (gmTowerUI.activeSelf && (gmLastTowerIndex != gmCurSelectNodeIndex) && hit.collider.transform.parent.GetComponent<Node>().ndCurTower)
                    {
                        gmTowerUIMananger.GetComponent<TowerUI>().TUTowerDataUpdate();
                        gmLastNode.GetComponentInParent<Node>().ndCurTower.GetComponent<Tower>().TowerRangeEffectOFF();
                        hit.collider.transform.parent.GetComponent<Node>().ndCurTower.GetComponent<Tower>().TowerRangeEffectON();
                    }

                    if (gmTowerUI.activeSelf && !hit.collider.transform.parent.GetComponent<Node>().ndCurTower)
                    {
                        gmTowerUI.SetActive(false);
                        gmLastNode.GetComponentInParent<Node>().ndCurTower.GetComponent<Tower>().TowerRangeEffectOFF();
                    }

                    gmLastNode = hit.transform.gameObject;
                    gmLastTowerIndex = gmCurSelectNodeIndex;
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

    public void testb()
    {
        int nums = random.Count;
        GmCreateNodeNumber(4, 71);
        StartCoroutine(GmCreateNode(nums, 0.45f));

        // for (int i = nums; i < random.Count; i++)
        // {
        //     GmCreateNodeTile(random[i], Random.Range(0, 4));
        // }
    }

    void test22()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                prePos = touch.position - touch.deltaPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                nowPos = touch.position - touch.deltaPosition;
                movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * Speed;
                camera.transform.Translate(movePos);
                prePos = touch.position - touch.deltaPosition;
            }
        }
    }

    IEnumerator GmCreateNode(int min, float wait)
    {
        for (int i = min; i < random.Count; i++)
        {
            GmCreateNodeTile(random[i], Random.Range(0, 4));
            //Debug.Log(i);
            yield return new WaitForSecondsRealtime(wait);
        }

        yield return null;
    }

}
