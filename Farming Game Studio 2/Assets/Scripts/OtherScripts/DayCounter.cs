using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    public static bool dayIncreased;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Night"))
        {
            GameManager.day++;
            dayIncreased = true;
        }
    }

    private void Update()
    {
        if (dayIncreased)
        {
            dayIncreased = false;
        }
    }
}
