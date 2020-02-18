using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    bool isGrounded;
    public GameObject smoke;
    public GameObject fallingSmoke;
    public GameObject introText;
    public GameObject introText2;

    bool waitForSecondsBoolForIntroText;
    float waitForSecondsFloatForIntroText = 5;

    bool canPlayExplosion;

    public AudioSource wind;
    public AudioSource explosion;

    int playExplosionOnce = 0;
    private void Start()
    {
        wind.Play();
    }
    void Update()
    {
        if (!isGrounded)
        {
            transform.Rotate(0f, 1f, 1f);
        } else transform.Rotate(0, 0, 0);

        if (waitForSecondsBoolForIntroText)
        {
            waitForSecondsFloatForIntroText -= Time.deltaTime;

            if (waitForSecondsFloatForIntroText <= 0)
            {
                introText.SetActive(true);
                waitForSecondsBoolForIntroText = false;
            }
        }

        if (canPlayExplosion)
        {
            explosion.Play();
            canPlayExplosion = false;
        }

        if (playExplosionOnce == 1)
        {
            canPlayExplosion = true;
        }

        if (IntroText.chatBox == 1)
        {
            introText2.SetActive(true);
        }

        if(IntroText.chatBox == 2)
        {
            SceneManager.LoadScene("Odera's Scene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(wind);
            playExplosionOnce++;
            Destroy(fallingSmoke);
            isGrounded = true;           
            smoke.SetActive(true);
            TimeForIntroText();
        }
    }

    void TimeForIntroText()
    {
        waitForSecondsBoolForIntroText = true;
    }

}
