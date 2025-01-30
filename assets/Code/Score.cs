using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(string action)
    {
        switch(action)
        {
            case "KillGhost":
                score +=10;
                break;
            case "Boss":
                score +=250;
                break;
            case "ObtainKey":
                score +=100;
                break;
            case "OpenDoor":
                score +=100;
                break;
            case "ObtainChest":
                score +=100;
                break;
            case "Spawner":
                score +=100;
                break;
             default:
            Debug.Log("Unknown action: " + action); //just in case
            break;
        }
        UpdateScoreText()
       
    }

    private void UpdateScoreText();
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("ScoreText is not assigned in the inspector.");
        }
    }
}