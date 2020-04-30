using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum typeEnum
    {
        Wood,
        Stone,
        CarrotSeed,
        TomatoSeed,
        Carrot,
        Tomato
        
    }
    public typeEnum type;
    public int amount;
}
