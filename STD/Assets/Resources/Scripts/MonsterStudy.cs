using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MonsterStudy : MonoBehaviour
{
    public static MonsterStudy msInstance;

    /// <summary> 
    /// <para>0 RED  </para>
    /// <para>1 GREEN  </para>
    /// <para>2 BLUE  </para>
    /// <para>3 ORANGE  </para>
    /// <para>4 PURPLE  </para>
    /// <para> Max = 100  </para>
    ///</summary>
    public int[] msColorWeight = new int[5];
    public int msColorMax;
    public int msColorMin;
    public float msDeadTimeMax;
    public float msDeadTimeMin;
    public float[] msMonsterDeadTime = new float[5];
    public int[] msMonsterDeadCount = new int[5];

    [SerializeField] GameObject[] msMonsterWeightText = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        if (msInstance == null)
        {
            msInstance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        WeightReset();
    }

    // Update is called once per frame
    void Update()
    {
        for (int k = 0; k < 5; k++)
        {
            // 몬스터 가중치 출력 부분 //
            msMonsterWeightText[k].GetComponent<TextMeshProUGUI>().text = "" + msColorWeight[k] + " %";
        }

        if (StageManager.smInstance.smStageEnd)
        {
            for (int i = 0; i < 5; i++)
            {
                msMonsterDeadTime[i] = msMonsterDeadTime[i] / msMonsterDeadCount[i];
            }
            WeightUpdate();
        }
    }

    void WeightReset()
    {
        for (int i = 0; i < 5; i++)
        {
            msColorWeight[i] = 20;
        }
    }

    void WeightUpdate()
    {
        msDeadTimeMax = msMonsterDeadTime[0];
        msDeadTimeMin = msMonsterDeadTime[0];
        msColorMax = 0;
        msColorMin = 0;

        for (int i = 1; i < 5; i++)
        {
            if (msDeadTimeMax < msMonsterDeadTime[i])
            {
                msDeadTimeMax = msMonsterDeadTime[i];
                msColorMax = i;
            }

            if (msDeadTimeMin > msMonsterDeadTime[i])
            {
                msDeadTimeMin = msMonsterDeadTime[i];
                msColorMin = i;
            }
        }
        if (msColorWeight[msColorMax] > 5 && msColorWeight[msColorMin] < 40)
        {
            msColorWeight[msColorMax] -= 1;
            msMonsterWeightText[msColorMax].transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f, 1, 0.2f).SetEase(Ease.OutQuad);
            msColorWeight[msColorMin] += 1;
            msMonsterWeightText[msColorMin].transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f, 1, 0.2f).SetEase(Ease.OutQuad);

            /*RM_ErrorMessage.transform.DOPunchRotation(new Vector3(0, 0, 10), 0.5f, 30, 90).SetEase(Ease.OutQuad).OnComplete(() =>
        {

            RM_ErrorMessage.SetActive(false);
        });
        */
        }

        for (int i = 0; i < 5; i++)
        {
            msMonsterDeadTime[i] = 0;
            msMonsterDeadCount[i] = 0;
        }
        StageManager.smInstance.smStageEnd = false;
    }
}
