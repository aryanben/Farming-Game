using System.Collections.Generic;
using UnityEngine;
public class TargetController : MonoBehaviour
{
    public static TargetController instance;
    public List<GameObject> enemiesInRange;
    public List<string> enemiesDetectedTag;
    private void Start()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < enemiesDetectedTag.Count; i++)
        {
            if (other.CompareTag(enemiesDetectedTag[i]))
            {
                GameManager.isAllowedToAttack = true;
                enemiesInRange.Add(other.gameObject);
                break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < enemiesDetectedTag.Count; i++)
        {
            if (other.CompareTag(enemiesDetectedTag[i]))
            {
                GameManager.isAllowedToAttack = false;
                enemiesInRange.Remove(other.gameObject);
                break;
            }
        }
    }
}
