using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    
    
    public int cropAge;
    public string CropName;
    bool watered;
    
    public int halfAge;

    public GameObject cropFruit;
    public GameObject cropPlantSmall;
    public GameObject cropPlantBig;
    public bool ready;
    
    private void Awake()
    {
        ready = false;
        watered = true;
        cropAge = 0;
        
    }
   
    public void TickUpdate()
    {
        
        cropAge++;
        if (!watered)
        {
            cropAge--;
          
        }
        if (cropAge >= halfAge)
        {
            cropPlantSmall.SetActive(false);
            cropPlantBig.SetActive(true);

        }
        if (cropAge >= (halfAge * 2))
        {
            ready = true;
            cropFruit.SetActive(true);
        }
    }
    public void Water() 
    {
        watered = true;
      
        //make soil darker
    }
    public void UnWater()
    {
        watered = false;

    }
    public void Harvest() 
    {
        if (ready)
        {
            if (CropName=="Tom")
            {
                Inventory.instance.AddItem(Item.typeEnum.Tomato, 3);
                Inventory.instance.AddItem(Item.typeEnum.TomatoSeed, 2);
                Destroy(gameObject);
            }
            else if (CropName == "Car")
            {
                Inventory.instance.AddItem(Item.typeEnum.Carrot, 3);
                Inventory.instance.AddItem(Item.typeEnum.CarrotSeed, 2);
                Destroy(gameObject);
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Sword")
        {
            Harvest();
        }
    }

}
