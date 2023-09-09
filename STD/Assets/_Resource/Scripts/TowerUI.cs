using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public GameObject tuUI;

    public GameObject tuTargetNode;
    public GameObject tuTargetTower;

    int tuTowerLevel;
    float tuTowerCurExp;
    float tuTowerMaxExp;

    int tuTowerUpgradeLevel;
    float tuTowerUpgradeGold;
    float tuTowerSellGold;

    float tuTowerAtk;
    float tuTowerAtkSpeed;
    float tuTowerRange;

    [SerializeField] string[] tuTowerInfoText;

    public GameObject tuTowerLevelUI;
    public GameObject tuTowerExpUI;
    public GameObject[] tuTowerInfoTextUI = new GameObject[3];
    public GameObject[] tuTowerInfoValueUI = new GameObject[3];
    public GameObject tuTowerUpgradeLevelUI;
    public Slider tuTowerExpSlider;
    public Slider tuTowerUpgradeLevelSlider;

    public GameObject tuTowerUpgradeUI;
    public GameObject tuTowerSellUI;
    // Start is called before the first frame update
    void Start()
    {
        tuTowerUpgradeLevelSlider.maxValue = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (tuUI.activeSelf)
        {

            tuTowerLevelUI.GetComponent<TextMeshProUGUI>().text = "LV <size=170%> " + tuTowerLevel;
            tuTowerExpUI.GetComponent<TextMeshProUGUI>().text = tuTowerCurExp + "<#557190> / " + tuTowerMaxExp;
            for (int i = 0; i < tuTowerInfoTextUI.Length; i++)
            {
                tuTowerInfoTextUI[i].GetComponent<TextMeshProUGUI>().text = tuTowerInfoText[i];
            }
            tuTowerInfoValueUI[0].GetComponent<TextMeshProUGUI>().text = "" + tuTowerAtk;
            tuTowerInfoValueUI[1].GetComponent<TextMeshProUGUI>().text = "" + tuTowerRange;
            tuTowerInfoValueUI[2].GetComponent<TextMeshProUGUI>().text = "" + tuTowerAtkSpeed;


            tuTowerUpgradeLevelUI.GetComponent<TextMeshProUGUI>().text = tuTowerUpgradeLevel + " LV";
            tuTowerUpgradeUI.GetComponent<TextMeshProUGUI>().text = "" + tuTowerUpgradeGold;
            tuTowerSellUI.GetComponent<TextMeshProUGUI>().text = "" + tuTowerSellGold;




            tuTowerExpSlider.value = tuTowerCurExp;
            tuTowerUpgradeLevelSlider.value = tuTowerUpgradeLevel;
        }

    }

    public void TUTowerDataUpdate()
    {
        tuTargetNode = GameManager.gmInstance.gmMapNodes[GameManager.gmInstance.gmCurSelectNodeIndex];
        tuTargetTower = tuTargetNode.GetComponent<Node>().ndCurTower;

        tuTowerAtk = tuTargetTower.GetComponent<Tower>().twrCurAtk;
        tuTowerRange = tuTargetTower.GetComponent<Tower>().twrCurRange;
        tuTowerAtkSpeed = tuTargetTower.GetComponent<Tower>().twrCurAtkCoolTime;
        tuTowerLevel = tuTargetTower.GetComponent<Tower>().twrLevel;
        tuTowerUpgradeLevel = tuTargetTower.GetComponent<Tower>().twrUpgradeLevel;
        tuTowerCurExp = tuTargetTower.GetComponent<Tower>().twrCurExp;
        tuTowerMaxExp = tuTargetTower.GetComponent<Tower>().twrMaxExp;
        tuTowerUpgradeGold = tuTargetTower.GetComponent<Tower>().twrUpgradeGold;
        tuTowerSellGold = tuTargetTower.GetComponent<Tower>().twrSellGold;

        tuTowerExpSlider.maxValue = tuTowerMaxExp;

    }
}
