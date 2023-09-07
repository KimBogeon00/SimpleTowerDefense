using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public GameObject tuTargetNode;
    public GameObject tuTargetTower;

    public int tuTowerLevel;
    public float tuTowerCurExp;
    public float tuTowerMaxExp;

    public int tuTowerUpgradeLevel;
    public float tuTowerUpgradeGold;
    public float tuTowerSellGold;

    public float tuTowerAtk;
    public float tuTowerAtkSpeed;
    public float tuTowerRange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ttss()
    {
        tuTargetNode = GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex];
        tuTargetTower = tuTargetNode.GetComponent<Node>().ndCurTower;
    }
}
