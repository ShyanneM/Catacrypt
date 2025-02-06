using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(string action)
    {
        switch(action)
        {
            case "Skeleton":
                score +=10;
                break;
            case "Boss":
                score +=250;
                break;
            case "Key":
                score +=100;
                break;
            case "Door":
                score +=100;
                break;
            case "Chest":
                score +=100;
                break;
            case "Spawner":
                score +=100;
                break;
             default:
            Debug.Log("Unknown action: " + action); //just in case
            break;
        }
        UpdateScoreText();
       
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score;
        }
        else
        {
            Debug.LogWarning("ScoreText is not assigned in the inspector.");
        } 
    }
}