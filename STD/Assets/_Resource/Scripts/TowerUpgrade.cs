using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerUpgrade : MonoBehaviour
{
    public static TowerUpgrade tuInstance;
    [Header(" [ Int ]")]

    public int tuTowerColorUpgradeGold;
    public int tuTowerColorTotalUpgradeCount;
    public int[] tuTowerColorUpgradeLevel = new int[5];
    [Space(20f)]
    [Header(" [ GameObject ]")]
    [SerializeField] GameObject[] tuTowerLevel = new GameObject[5];
    [SerializeField] GameObject tuUpgradeGold;
    [SerializeField] GameObject tuTowerParent;

    [Space(20f)]
    [Header(" [ Other ]")]

    [SerializeField] List<GameObject> tuTowers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (tuInstance == null)
        {
            tuInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        tuTowerColorUpgradeGold = 10;
        for (int i = 0; i < 5; i++)
        {
            tuTowerColorUpgradeLevel[i] = 1;
        }
        tuUpgradeGold.GetComponent<TextMeshProUGUI>().text = "업그레이드 비용 : " + tuTowerColorUpgradeGold;
    }

    // Update is called once per frame
    void Update()
    {
        for (int tu1 = 0; tu1 < 5; tu1++)
        {
            // 몬스터 가중치 출력 부분 //
            tuTowerLevel[tu1].GetComponent<TextMeshProUGUI>().text = "" + tuTowerColorUpgradeLevel[tu1];
        }
    }

    public void TowerColorUpgrade(int colorindex)
    {
        tuTowers.Clear();
        for (int tu2 = 0; tu2 < tuTowerParent.transform.childCount; tu2++)
        {
            tuTowers.Add(tuTowerParent.transform.GetChild(tu2).gameObject);
        }
        if (GameManager.gmInstance.gmGold >= tuTowerColorUpgradeGold)
        {
            tuTowerColorUpgradeLevel[colorindex] += 1;
            tuTowerColorTotalUpgradeCount += 1;
            GameManager.gmInstance.gmGold -= tuTowerColorUpgradeGold;
            tuTowerColorUpgradeGold = tuTowerColorTotalUpgradeCount + 10;
        }
        for (int tu3 = 0; tu3 < tuTowers.Count; tu3++)
        {
            tuTowers[tu3].GetComponent<Tower>().TowerUpdate();
        }
        tuUpgradeGold.GetComponent<TextMeshProUGUI>().text = "업그레이드 비용 : " + tuTowerColorUpgradeGold;
    }
}
