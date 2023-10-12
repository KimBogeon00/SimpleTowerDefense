using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildManager : MonoBehaviour
{
    [Header(" [ int ]")]
    public int bmTowerSelectNum; // 현재 선택중인 타워.
    public int[] bmTowerBuyCount; // 보유중인 타워 수

    [Space(20f)]
    [Header(" [ GameObject ]")]
    [SerializeField] GameObject bmTowerUI; // 타워 구매 UI 
    [SerializeField] GameObject bmTowerParent; // 타워 스폰시 정리하기위한 용도.
    public GameObject[] bmTowerList; // 건설 가능한 타워 리스트.
    [SerializeField] GameObject[] bmTowerBuyCountUI; // 보유중인 타워 수 표시.
    // Start is called before the first frame update
    void Start()
    {
        TowerCountUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BuyTower() // 타워 구매. 
    {
        if (GameManager.gmInstance.gmGold >= 15)
        {
            GameManager.gmInstance.gmGold -= 15;
            bmTowerBuyCount[Random.Range(0, 3)] += 1;
            TowerCountUpdate();
        }
    }


    public void BuildTowerNormal() // 노말 타워 빌드
    {
        if (bmTowerBuyCount[0] > 0)
        {
            if (GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].GetComponent<Node>().ndCurTower == null)
            {
                GameObject tower = Instantiate(bmTowerList[0], GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].transform.position, Quaternion.identity) as GameObject;
                GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].GetComponent<Node>().ndCurTower = tower;
                tower.transform.SetParent(bmTowerParent.transform);
            }
            bmTowerBuyCount[0] -= 1;
            TowerCountUpdate();
        }
    }
    public void BuildTowerRapid() // 레피드 타워 빌드
    {
        if (bmTowerBuyCount[1] > 0)
        {
            if (GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].GetComponent<Node>().ndCurTower == null)
            {
                GameObject tower = Instantiate(bmTowerList[1], GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].transform.position, Quaternion.identity) as GameObject;
                GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].GetComponent<Node>().ndCurTower = tower;
                tower.transform.SetParent(bmTowerParent.transform);
            }
            bmTowerBuyCount[1] -= 1;
            TowerCountUpdate();
        }
    }
    public void BuildTowerSniper() // 스나이퍼 타워 빌드
    {
        if (bmTowerBuyCount[2] > 0)
        {
            if (GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].GetComponent<Node>().ndCurTower == null)
            {
                GameObject tower = Instantiate(bmTowerList[2], GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].transform.position, Quaternion.identity) as GameObject;
                GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex].GetComponent<Node>().ndCurTower = tower;
                tower.transform.SetParent(bmTowerParent.transform);
            }
            bmTowerBuyCount[2] -= 1;
            TowerCountUpdate();
        }
    }

    void TowerCountUpdate() // 보유중인 타워수 업데이트
    {
        bmTowerBuyCountUI[0].GetComponent<TextMeshProUGUI>().text = "" + bmTowerBuyCount[0];
        bmTowerBuyCountUI[1].GetComponent<TextMeshProUGUI>().text = "" + bmTowerBuyCount[1];
        bmTowerBuyCountUI[2].GetComponent<TextMeshProUGUI>().text = "" + bmTowerBuyCount[2];
    }

}
