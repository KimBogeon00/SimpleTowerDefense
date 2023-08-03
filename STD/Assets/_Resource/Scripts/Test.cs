using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Target;
    public GameObject[] Obj = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void A()
    {
        GameObject a = Instantiate(Obj[Random.Range(0, 10)], this.transform.position, Quaternion.identity) as GameObject;
        a.gameObject.GetComponent<ProjectileMoveScript>().SetTargets(Target);
    }
}
