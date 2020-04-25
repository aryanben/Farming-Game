using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTGameManager : MonoBehaviour
{
    public static BTGameManager SharedInstance;

    public GameObject player;

    public AudioSource MusicSource;

    public int playerHealthMax;
    public int playerHealth;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        playerHealth = playerHealthMax;
    }

    void Update()
    {
        if (playerHealth <= 0)
        {
            player.GetComponent<Player>().anim.SetBool("IsDead", true);
        }
    }

    //public void SetDamage(int dmg, GameObject obj)
    //{
    //    switch (obj.name)
    //    {
    //        case "Player":

    //            playerHealth -= dmg;
    //            break;
    //        default:
    //            break;
    //    }
    //}
}