using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadDay : MonoBehaviour
{
    float loadDay = 10f;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.day += 1;
    }

    // Update is called once per frame
    void Update()
    {
        loadDay -= Time.deltaTime;

        if(loadDay <= 0)
        {           
            SceneManager.LoadSceneAsync("Odera's Scene");
            loadDay = 10f;           
        }
    }
}
