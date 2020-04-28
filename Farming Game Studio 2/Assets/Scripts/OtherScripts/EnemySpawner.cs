using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnStrongSpiderPoint;
    public Transform[] spawnWeakSpiderPoint;
    public Transform[] spawnPointSlime;
    public GameObject[] strongSpider;
    public GameObject[] weakSpider;
    public GameObject[] slime;

    int strongSpiderMonster;
    int weakSpiderMonster;
    int slimeMonster;

    public bool spawnAllowed;

    private void Start()
    {
        spawnAllowed = true;
        Invoke("SpawnMonster", 0);
    }
    private void Update()
    {
        if(strongSpider == null && slime == null && weakSpider == null)
        {
            Invoke("SpawnMonster", 0);
        }
    }
    private void SpawnMonster()
    {
        if (spawnAllowed)
        {
            strongSpiderMonster =  strongSpider.Length - 1;
            weakSpiderMonster =  weakSpider.Length - 1;

            slimeMonster = Random.Range(0, slime.Length);

            for (int i = 0; i < spawnWeakSpiderPoint.Length; i++)
            {
                Instantiate(weakSpider[weakSpiderMonster], spawnWeakSpiderPoint[i].position, Quaternion.identity);               
            }
            for (int i = 0; i < spawnStrongSpiderPoint.Length; i++)
            {
                Instantiate(strongSpider[strongSpiderMonster], spawnStrongSpiderPoint[i].position, Quaternion.identity);
            }

            for (int i = 0; i < spawnPointSlime.Length; i++)
            {
                Instantiate(slime[slimeMonster], spawnPointSlime[i].position, Quaternion.identity);
            }
        }
    }
}
