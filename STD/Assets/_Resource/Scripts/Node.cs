using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header(" [ int ]")]
    public int ndNodeIndex; // 타워 노드 인덱스
    [Space(20f)]
    [Header(" [ GameObject ]")]
    public GameObject ndCurTower; // 노드에 있는 타워저장하는 변수.
    // Start is called before the first frame update
    void Start()
    {
        ndCurTower = null;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
