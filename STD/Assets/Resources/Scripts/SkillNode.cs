using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SkillNode : MonoBehaviour
{
    /// <summary> 
    /// <para>0 : Darkness, </para>
    /// <para>1 : Flame, </para>
    /// <para>2 : Heart, </para>
    /// <para>3 : Ice, </para>
    /// <para>4 : Leaves, </para>
    /// <para>5 : Light, </para>
    /// <para>6 : Lightning, </para>
    /// <para>7 : Moon, </para>
    /// <para>8 : Soil, </para>
    /// <para>9 : Sun, </para>
    /// <para>10 : Water, </para>
    /// <para>11 : Wind. </para>
    ///</summary>
    [Tooltip(" 타워 속성 부여 / 0 : Darkness, 1 : Flame, 2 : Heart, 3 : Ice, 4 : Leaves, 5 : Light, 6 : Lightning, 7 : Moon, 8 : Soil, 9 : Sun, 10 : Water, 11 : Wind")]
    [Header(" [ Int ]")]
    public int snIdentity;
    /// <summary> 노드 인덱스. </summary>
    [Tooltip("노드 인덱스.")]
    public int snNodeIndex;
    /// <summary> 노드 타입   0 : 코어, 1 : 서브, 2 : 패시브. </summary>
    [Tooltip(" 노드 타입   0 : 코어, 1 : 서브, 2 : 패시브 ")]
    public int snNodeType;

    /// <summary> 최대 업그레이드 횟수 </summary>
    public int snMaxNodeUpgradeCount;
    /// <summary> 현재 업그레이드 횟수 </summary>
    public int snCurNodeUpgradeCount;

    /// <summary> 몬스터 경험치 기반 레벨의 최대 레벨 증가. </summary>
    [Tooltip("몬스터 경험치 기반 레벨의 최대 레벨 증가.")]
    public int snMaxLevelIncrease;
    /// <summary> 골드 업그레이드 레벨의 최대 레벨 증가. </summary>
    [Tooltip("골드 업그레이드 레벨의 최대 레벨 증가.")]
    public int snMaxUpgradeLevelIncrease;

    /// <summary> 공격력 증가 </summary>
    [Space(20f)]
    [Header(" [ Float ]")]
    public float snAtkIncrease;
    /// <summary> 사거리 증가 </summary>
    public float snRangeIncrease;
    /// <summary> 공격속도 증가 </summary>
    public float snAtkSpeedIncrease;
    /// <summary> 획득 경험치 증가 </summary>
    public float snExpIncrease;
    /// <summary> 타워 업그레이드 골드 요구량 감소. </summary>
    public float snUpgradeGoldDecrease;

    /// <summary> 타워 업그레이드에 필요한 재화. </summary>
    [Tooltip(" 타워 업그레이드에 필요한 재화. ")]
    public float[] snUpgradeNeedValue;
    [Space(20f)]
    [Header(" [ String ]")]
    public string[] snNodeTooltip;


    [Space(20f)]
    [Header(" [ Ohter ]")]
    public Slider[] snSlider;
    public GameObject[] snSliderText;
    // Start is called before the first frame update
    void Start()
    {
        NodeUIUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary> 노드 업그레이드 함수. </summary>
    public void NodeUpgrade()
    {
        if (snCurNodeUpgradeCount < snMaxNodeUpgradeCount)
        {
            int count = snUpgradeNeedValue.Length;
            // 플레이어 재화와 비교. // 

            if (TowerSkillTreeManager.tstmInstance.NormalTowerCheck(snNodeIndex))
            {
                snCurNodeUpgradeCount += 1;
                if (snCurNodeUpgradeCount == snMaxNodeUpgradeCount)
                {
                    TowerSkillTreeManager.tstmInstance.NormalTowerUpdate(snNodeIndex);
                }
                NodeUIUpdate();
                Debug.Log("성공" + snNodeIndex);
            }
        }

    }

    /// <summary> 노드 선택시 매니져에 노드 정보를 보낸다. </summary>
    public void NodeSelect()
    {
        TowerSkillTreeManager.tstmInstance.tstCurNode = this.gameObject;
        TowerSkillTreeManager.tstmInstance.tstCurNodeIndex = snNodeIndex;
        TowerSkillTreeManager.tstmInstance.UIInfoUpdate();
    }

    /// <summary> 노드 UI 업데이트 함수. </summary>
    void NodeUIUpdate()
    {
        for (int i = 0; i < snSlider.Length; i++)
        {
            snSlider[i].maxValue = snMaxNodeUpgradeCount;
            snSlider[i].value = snCurNodeUpgradeCount;
        }
        for (int j = 0; j < snSliderText.Length; j++)
        {
            snSliderText[j].GetComponent<TextMeshProUGUI>().text = "[ " + snCurNodeUpgradeCount + " <#557190>/ " + snMaxNodeUpgradeCount + " ]";
        }
    }
}
