using UnityEngine.SceneManagement;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    float treeHealth;
    float stoneHealth;

    bool canAttackTree;
    bool canAttackStone;

    float axeDamage = 0.5f;

    float wood = 5f;
    float stone = 5f;

    public GameObject sightSeeCanvas;
    public GameObject sleepCanvas;
    public GameObject camera;
    CameraMovement cameraAnim;
    private void Start()
    {
        treeHealth = 5;
        stoneHealth = 5;
        cameraAnim = camera.GetComponent<CameraMovement>();
    }

    private void Update()
    {
        if (canAttackTree)
        {
            canAttackTree = false;
            treeHealth -= axeDamage;
            Debug.Log("Inventory.AddWood = " + wood);
        }
        if (canAttackStone)
        {
            canAttackStone = false;
            Debug.Log("Inventory.AddStone = " + stone);
            stoneHealth -= axeDamage;
        }

        if(treeHealth <= 0)
        {
            Debug.Log("Inventory.AddWood = " + 20);
            Destroy(gameObject);
        }

        if(stoneHealth <= 0)
        {
            Debug.Log("Inventory.AddStone = " + 20);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Tree") && other.CompareTag("Sword")) //Axe Wood
        {
            canAttackTree = true;
        }

        if (this.CompareTag("Stone") && other.CompareTag("Sword")) //Axe Stone
        {
            canAttackStone = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (this.CompareTag("Well") && other.gameObject.CompareTag("Player"))
        {
            sightSeeCanvas.SetActive(true);
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("FishSightSee");
            }
        }

        if (this.CompareTag("Tent") && other.gameObject.CompareTag("Player"))
        {
            sleepCanvas.SetActive(true);
            if (Input.GetKey(KeyCode.Return))
            {
                cameraAnim.GetComponent<Animation>().Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.CompareTag("Well") && other.gameObject.CompareTag("Player"))
        {
            sightSeeCanvas.SetActive(false);
        }
        if (this.CompareTag("Tent") && other.gameObject.CompareTag("Player"))
        {
            sleepCanvas.SetActive(false);
        }
    }
}
