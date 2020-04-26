﻿using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    bool isGrounded;
    public GameObject smoke;
    public GameObject introText;

    bool waitForSecondsBoolForIntroText;
    float waitForSecondsFloatForIntroText = 5;

    bool canPlayExplosion;

    public AudioSource wind;
    public AudioSource explosion;
    public AudioSource line1AI;
    int playExplosionOnce = 0;
    Rigidbody rb;


    public Camera introCamera;
    public Camera MainCamera;

    public float switchOnMainCamera;
    private void Start()
    {
        wind.Play();
        rb = GetComponent<Rigidbody>();
        MainCamera.enabled = false;
    }
    void Update()
    {
        switchOnMainCamera -= Time.deltaTime;

        if (switchOnMainCamera <= 0)
        {
            MainCamera.enabled = true;
            introCamera.enabled = false;
        }

        if (waitForSecondsBoolForIntroText)
        {
            waitForSecondsFloatForIntroText -= Time.deltaTime;

            if (waitForSecondsFloatForIntroText <= 0)
            {
                line1AI.Play();
                introText.SetActive(true);
                waitForSecondsBoolForIntroText = false;
            }
        }


        if (canPlayExplosion)
        {
            explosion.Play();
            canPlayExplosion = false;
        }

        if (playExplosionOnce == 1)
        {
            canPlayExplosion = true;
        }

        if (IntroText.chatBox == 0)
        {
            //Play Voice For Chat one
        }

        if (IntroText.chatBox == 1)
        {
            SceneManager.LoadScene("Odera's Scene");
        }
    }


    void PointOfContact()
    {
        Destroy(wind);
        playExplosionOnce++;
        smoke.SetActive(true);
        TimeForIntroText();
    }

    void TimeForIntroText()
    {
        waitForSecondsBoolForIntroText = true;
    }

}
