using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingCrops : MonoBehaviour
{
    public LayerMask planeLayerMask;
    void Start()
    {
        Inventory.instance.AddItem(Item.typeEnum.Carrot, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Health.Instance.TakeDamage(10);
        }
        if (YellowBorderController.currentSlotIndex == 5)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                bool contains = false;
                int container = 0;
                for (int i = 0; i < Inventory.instance.itemList.Count; i++)
                {
                    if (Inventory.instance.itemList[i].type == Item.typeEnum.Carrot)
                    {
                        if (Inventory.instance.itemList[i].amount > 0)
                        {
                            contains = true;
                            container = i;
                            break;
                        }
                    }

                }
                if (contains)
                {
                    Inventory.instance.AddItem(Item.typeEnum.Carrot, -1);
                    Health.Instance.TakeDamage(-5);
                }
              
            }
            
        }
        else if (YellowBorderController.currentSlotIndex == 6)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                bool contains = false;
                int container = 0;
                for (int i = 0; i < Inventory.instance.itemList.Count; i++)
                {
                    if (Inventory.instance.itemList[i].type == Item.typeEnum.Tomato)
                    {
                        if (Inventory.instance.itemList[i].amount > 0)
                        {
                            contains = true;
                            container = i;
                            break;
                        }
                    }

                }
                if (contains)
                {
                    Inventory.instance.AddItem(Item.typeEnum.Tomato, -1);
                    Health.Instance.TakeDamage(-5);
                }

            }
        }

        if (YellowBorderController.currentSlotIndex == 3)
        {
            bool contains = false;
            int container = 0;
            for (int i = 0; i < Inventory.instance.itemList.Count; i++)
            {
                if (Inventory.instance.itemList[i].type == Item.typeEnum.CarrotSeed)
                {
                    if (Inventory.instance.itemList[i].amount > 0)
                    {
                        contains = true;
                        container = i;
                        break;
                    }
                }

            }
            if (contains)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, planeLayerMask))
                    {
                        GameObject.Find("testobject").GetComponent<SubjectManager>().PlantCrop(hit.point, "Carrot");
                        Inventory.instance.itemList[container].amount--;
                        GameObject.Find("YellowBorder").GetComponent<YellowBorderController>().ItemAddCheck();
                    }
                }
            }
            else
            {
                Debug.Log("no carrot seed my freind");
            }
        }
        if (YellowBorderController.currentSlotIndex == 4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bool contains = false;
                int container = 0;
                for (int i = 0; i < Inventory.instance.itemList.Count; i++)
                {
                    if (Inventory.instance.itemList[i].type == Item.typeEnum.TomatoSeed)
                    {
                        if (Inventory.instance.itemList[i].amount > 0)
                        {
                            contains = true;
                            container = i;
                            break;
                        }

                    }

                }
                if (contains)
                {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, planeLayerMask))
                    {
                        GameObject.Find("testobject").GetComponent<SubjectManager>().PlantCrop(hit.point, "Tomato");
                        Inventory.instance.itemList[container].amount--;
                        GameObject.Find("YellowBorder").GetComponent<YellowBorderController>().ItemAddCheck();
                    }

                }
                else
                {
                    Debug.Log("no carrot seed my freind");
                }
            }
        }
    }
}
