using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, (player.position + new Vector3(0, 3.7f, -5)), 0.1f);        
    }
}
