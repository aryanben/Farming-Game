using System.IO;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxScoreboardEntries = 5;
    [SerializeField] private Transform highscoresHolderTransform;
    [SerializeField] private GameObject scoreboardEntryObject;
    private string SavePath => $"{Application.persistentDataPath}/highscore.json";

    private void Start()
    {
        ScoreboardSaveData savedScores = GetSavedScores();

        UpdateUI(savedScores);

        SaveScores(savedScores);
    }
    public void AddTestEntry()
    {
        AddEntry(new ScoreboardEntryData()
        {
            entryName = GameManager.gm.playerName,
            entryScore = GameManager.day
        });
    }
    public void AddEntry(ScoreboardEntryData scoreboardEntryData)
    {
        ScoreboardSaveData savedScores = GetSavedScores();

        bool scoreAdded = false;

        //Check if the score is low enough to be added.
        for (int i = 0; i < savedScores.highscores.Count; i++)
        {
            if (GameManager.day > savedScores.highscores[i].entryScore)
            {
                savedScores.highscores.Insert(i, scoreboardEntryData);
                scoreAdded = true;
                break;
            }
        }

        //Check if the score can be added to the end of the list.
        if (!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries)
        {
            savedScores.highscores.Add(scoreboardEntryData);
        }

        //Remove any scores past the limit.
        if (savedScores.highscores.Count > maxScoreboardEntries)
        {
            savedScores.highscores.RemoveRange(maxScoreboardEntries, savedScores.highscores.Count - maxScoreboardEntries);
        }

        UpdateUI(savedScores);

        SaveScores(savedScores);
    }

    private void UpdateUI(ScoreboardSaveData savedScores)
    {
        foreach (Transform child in highscoresHolderTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (ScoreboardEntryData highscore in savedScores.highscores)
        {
            Instantiate(scoreboardEntryObject, highscoresHolderTransform).GetComponent<ScoreboardEntryUI>().Initialize(highscore);
        }
    }

    private ScoreboardSaveData GetSavedScores()
    {
        if (!File.Exists(SavePath))
        {
            File.Create(SavePath).Dispose();
            return new ScoreboardSaveData();
        }

        using (StreamReader stream = new StreamReader(SavePath))
        {
            string json = stream.ReadToEnd();

            return JsonUtility.FromJson<ScoreboardSaveData>(json);
        }
    }

    private void SaveScores(ScoreboardSaveData scoreboardSaveData)
    {
        using (StreamWriter stream = new StreamWriter(SavePath))
        {
            string json = JsonUtility.ToJson(scoreboardSaveData, true); //json file will be displayed vertically 
            stream.Write(json);
        }
    }
}
