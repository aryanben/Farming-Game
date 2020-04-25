using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public static PlayerFollow instance;
    Transform Cam;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    public bool rotateAroundPlayer = true;
    public float rotationSpeed = 5.0f;
    Vector3 offset;
    public Transform player;
    public Quaternion camTurnAngle;
    void Start()
    {
        Cam = this.transform;
        offset = transform.position - player.position;
        instance = this;
    }

    void Update()
    {
        if(player != null)
        {
            Vector3 newPos = player.position + offset;
            transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

            if (rotateAroundPlayer)
            {
                Vector3 lookAt = player.position;
                lookAt.y = player.position.y - .1f;
                transform.LookAt(lookAt);
            }

            if (Input.GetMouseButton(2))
            {
                if (rotateAroundPlayer)
                {
                    camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
                    offset = camTurnAngle * offset;
                }
            }

            if (GameObject.FindGameObjectWithTag("BoidManager"))
            {
                if (rotateAroundPlayer)
                {
                    if (Input.GetMouseButton(2))
                    {
                        Vector3 lookAt = player.position;
                        lookAt.y = player.position.y - .1f;
                        transform.LookAt(lookAt);

                        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
                        offset = camTurnAngle * offset;
                    }
                }
            }
        }        
    }
}
