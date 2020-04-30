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

    public GameObject tutorialTree;
    public GameObject tutorialStone;

    //For New Day
    public GameObject dayLoadingPanel;
    public bool startNewDay;
    bool incrementDay;
    public float startNewDayTick = 1;
    bool canStartTheDay = true;
    float timeLimitToNotSleep = 60;

    public static int destroyedMaterial; //For the dialogue
    private void Start()
    {
        treeHealth = 2;
        stoneHealth = 2;
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

        if (treeHealth <= 0)
        {
            if (treeHealth <= 0 && tutorialTree != null)
            {
                destroyedMaterial = 1;
            }

            Debug.Log("Inventory.AddWood = " + 20);
            Destroy(gameObject);
        }

        if (stoneHealth <= 0)
        {
            Debug.Log("Inventory.AddStone = " + 20);
            Destroy(gameObject);
        }



        if (startNewDay)
        {
            dayLoadingPanel.SetActive(true);

            //Set Sun transforms to normal
            Sun.instance.sun.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            Sun.instance.night.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

            Sun.instance.sun.transform.position = new Vector3(0, 398, -0.7f);
            Sun.instance.night.transform.position = new Vector3(0, -1.2f, -0.7f);

            Sun.canRotate = false;
            startNewDayTick -= Time.deltaTime;

            if (startNewDayTick <= 0)
            {
                dayLoadingPanel.SetActive(false);
                startNewDay = false;
                incrementDay = true;
                startNewDayTick = 1;
            }
        }
        if (incrementDay)
        {
            GameManager.day++;
            Sun.canRotate = true;
            incrementDay = false;
            canStartTheDay = false;
        }
        if (startNewDayTick >= 1)
        {
            incrementDay = false;
        }

        if(canStartTheDay == false)
        {
            timeLimitToNotSleep -= Time.deltaTime;

            if(timeLimitToNotSleep <= 0)
            {
                canStartTheDay = true;
                timeLimitToNotSleep = 10;
            }
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
        //if (this.CompareTag("Well") && other.gameObject.CompareTag("Player"))
        //{
        //    sightSeeCanvas.SetActive(true);
        //    if (Input.GetKey(KeyCode.Return))
        //    {
        //        SceneManager.LoadScene("FishSightSee");
        //    }
        //}

        if (this.CompareTag("Tent") && other.gameObject.CompareTag("Player"))
        {
            sleepCanvas.SetActive(true);

            if (Input.GetKey(KeyCode.Return))
            {
                if (canStartTheDay)
                {
                    startNewDay = true;
                }      
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
