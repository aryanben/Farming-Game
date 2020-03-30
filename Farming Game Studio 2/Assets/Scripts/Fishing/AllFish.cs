using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllFish : MonoBehaviour
{
    public static AllFish instance;
    public GameObject fish;
    public int sizeOfTank = 2;
    static int fishNumber = 75;
    public static GameObject[] allFish = new GameObject[fishNumber];
    public static Vector3 goalPos = Vector3.zero;
    void Start()
    {
        instance = this;
        for (int i = 0; i < fishNumber; i++)
        {
            Vector3 position = new Vector3(Random.Range(-sizeOfTank, sizeOfTank), Random.Range(-sizeOfTank, sizeOfTank), Random.Range(-sizeOfTank, sizeOfTank));
            allFish[i] = Instantiate(fish, position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 15500) < 50)
        {
            goalPos = new Vector3(Random.Range(-sizeOfTank, sizeOfTank), Random.Range(-sizeOfTank, sizeOfTank), Random.Range(-sizeOfTank, sizeOfTank));
        }
    }
}
