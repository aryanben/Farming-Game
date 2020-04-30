using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList;

    public static Inventory instance;
    public Inventory()
    {
        itemList = new List<Item>();
    }

    // Update is called once per frame
    void Awake()
    {
        instance = this;
        
    }
    private void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddItem(Item.typeEnum.Wood, 5);

            for (int i = 0; i < itemList.Count; i++)
            {
               
                Debug.Log(itemList[i].type.ToString() + " = " + itemList[i].amount);
                
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AddItem(Item.typeEnum.Stone, 5);
            for (int i = 0; i < itemList.Count; i++)
            {
               
                Debug.Log(itemList[i].type.ToString() + " = " + itemList[i].amount);
            }
        }
    }
    public void AddItem(Item.typeEnum addType, int addAmount)
    {
        bool contains=false;
        int container=0;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].type == addType)
            {
                contains = true;
                container = i;
                break;
            }

        }
        if (contains)
        {
            itemList[container].amount += addAmount;
        }
        else
        {
            Item adder = new Item();
            adder.type = addType;
            adder.amount = addAmount;
            itemList.Add(adder);
        }
        GameObject.Find("YellowBorder").GetComponent<YellowBorderController>().ItemAddCheck();

    }
}
