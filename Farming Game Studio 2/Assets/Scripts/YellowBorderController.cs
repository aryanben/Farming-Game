using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowBorderController : MonoBehaviour
{
    public GameObject slot1, slot2, slot3, slot4, slot5, slot6, slot7;
    int scrollNumber;
    bool canScroll;

    public Image yellowBorder;

    private void Start()
    {
        yellowBorder.enabled = false;
    }
    void Update()
    {
        transform.position = this.transform.position;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot1.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot2.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot3.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot4.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot5.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot6.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot7.transform.position;
        }
    }

    public void Button(int index)
    {
        if (index == 1)
        {
            yellowBorder.enabled = true;
            this.transform.position = slot1.transform.position;
        }
        if (index == 2)
        {
            yellowBorder.enabled = true;
            this.transform.position = slot2.transform.position;
        }
        if (index == 3)
        {
            yellowBorder.enabled = true;
            this.transform.position = slot3.transform.position;
        }
        if (index == 4)
        {
            yellowBorder.enabled = true;
            this.transform.position = slot4.transform.position;
        }
        if (index == 5)
        {
            yellowBorder.enabled = true;
            this.transform.position = slot5.transform.position;
        }
        if (index == 6)
        {
            yellowBorder.enabled = true;
            this.transform.position = slot6.transform.position;
        }
        if (index == 7)
        {
            yellowBorder.enabled = true;
            this.transform.position = slot7.transform.position;
        }
    }

    public void Scroll()
    {
        canScroll = true;

        if (canScroll)
        {           
            if (Input.GetAxis("Mouse ScrollWheel") > 0) 
            {
                scrollNumber++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                scrollNumber--;
            }

            if (scrollNumber <= 1)
            {
                yellowBorder.enabled = true;
                this.transform.position = slot1.transform.position;
            }
            if (scrollNumber == 2)
            {
                yellowBorder.enabled = true;
                this.transform.position = slot2.transform.position;
            }
            if (scrollNumber == 3)
            {
                yellowBorder.enabled = true;
                this.transform.position = slot3.transform.position;
            }
            if (scrollNumber == 4)
            {
                yellowBorder.enabled = true;
                this.transform.position = slot4.transform.position;
            }
            if (scrollNumber == 5)
            {
                yellowBorder.enabled = true;
                this.transform.position = slot5.transform.position;
            }
            if (scrollNumber == 6)
            {
                yellowBorder.enabled = true;
                this.transform.position = slot6.transform.position;
            }
            if (scrollNumber >= 7)
            {
                yellowBorder.enabled = true;
                this.transform.position = slot7.transform.position;
            }
        }
        canScroll = false;
    }
}
