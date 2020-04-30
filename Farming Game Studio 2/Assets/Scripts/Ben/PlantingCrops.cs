using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingCrops : MonoBehaviour
{
    public LayerMask planeLayerMask;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (YellowBorderController.currentSlotIndex == 3)
        {
            bool contains = false;
            int container = 0;
            for (int i = 0; i < Inventory.instance.itemList.Count; i++)
            {
                if (Inventory.instance.itemList[i].type == Item.typeEnum.CarrotSeed)
                {
                    contains = true;
                    container = i;
                    break;
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
                        contains = true;
                        container = i;
                        break;
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
