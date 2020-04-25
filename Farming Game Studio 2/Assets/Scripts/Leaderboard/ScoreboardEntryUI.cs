using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private Text entryNameText = null;
    [SerializeField] private Text entryScoreText = null;

    public void Initialize(ScoreboardEntryData scoreboardEntryData)
    {
        entryNameText.text = scoreboardEntryData.entryName;
        entryScoreText.text = "Day Survived: " + scoreboardEntryData.entryScore.ToString();
    }
}
