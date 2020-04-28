using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
   
    public GameObject line2;
    public GameObject line3;
    public GameObject line4;
    public GameObject line5;
    public GameObject bewareSign;
    //AudioSources
    public AudioSource line2Audio;
    public AudioSource line3Audio;
    public AudioSource line4Audio;
    public AudioSource line5Audio;

    private void Start()
    {
        line2.SetActive(true);
    }

    private void Update()
    {
        if(Interactable.destroyedMaterial == 1)
        {
            line4.SetActive(true);
            line3.SetActive(false);
            line4Audio.Play();
            Interactable.destroyedMaterial = 2;
        }

        if(Player.enemySeen == 1)
        {
            line4.SetActive(false);
            line5.SetActive(true);
            line5Audio.Play();
            Player.enemySeen = 2;
            Destroy(bewareSign);
        }
    }

    public void ExitDialogue(int index)
    {
        if (index == 0)
        {
            line2.SetActive(false);
            line2Audio.Stop();
            line3.SetActive(true);
            line3Audio.Play();
            index++;
        }
        else if (index == 1)
        {
            line3.SetActive(false);
            line3Audio.Stop();
            index++;            
        }
        else if (index == 2)
        {       
            line4.SetActive(false);
            line4Audio.Stop();
        }
        else if (index == 3)
        {
            line5.SetActive(false);
            line5Audio.Stop();
        }
    }
}
