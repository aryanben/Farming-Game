using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class HoldPlayerAnim : MonoBehaviour
{
    Player hasAnimation;
    //Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        hasAnimation = FindObjectOfType<Player>();
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Well"))
        //{
        //    hasAnimation.PlayAnimation(hasAnimation.quickAnim.GetClip("Run"));
        //}
        //else
        //if (collision.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Tree"))
        //{
        //    hasAnimation.PlayAnimation(hasAnimation.quickAnim.GetClip("ChainSaw_Slash2"));
        //}
    }
}
