using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class IncreaseScoreThroughScenes
{
    public static int playerScore;
}
public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public GameObject mill;
    public Texture2D combatCursor;
    public Texture2D normalCursor;
    CursorMode cursorMode = CursorMode.Auto;

    //Leaderboard
    public GameObject leaderBoard;
    public string playerName;
    public static int day = 1;
    public Text scoreText;
    Scoreboard scoreboardClass;
    public GameObject scoreBoard;
    public GameObject inputField;
    public GameObject showLeaderboard;
    float increaseDay = 244;
    public GameObject scoreAndInputFieldCanvas;

    public static bool isAllowedToAttack;
    void Start()
    {
        showLeaderboard.SetActive(false);

        gm = this;

        scoreboardClass = scoreBoard.GetComponent<Scoreboard>();

        Cursor.visible = true;
    }
    void Update()
    {
        Vector2 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursor;
        Cursor.SetCursor(normalCursor, Vector2.zero, cursorMode);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                isAllowedToAttack = true;
                Cursor.SetCursor(combatCursor, Vector2.one, cursorMode);
            }
            else isAllowedToAttack = false;
        }

        mill.gameObject.transform.Rotate(0, 0, 50 * Time.deltaTime);
        IncreaseScoreThroughScenes.playerScore = day;
        scoreText.text = "Day: " + day.ToString();
        increaseDay -= Time.deltaTime;

        if (increaseDay <= 0)
        {
            day++;
            increaseDay = 310f;
        }
    }
    public void ShowPlayerName()
    {
        playerName = inputField.GetComponent<Text>().text;
        scoreboardClass.AddTestEntry();
        showLeaderboard.SetActive(true);        
    }
    public void LeaderBoard()
    {
        leaderBoard.SetActive(true);
    }
    public void Exit()
    {
        SceneManager.LoadScene("Odera's Scene");
        day = 1;
    }
}
