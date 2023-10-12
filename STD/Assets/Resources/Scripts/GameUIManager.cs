using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager gumInstance;
    [Header(" [ GameObject ]")]
    public GameObject gumPlayerGold;

    [Space(10f)]
    [Header(" [ GameObject UI ]")]
    [SerializeField] GameObject gumBossUI;
    [SerializeField] GameObject gumTowerBuyUI;
    [SerializeField] GameObject gumNodeBuyUI;

    // Start is called before the first frame update
    void Start()
    {
        if (gumInstance == null)
        {
            gumInstance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        gumPlayerGold.GetComponent<TextMeshProUGUI>().text = GameManager.gmInstance.gmGold + "";
    }

    public void BossUIOn()
    {
        Sequence bossOnSequence;
        gumBossUI.SetActive(true);
        bossOnSequence = DOTween.Sequence().SetAutoKill(false)
        .Append(gumBossUI.transform.DOLocalMove(new Vector3(0, -450, 0), 1.0f).SetEase(Ease.OutQuad).From(new Vector3(0, 1000, 0)));

    }
    public void BossUIOff()
    {
        Sequence bossOffSequence;
        bossOffSequence = DOTween.Sequence()
        .Append(gumBossUI.transform.DOLocalMove(new Vector3(1600, -450, 0), 0.5f).SetEase(Ease.OutQuad))
        .OnComplete(() =>
        {
            gumBossUI.SetActive(false);
        });
    }

    public void TowerUIOnOff() // 타워 구매 UI 제어
    {
        if (gumTowerBuyUI.activeSelf)
        {
            gumTowerBuyUI.SetActive(false);
        }
        else if (!gumTowerBuyUI.activeSelf)
        {
            gumTowerBuyUI.SetActive(true);
            gumNodeBuyUI.SetActive(false);
        }
    }

    public void NodeUIOnOff() // 타워 구매 UI 제어
    {
        if (gumNodeBuyUI.activeSelf)
        {
            gumNodeBuyUI.SetActive(false);
        }
        else if (!gumNodeBuyUI.activeSelf)
        {
            gumNodeBuyUI.SetActive(true);
            gumTowerBuyUI.SetActive(false);
        }
    }
}
