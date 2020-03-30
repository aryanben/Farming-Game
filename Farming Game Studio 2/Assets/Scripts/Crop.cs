using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public int currentDay;
    public int cropPlantDate;
    public int cropAge;
    public string CropName;
    bool watered;
    public int halfAge;

    public GameObject cropFruit;
    public GameObject cropPlantSmall;
    public GameObject cropPlantBig;
    public enum State
    {
        Planted,

        Ready

    }
    public State cropState;
    private void Update()
    {
        DayUpdate();
    }
    public void DayUpdate()
    {
        cropAge = currentDay - cropPlantDate;
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
            cropState = State.Ready;
            cropFruit.SetActive(true);
        }
    }
    public void Water() 
    {
        watered = true;
        //make soil darker
    }
    public void Harvest() 
    {
        if (cropState==State.Ready)
        {

        }
    }


}
