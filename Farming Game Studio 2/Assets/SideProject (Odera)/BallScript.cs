using UnityEngine.UI;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public GameObject youWinCanvas;
    public GameObject instructionPanel;
    Rigidbody rb;
    bool start = false;
    float decreaseTimer = 3f;
    bool instructionsEnabled = true;
    public Text scoreText;
    int score;

    float incrementScoreTime = 1;
    private void Start()
    {
        youWinCanvas.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        instructionPanel.SetActive(true);
        decreaseTimer -= Time.deltaTime;

        if (decreaseTimer <= 0)
        {
            start = true;
            instructionPanel.SetActive(false);
            instructionsEnabled = false;
        }

        if (instructionsEnabled)
        {
            rb.isKinematic = true;
        }

        if (instructionsEnabled == false)
        {         
            if (start)
            {
                incrementScoreTime -= Time.deltaTime;

                if(incrementScoreTime <= 0)
                {
                    score++;
                    incrementScoreTime = 1;
                }
                
                rb.isKinematic = false;

                if (Input.GetMouseButton(0))
                {
                    rb.velocity = Vector3.down * 4000 * Time.deltaTime;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("UnbreakableBlock"))
        {
            rb.velocity = Vector3.up * 600f * Time.deltaTime;
            collision.gameObject.GetComponent<Renderer>().material.color = Color.white;

            if (collision.gameObject.GetComponent<Renderer>().material.color == Color.white)
            {
                Destroy(collision.gameObject, 1f);
            }
        }

        if (collision.transform.CompareTag("BreakableBlock"))
        {
            rb.velocity = Vector3.up * 300f * Time.deltaTime;

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("You Win"))
        {
            youWinCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
