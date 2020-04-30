using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : ScriptableObject
{
    public List<Crop> myObservers;
    public string subjectName;

   

    public void UpdateObservers() 
    {
        for (int i = 0; i < myObservers.Count; i++)
        {
            myObservers[i].TickUpdate();
        }
    }
}
