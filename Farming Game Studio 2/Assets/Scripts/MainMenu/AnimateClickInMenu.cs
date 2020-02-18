using UnityEngine.SceneManagement;
using UnityEngine;

public class AnimateClickInMenu : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   public void Play()
   {
        anim.SetBool("clicked", true);
   }
    public void Stop()
    {
        anim.SetBool("clicked", false);
    }

    public void OnStart()
    {
        SceneManager.LoadScene("Intro");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
