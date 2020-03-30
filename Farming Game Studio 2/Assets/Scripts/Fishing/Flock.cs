using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public float speed = 0.01f;
    public float rotationSpeed = 4.0f;
    public float neighbourDistance = 3.0f;
    bool turning = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= AllFish.instance.sizeOfTank)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            speed = Random.Range(.5f, 1f);
        }
        else 
        {
            if (Random.Range(0, 5) < 1)
            {
                ApplyRules();
            }               
        }
        transform.Translate(0, 0, Time.deltaTime * speed);


    }

    void ApplyRules()
    {
        GameObject[] allFishes;
        allFishes = AllFish.allFish;

        Vector3 center = Vector3.zero;
        Vector3 avoid = Vector3.zero;
        float groupSpeed = 0.1f;

        Vector3 goalPos = AllFish.goalPos;

        float dist;
      
        int groupSize = 0;

        foreach (GameObject fish in allFishes)
        {
            if (fish != this.gameObject)
            {               
                dist = Vector3.Distance(fish.transform.position, this.transform.position);
                if (dist <= neighbourDistance)
                {
                    center += fish.transform.position;
                    groupSize++;

                    if (dist < .9f)
                    {
                        Debug.Log(dist);
                        avoid = avoid + (this.transform.position - fish.transform.position);
                    }

                    Flock anotherFlock = fish.GetComponent<Flock>();
                    groupSpeed += anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            center = center / groupSize + (goalPos - this.transform.position);
            speed = groupSpeed / groupSize;

            Vector3 direction = (center + avoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }

    }
}
