
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    // Update is called once per frame

    public static bool isNight;
    public static bool isDay;

    void Update()
    {
        transform.Rotate(1.2f * Time.deltaTime, 0, 0);
    }
}
