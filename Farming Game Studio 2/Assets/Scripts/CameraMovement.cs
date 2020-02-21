using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 offset;
    public Transform player;
    private void Start()
    {
        offset = player.transform.position - transform.position;
    }
    void Update()
    {
        if(player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.position - offset, 0.1f);
        }        
    }
}
