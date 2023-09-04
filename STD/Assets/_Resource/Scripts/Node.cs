using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header(" [ int ]")]
    public int ndNodeIndex; // 타워 노드 인덱스
    public int ndNodeIdentity;
    [Space(20f)]
    [Header(" [ GameObject ]")]
    public GameObject ndCurTower; // 노드에 있는 타워저장하는 변수.
    // Start is called before the first frame update
    void Start()
    {
        ndCurTower = null;

        switch (ndNodeIdentity)
        {
            case 1:
                ndCurTower.GetComponent<Tower>().NodeIdentityI();
                break;
            case 2:
                ndCurTower.GetComponent<Tower>().NodeIdentityII();
                break;
            case 3:
                ndCurTower.GetComponent<Tower>().NodeIdentityIII();
                break;
            case 4:
                ndCurTower.GetComponent<Tower>().NodeIdentityIV();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
