using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float rotation = 2.5f;
    public GameObject sun;
    public GameObject night;
    public static Sun instance;
    public static bool canRotate;

    private void Start()
    {
        canRotate = true;
        instance = this;
    }
    void Update()
    {
        if (canRotate)
        {
            transform.RotateAround(Vector3.zero, Vector3.right, rotation * Time.deltaTime);
            transform.LookAt(Vector3.zero);
        }  
    }
}
