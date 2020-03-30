
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.7f * Time.deltaTime, 0, 0);
    }
}
