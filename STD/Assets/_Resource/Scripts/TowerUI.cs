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
    int tuTowerAbility1;
    bool[] tuTowerAbilityCheck = new bool[5];
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
    public GameObject[] tuTowerInfoUI = new GameObject[8];
    public GameObject[] tuTowerInfoTextUI = new GameObject[8];
    public GameObject[] tuTowerInfoValueUI = new GameObject[8];
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
            TUTowerDataUpdate();

            tuTowerLevelUI.GetComponent<TextMeshProUGUI>().text = "LV <size=170%> " + tuTowerLevel;
            tuTowerExpUI.GetComponent<TextMeshProUGUI>().text = tuTowerCurExp + "<#557190> / " + tuTowerMaxExp;
            for (int i = 0; i < tuTowerInfoTextUI.Length; i++)
            {
                tuTowerInfoTextUI[i].GetComponent<TextMeshProUGUI>().text = tuTowerInfoText[i];
            }
            tuTowerInfoValueUI[0].GetComponent<TextMeshProUGUI>().text = "" + tuTowerAtk;
            tuTowerInfoValueUI[1].GetComponent<TextMeshProUGUI>().text = "" + tuTowerRange;
            tuTowerInfoValueUI[2].GetComponent<TextMeshProUGUI>().text = tuTowerAtkSpeed + " / 1 sec";


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

        if (tuTargetTower.GetComponent<Tower>().twrType == 0)
        {
            tuTowerInfoValueUI[3].GetComponent<TextMeshProUGUI>().text = tuTargetTower.GetComponent<Tower>().twrValueInt[0] + " %";
            tuTowerInfoText[3] = tuTargetTower.GetComponent<Tower>().twrValueIStr[0];
        }
        else if (tuTargetTower.GetComponent<Tower>().twrType == 2)
        {
            tuTowerInfoValueUI[3].GetComponent<TextMeshProUGUI>().text = tuTargetTower.GetComponent<Tower>().twrValueFloat[0] + " %";
            tuTowerInfoValueUI[4].GetComponent<TextMeshProUGUI>().text = tuTargetTower.GetComponent<Tower>().twrValueFloat[1] * 100 + " %";
            tuTowerInfoValueUI[5].GetComponent<TextMeshProUGUI>().text = tuTargetTower.GetComponent<Tower>().twrValueFloat[2] * 100 + " %";

            tuTowerInfoText[3] = tuTargetTower.GetComponent<Tower>().twrValueFStr[0];
            tuTowerInfoText[4] = tuTargetTower.GetComponent<Tower>().twrValueFStr[1];
            tuTowerInfoText[5] = tuTargetTower.GetComponent<Tower>().twrValueFStr[2];
        }

        //tuTowerLevel = tuTargetTower.GetComponent<Tower>().twrLevel;
        tuTowerUpgradeLevel = tuTargetTower.GetComponent<Tower>().twrUpgradeLevel;
        tuTowerCurExp = tuTargetTower.GetComponent<Tower>().twrCurExp;
        tuTowerMaxExp = tuTargetTower.GetComponent<Tower>().twrMaxExp;
        tuTowerUpgradeGold = tuTargetTower.GetComponent<Tower>().twrUpgradeGold;
        tuTowerSellGold = tuTargetTower.GetComponent<Tower>().twrSellGold;

        for (int tu2 = 0; tu2 < 4; tu2++)
        {
            tuTowerAbilityCheck[tu2] = tuTargetTower.GetComponent<Tower>().twrAbilityCheck[tu2];
        }

        if (tuTowerAbilityCheck[0])
        {
            tuTowerInfoUI[3].SetActive(true);
        }
        else
        {
            tuTowerInfoUI[3].SetActive(false);
        }

        if (tuTowerAbilityCheck[1])
        {
            tuTowerInfoUI[4].SetActive(true);
        }
        else
        {
            tuTowerInfoUI[4].SetActive(false);
        }

        if (tuTowerAbilityCheck[2])
        {
            tuTowerInfoUI[5].SetActive(true);
        }
        else
        {
            tuTowerInfoUI[5].SetActive(false);
        }

        if (tuTowerAbilityCheck[3])
        {
            tuTowerInfoUI[6].SetActive(true);
        }
        else
        {
            tuTowerInfoUI[6].SetActive(false);
        }

        if (tuTowerAbilityCheck[4])
        {
            tuTowerInfoUI[7].SetActive(true);
        }
        else
        {
            tuTowerInfoUI[7].SetActive(false);
        }

        tuTowerExpSlider.maxValue = tuTowerMaxExp;
    }

    public void TUTowerUpgrade()
    {
        tuTargetTower.GetComponent<Tower>().TowerUpdate();
        TUTowerDataUpdate();
    }

    public void TUColorChange()
    {
        tuTargetTower.GetComponent<Tower>().twrColor = Random.Range(0, 5);
        tuTargetTower.GetComponent<Tower>().TowerUpdate();
    }


}
