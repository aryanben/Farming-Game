using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float y = 3.7f;
    public float z = -5;
    public float x = 0;
    public Transform player;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, (player.position + new Vector3(x, y, z)), 0.1f);        
    }
}
