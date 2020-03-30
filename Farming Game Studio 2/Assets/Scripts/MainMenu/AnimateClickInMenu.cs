using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
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

    public void OnHoverTitle()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1);
    }

    public void OnLeaveHoverTitle()
    {
        transform.localScale = new Vector3(1.03f, 1.03f, 1);
    }

    public void ExitFromFishing()
    {
        SceneManager.LoadScene("Odera's Scene");
    }
    public void OnExit()
    {
        Application.Quit();
    }
}
