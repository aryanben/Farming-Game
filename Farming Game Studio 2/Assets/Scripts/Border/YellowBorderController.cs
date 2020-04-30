using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowBorderController : MonoBehaviour
{
    public GameObject slot1, slot2, slot3, slot4, slot5, slot6, slot7;
    int scrollNumber;
    bool canScroll;
    public static int currentSlotIndex;
    public Image yellowBorder;

    public void ItemAddCheck()
    {

        for (int i = 0; i < Inventory.instance.itemList.Count; i++)
        {
            if (Inventory.instance.itemList[i].type == Item.typeEnum.Wood)
            {
                if (Inventory.instance.itemList[i].amount > 0)
                {
                    slot1.transform.GetChild(0).gameObject.SetActive(true);
                    slot1.transform.GetChild(1).gameObject.SetActive(true);
                    slot1.transform.GetChild(1).gameObject.GetComponent<Text>().text = Inventory.instance.itemList[i].amount.ToString();

                }
                else
                {
                    slot1.transform.GetChild(0).gameObject.SetActive(false);
                    slot1.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else if (Inventory.instance.itemList[i].type == Item.typeEnum.Stone)
            {
                if (Inventory.instance.itemList[i].amount > 0)
                {
                    slot2.transform.GetChild(0).gameObject.SetActive(true);
                    slot2.transform.GetChild(1).gameObject.SetActive(true);
                    slot2.transform.GetChild(1).gameObject.GetComponent<Text>().text = Inventory.instance.itemList[i].amount.ToString();

                }
                else
                {
                    slot2.transform.GetChild(0).gameObject.SetActive(false);
                    slot2.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else if (Inventory.instance.itemList[i].type == Item.typeEnum.CarrotSeed)
            {
                if (Inventory.instance.itemList[i].amount > 0)
                {
                    slot3.transform.GetChild(0).gameObject.SetActive(true);
                    slot3.transform.GetChild(1).gameObject.SetActive(true);
                    slot3.transform.GetChild(1).gameObject.GetComponent<Text>().text = Inventory.instance.itemList[i].amount.ToString();

                }
                else
                {
                    slot3.transform.GetChild(0).gameObject.SetActive(false);
                    slot3.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else if (Inventory.instance.itemList[i].type == Item.typeEnum.TomatoSeed)
            {
                if (Inventory.instance.itemList[i].amount > 0)
                {
                    slot4.transform.GetChild(0).gameObject.SetActive(true);
                    slot4.transform.GetChild(1).gameObject.SetActive(true);
                    slot4.transform.GetChild(1).gameObject.GetComponent<Text>().text = Inventory.instance.itemList[i].amount.ToString();

                }
                else
                {
                    slot4.transform.GetChild(0).gameObject.SetActive(false);
                    slot4.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else if (Inventory.instance.itemList[i].type == Item.typeEnum.Carrot)
            {
                if (Inventory.instance.itemList[i].amount > 0)
                {
                    slot5.transform.GetChild(0).gameObject.SetActive(true);
                    slot5.transform.GetChild(1).gameObject.SetActive(true);
                    slot5.transform.GetChild(1).gameObject.GetComponent<Text>().text = Inventory.instance.itemList[i].amount.ToString();

                }
                else
                {
                    slot5.transform.GetChild(0).gameObject.SetActive(false);
                    slot5.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else if (Inventory.instance.itemList[i].type == Item.typeEnum.Tomato)
            {
                if (Inventory.instance.itemList[i].amount > 0)
                {
                    slot6.transform.GetChild(0).gameObject.SetActive(true);
                    slot6.transform.GetChild(1).gameObject.SetActive(true);
                    slot6.transform.GetChild(1).gameObject.GetComponent<Text>().text = Inventory.instance.itemList[i].amount.ToString();

                }
                else
                {
                    slot6.transform.GetChild(0).gameObject.SetActive(false);
                    slot6.transform.GetChild(1).gameObject.SetActive(false);
                }
            }

        }
    }

    private void Start()
    {

        yellowBorder.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Inventory.instance.AddItem(Item.typeEnum.Stone, 1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Inventory.instance.AddItem(Item.typeEnum.Carrot, 1);
        }
        transform.position = this.transform.position;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot1.transform.position;
            currentSlotIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot2.transform.position;
            currentSlotIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot3.transform.position;
            currentSlotIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot4.transform.position;
            currentSlotIndex = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot5.transform.position;
            currentSlotIndex = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot6.transform.position;
            currentSlotIndex = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            yellowBorder.enabled = true;
            this.transform.position = slot7.transform.position;
            currentSlotIndex = 7;
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
        currentSlotIndex = index;
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
                scrollNumber = 1;

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
                scrollNumber = 7;
            }
            currentSlotIndex = scrollNumber;
        }
        canScroll = false;
    }
}
