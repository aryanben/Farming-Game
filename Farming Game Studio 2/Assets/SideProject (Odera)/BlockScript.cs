using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    Animation anim;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        anim = ball.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            anim.Play();
        }
    }
}
