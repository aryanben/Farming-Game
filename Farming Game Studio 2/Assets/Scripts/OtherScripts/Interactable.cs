using UnityEngine.SceneManagement;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public static Interactable interactable;
    float treeHealth;
    float stoneHealth;

    bool canAttackTree;
    bool canAttackStone;

    float axeDamage = 0.5f;

    float wood = 5f;
    float stone = 5f;

    public GameObject sleepCanvas;
    public GameObject camera;
    CameraMovement cameraAnim;

    public GameObject tutorialTree;

    //For New Day
    public GameObject dayLoadingPanel;
    public bool startNewDay;
    bool incrementDay;
    public float startNewDayTick = 1;
    bool canStartTheDay = true;
    float timeLimitToNotSleep = 60;
    public static int amountOfTrees = 22;

    public static int destroyedMaterial; //For the dialogue
    private void Awake()
    {        
        startNewDayTick = 3;
    }
    private void Start()
    {
        sleepCanvas = GameObject.Find("Sleep Canvas");
        camera = GameObject.Find("Main Camera");
        tutorialTree = GameObject.Find("TutorialTree(Clone)");
        dayLoadingPanel = GameObject.Find("DayLoading");
        treeHealth = 2;
        stoneHealth = 2;
        cameraAnim = camera.GetComponent<CameraMovement>();
        interactable = this;
    }

    private void Update()
    {
        if (canAttackTree)
        {
            canAttackTree = false;
            treeHealth -= axeDamage;
            Inventory.instance.AddItem(Item.typeEnum.Wood, 5);
        }
        if (canAttackStone)
        {
            canAttackStone = false;
            Inventory.instance.AddItem(Item.typeEnum.Stone, 5);
            stoneHealth -= axeDamage;
        }

        if (treeHealth <= 0)
        {
            if (treeHealth <= 0 && tutorialTree != null)
            {
                destroyedMaterial = 1;
            }

            amountOfTrees--;
            Inventory.instance.AddItem(Item.typeEnum.Wood, 20);
            Destroy(gameObject);
        }

        if (stoneHealth <= 0)
        {
            amountOfTrees--;
            Inventory.instance.AddItem(Item.typeEnum.Stone, 20);
            Destroy(gameObject);
        }


        if (startNewDay)
        {
            dayLoadingPanel.GetComponent<Canvas>().enabled =true;

            //Set Sun transforms to normal
            Sun.instance.sun.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            Sun.instance.night.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

            Sun.instance.sun.transform.position = new Vector3(0, 398, -0.7f);
            Sun.instance.night.transform.position = new Vector3(0, -1.2f, -0.7f);

            Sun.canRotate = false;
            startNewDayTick -= Time.deltaTime;

            if (startNewDayTick <= 0)
            {
                dayLoadingPanel.GetComponent<Canvas>().enabled = false;
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
        if (this.CompareTag("Tent") && other.gameObject.CompareTag("Player"))
        {
            sleepCanvas.GetComponent<Canvas>().enabled = true;

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
        if (this.CompareTag("Tent") && other.gameObject.CompareTag("Player"))
        {
            sleepCanvas.GetComponent<Canvas>().enabled = false;
        }
    }
}
