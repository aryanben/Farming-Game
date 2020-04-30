using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject tutorialTree;
    public GameObject tutorialTreePoint;

    public GameObject trees;
    public GameObject stones;

    public GameObject[] treeSpawnPositions;
    public GameObject[] stoneSpawnPositions;

    public LayerMask treeMask;
    public LayerMask stoneMask;
    void Start()
    {
        Instantiate(tutorialTree, tutorialTreePoint.transform.position, Quaternion.identity);
        for (int i = 0; i < treeSpawnPositions.Length; i++)
        {
            Instantiate(trees, treeSpawnPositions[i].transform.position, Quaternion.identity);
        }

        for (int i = 0; i < stoneSpawnPositions.Length; i++)
        {
            Instantiate(stones, stoneSpawnPositions[i].transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (DayCounter.dayIncreased && Interactable.amountOfTrees < 22) 
        {            
            for (int i = 0; i < treeSpawnPositions.Length; i++)
            {              
                if (Physics.CheckSphere(treeSpawnPositions[i].transform.position, 10, treeMask))
                {
                    continue;
                }
                else
                {
                    Instantiate(trees, treeSpawnPositions[i].transform.position, Quaternion.identity);
                }               
            }
            for (int i = 0; i < stoneSpawnPositions.Length; i++)
            {
                if (Physics.CheckSphere(stoneSpawnPositions[i].transform.position, 10, stoneMask))
                {
                    continue;
                }
                else
                {
                    Instantiate(stones, stoneSpawnPositions[i].transform.position, Quaternion.identity);
                }              
            }
        }
    }
}