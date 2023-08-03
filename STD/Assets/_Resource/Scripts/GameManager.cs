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
    /// <summary> 노드가 선택될때 생성될 이미지. </summary>
    [SerializeField] GameObject gmSelectSprite;
    /// <summary> 노드 선택 이미지를 가로로 눕히기 위한 변수. </summary>
    [SerializeField] Quaternion gmSelectSpriteQuaternion;


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
}
