using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [Header(" [ int ]")]
    public int bmTowerSelectNum; // 현재 선택중인 타워.

    [Space(20f)]
    [Header(" [ GameObject ]")]
    public GameObject[] bmTowerList; // 건설 가능한 타워 리스트.
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildTower() 
    {
        if (GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].GetComponent<Node>().ndCurTower == null)
        {
            GameObject tower = Instantiate(bmTowerList[bmTowerSelectNum], GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].transform.position, Quaternion.identity) as GameObject;
            GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].GetComponent<Node>().ndCurTower = tower;
        }

    }
}
