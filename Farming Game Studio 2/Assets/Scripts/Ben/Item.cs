using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum typeEnum
    {
        Wood,
        Stone,
        Seed
    }
    public typeEnum type;
    public int amount;
}
