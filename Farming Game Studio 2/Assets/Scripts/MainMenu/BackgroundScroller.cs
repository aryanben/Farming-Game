using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 0;
    float pos = 0;
    private RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
    }
    void Update()
    {
        pos += speed;
        image.uvRect = new Rect(pos, 0, image.uvRect.width, image.uvRect.height);
    }
}
