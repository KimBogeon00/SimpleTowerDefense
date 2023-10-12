using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
    // 타워의 속성을 정할수있는 속성 노드가 총 4개 있다.
    // 일정 조건이 만족될때 코어 노드 업그레이드를 할수있다.
*/

public class TowerSkillTreeManager : MonoBehaviour
{
    public static TowerSkillTreeManager tstmInstance;
    public GameObject tsNodeInfoUI;
    [Space(10f)]

    public GameObject tstCurNode;
    public int tstCurNodeIndex;
    public GameObject tstNodeLevel;
    public GameObject[] tstNodeToolTip;
    [Tooltip("0은 배경 이미지, 1은 텍스트. ")]
    public GameObject[] tstNodeType;
    [Tooltip("Type 배경에 넣을 이미지들. ")]
    public Sprite[] tstNodeTypeSprite;

    [Space(20f)]
    [Header(" [ Normal Tower ]")]
    public GameObject[] tstNormalElementNode; // 노말 타워 속성
    public GameObject[] tstNormalCoreNode; // 노말 타워 코어
    /// <summary>
    /// <para> 0 : Core1, 1 : Core2 </para>
    /// </summary>
    public bool[] tstNormalCoreNodeCheck; // 노말 타워 코어 업그레이드 체크
    public GameObject[] tstNormalSubNode; // 노말 타워 서브
    /// <summary>
    /// <para> 0 : Sub1, 1 : Sub2, 2 : Sub3 </para>
    /// <para> 3 : Sub1-1, 4 : Sub2-1, 5 : Sub3-1 </para>
    /// <para> 6 : Sub4, 7 : Sub5, 8 : Sub4-1 </para>
    /// <para> 9 : Sub5-1 </para>
    /// </summary>
    public bool[] tstNormalSubNodeCheck; // 노말 타워 서브 업그레이드 체크
    public GameObject[] tstNormalPassiveNode; // 노말 타워 패시브
    public bool[] tstNormalPassiveNodeCheck; // 노말 타워 패시브 업그레이드 체크


    // Start is called before the first frame update
    void Start()
    {
        if (tstmInstance == null)
        {
            tstmInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NodeUpgrades()
    {
        tstCurNode.GetComponent<SkillNode>().NodeUpgrade();
        UIInfoUpdate();
    }

    /// <summary> 노드 정보 UI 업데이트 하기. </summary>
    public void UIInfoUpdate()
    {
        if (tsNodeInfoUI.activeSelf == false)
        {
            tsNodeInfoUI.SetActive(true);
        }
        switch (tstCurNode.GetComponent<SkillNode>().snNodeType)
        {
            case 0:
                tstNodeType[0].GetComponent<Image>().sprite = tstNodeTypeSprite[0];
                tstNodeType[1].GetComponent<TextMeshProUGUI>().text = "Core";
                break;
            case 1:
                tstNodeType[0].GetComponent<Image>().sprite = tstNodeTypeSprite[1];
                tstNodeType[1].GetComponent<TextMeshProUGUI>().text = "Sub";
                break;
            case 2:
                tstNodeType[0].GetComponent<Image>().sprite = tstNodeTypeSprite[2];
                tstNodeType[1].GetComponent<TextMeshProUGUI>().text = "Passive";
                break;
            default:
                break;
        }
        for (int j = 0; j < tstNodeToolTip.Length; j++)
        {
            tstNodeToolTip[j].SetActive(false);
        }
        for (int i = 0; i < tstCurNode.GetComponent<SkillNode>().snNodeTooltip.Length; i++)
        {
            tstNodeToolTip[i].SetActive(true);
            tstNodeToolTip[i].GetComponent<TextMeshProUGUI>().text = "" + tstCurNode.GetComponent<SkillNode>().snNodeTooltip[i];
        }
        tstNodeLevel.GetComponent<TextMeshProUGUI>().text = "" + tstCurNode.GetComponent<SkillNode>().snCurNodeUpgradeCount;
    }

    /// <summary> 이전 노드가 업그레이드 되었는지 확인하기위한 함수 </summary>
    /// <param name="index"> 타워 노드의 index 값을 통해 확인한다. </param>
    public bool NormalTowerCheck(int index)
    {
        switch (index)
        {
            case 1005:
                return true;
            case 1006:
                if (tstNormalCoreNodeCheck[0] == true)
                    return true;
                break;
            case 1007:
                if (tstNormalCoreNodeCheck[0] == true)
                    return true;
                break;
            case 1008:
                if (tstNormalCoreNodeCheck[0] == true)
                    return true;
                break;
            case 1009:
                if (tstNormalSubNodeCheck[0] == true)
                    return true;
                break;
            case 1010:
                if (tstNormalSubNodeCheck[1] == true)
                    return true;
                break;
            case 1011:
                if (tstNormalSubNodeCheck[2] == true)
                    return true;
                break;
            case 1012:
                if (tstNormalSubNodeCheck[3] == true && tstNormalSubNodeCheck[4] == true && tstNormalSubNodeCheck[5] == true)
                    return true;
                break;
            case 1013:
                if (tstNormalCoreNodeCheck[1] == true)
                    return true;
                break;
            case 1014:
                if (tstNormalCoreNodeCheck[1] == true)
                    return true;
                break;
            case 1015:
                if (tstNormalSubNodeCheck[6] == true)
                    return true;
                break;
            case 1016:
                if (tstNormalSubNodeCheck[7] == true)
                    return true;
                break;
            default:
                break;
        }
        return false;
    }

    public void NormalTowerUpdate(int index)
    {
        switch (index)
        {
            case 1005:
                tstNormalCoreNodeCheck[0] = true;
                break;
            case 1006:
                tstNormalSubNodeCheck[0] = true;
                break;
            case 1007:
                tstNormalSubNodeCheck[1] = true;
                break;
            case 1008:
                tstNormalSubNodeCheck[2] = true;
                break;
            case 1009:
                tstNormalSubNodeCheck[3] = true;
                break;
            case 1010:
                tstNormalSubNodeCheck[4] = true;
                break;
            case 1011:
                tstNormalSubNodeCheck[5] = true;
                break;
            case 1012:
                tstNormalCoreNodeCheck[1] = true;
                break;
            case 1013:
                tstNormalSubNodeCheck[6] = true;
                break;
            case 1014:
                tstNormalSubNodeCheck[7] = true;
                break;
            case 1015:
                tstNormalSubNodeCheck[8] = true;
                break;
            case 1016:
                tstNormalSubNodeCheck[9] = true;
                break;
            default:
                break;
        }
    }




}
