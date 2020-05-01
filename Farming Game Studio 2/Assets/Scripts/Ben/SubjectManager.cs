using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectManager : MonoBehaviour
{
    public Subject cropManager;
    public GameObject carrotPrefab;
    public GameObject tomatoPrefab;
    public float tickTime;
    public float tickSeconds;
    public LayerMask planeLayerMask;
    void Awake()
    {
        cropManager= ScriptableObject.CreateInstance<Subject>();
        cropManager.myObservers = new List<Crop>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //if (Input.GetKeyDown(KeyCode.T)) 
        //{
        //    cropManager.UpdateObservers();
        //}

        if (tickTime<=0)
        {
            cropManager.UpdateObservers();
            tickTime = tickSeconds;
        }
        tickTime -= Time.deltaTime;
    }
    public void AddCropToList(Crop crop) 
    {
        cropManager.myObservers.Add(crop);
    }

    public void PlantCrop(Vector3 position, string name)
    {

        if (name=="Tomato")
        {
             GameObject temp =  Instantiate(tomatoPrefab, position, Quaternion.identity);
             AddCropToList(temp.GetComponent<Crop>());
             temp.GetComponent<Crop>().CropName = "Tom";
        }
        else if (name == "Carrot")
        {
            GameObject temp = Instantiate(carrotPrefab, position, Quaternion.identity);
            AddCropToList(temp.GetComponent<Crop>());
            temp.GetComponent<Crop>().CropName = "Car";
        }
    }
}
