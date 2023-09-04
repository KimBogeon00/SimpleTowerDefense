using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;


    /// <summary> 현재 선택중인 노드 인덱스 변수. </summary>
    [Header(" [ Int ]")]
    public int gmCurSelectNodeIndex;


    /// <summary> 노드 부모를 저장하기 위한 변수. </summary>
    [Space(20f)]
    [Header(" [ GameObject ]")]
    public GameObject gmMapNodesParent;
    /// <summary> 노드를 선택할때 생성되는 이미지를 저장하기위한 GameObject </summary>
    public GameObject gmNodeSpriteParent;
    /// <summary> 맵에 있는 노드를 저장하기 위한 변수. </summary>
    public GameObject[] gmMapNodes = new GameObject[45];
    public GameObject[] gmNodeType;
    /// <summary> 노드가 선택될때 생성될 이미지. </summary>
    [SerializeField] GameObject gmSelectSprite;
    /// <summary> 노드 선택 이미지를 가로로 눕히기 위한 변수. </summary>
    [SerializeField] Quaternion gmSelectSpriteQuaternion;

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

    public void testb()
    {
        int nums = random.Count;
        GmCreateNodeNumber(4, 36);
        StartCoroutine(GmCreateNode(nums, 0.65f));

        // for (int i = nums; i < random.Count; i++)
        // {
        //     GmCreateNodeTile(random[i], Random.Range(0, 4));
        // }
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
