using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TowerUpgrade : MonoBehaviour
{
    public static TowerUpgrade tuInstance;
    [Header(" [ Int ]")]
    public int[] tuTowerColorCount = new int[5]; // 타워 색갈별 갯수
    public int[] tuTowerKindCount = new int[3]; // 타워 종류별 갯수
    public int tuTowerColorUpgradeGold; // 타워 업그레이드 필요한 비용
    public int tuTowerColorTotalUpgradeCount; // 총 타워 업그레이드 횟수
    public int[] tuTowerColorUpgradeLevel = new int[5]; // 색갈별 타워 업그레이드 횟수
    [Space(20f)]
    [Header(" [ GameObject ]")]

    [SerializeField] GameObject[] tuTowerUpgradeUI = new GameObject[5]; // DoTween 사용을 위한 UpgradeUI
    [SerializeField] GameObject[] tuTowerLevel = new GameObject[5]; // 타워 레벨 표시를 위한 GameObject
    [SerializeField] GameObject tuUpgradeGold; // 업그레이드 비용 표시위한 GameObject
    [SerializeField] GameObject tuTowerParent; // 타워가 저장되어있는 위치를 알기위한 GameObject

    [Space(20f)]
    [Header(" [ Other ]")]

    [SerializeField] List<GameObject> tuTowers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (tuInstance == null)
        {
            tuInstance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        tuTowerColorUpgradeGold = 10;
        for (int tu0 = 0; tu0 < 5; tu0++)
        {
            tuTowerColorUpgradeLevel[tu0] = 1;
        }
        tuUpgradeGold.GetComponent<TextMeshProUGUI>().text = "업그레이드 비용 : " + tuTowerColorUpgradeGold;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TowerColorUpgrade(int colorindex)
    {
        tuTowers.Clear();
        for (int i = 0; i < tuTowerParent.transform.childCount; i++) // Parent 밑에있는 타워들을 tuTowers 리스트에 모두 추가
        {
            tuTowers.Add(tuTowerParent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < tuTowers.Count; i++)
        {
            tuTowerKindCount[tuTowers[i].GetComponent<Tower>().twrType] += 1;
            tuTowerColorCount[tuTowers[i].GetComponent<Tower>().twrColor] += 1;
        }

        if (GameManager.gmInstance.BuyCheckGold(tuTowerColorUpgradeGold)) // 업그레이드에 필요한 골드량 만큼 있는지 체크후 업그레이드 진행.
        {
            tuTowerColorUpgradeLevel[colorindex] += 1;
            tuTowerColorTotalUpgradeCount += 1;
            tuTowerColorUpgradeGold = tuTowerColorTotalUpgradeCount + 10;
            GameManager.gmInstance.InfoMessage(1, tuTowerUpgradeUI[colorindex], " 업그레이드 완료 !! ");
            tuTowerUpgradeUI[colorindex].GetComponent<Button>().interactable = false;
            tuTowerUpgradeUI[colorindex].transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f, 1, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {

            tuTowerUpgradeUI[colorindex].GetComponent<Button>().interactable = true;
        });
        }
        else
        {
            GameManager.gmInstance.InfoMessage(0, tuTowerUpgradeUI[colorindex], " 돈이 부족합니다 !! ");
            tuTowerUpgradeUI[colorindex].GetComponent<Button>().interactable = false;
            tuTowerUpgradeUI[colorindex].transform.DOShakePosition(0.5f, new Vector3(10f, 1f, 10f), 5, 20, false, true).SetEase(Ease.OutQuad).OnComplete(() =>
        {

            tuTowerUpgradeUI[colorindex].GetComponent<Button>().interactable = true;
        });
            //DOPunchRotation(new Vector3(0, 0, 10), 0.5f, 30, 90).SetEase(Ease.OutQuad)
        }

        for (int i = 0; i < tuTowers.Count; i++) // 업그레이드 완료후 모든 타워의 데이터를 업데이트 시킴.
        {
            tuTowers[i].GetComponent<Tower>().TowerUpdate();
        }

        for (int i = 0; i < 5; i++) // 색상별 타워 업그레이드 횟수 표시.
        {
            tuTowerLevel[i].GetComponent<TextMeshProUGUI>().text = "" + tuTowerColorUpgradeLevel[i];
        }

        tuUpgradeGold.GetComponent<TextMeshProUGUI>().text = "업그레이드 비용 : " + tuTowerColorUpgradeGold;
    }
}
