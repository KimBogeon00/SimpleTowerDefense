using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 몬스터의 HP 바를 고정시키기 위한 스크립트. </summary>
public class FixedSlider : MonoBehaviour
{
    [SerializeField] Transform fsCamera;
    // Start is called before the first frame update
    void Start()
    {
        fsCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + fsCamera.rotation * Vector3.forward, fsCamera.rotation * Vector3.up);
    }
}
