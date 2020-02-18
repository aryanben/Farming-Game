using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    public static int chatBox = 0;
    public void OnClick()
    {
        Destroy(gameObject);
        chatBox++;
    }
}
