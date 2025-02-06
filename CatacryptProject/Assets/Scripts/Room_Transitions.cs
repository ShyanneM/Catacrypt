//using System.Numerics;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class GameController : MonoBehaviour
{
    private float continueTimer = 10f;
    private bool isOnGameOverScreen = false;
    private int selectedCharacter;
    private string[] Characters = {"Fae", "Nephilim", "Witch", "Knight"};
    private bool hasSpecialItem = false; //special item to clear the game
    private PlayerController playerController;

    public Image upArrow;
    public Image leftArrow;
    public Image rightArrow;
    public Image downArrow;
    public TextMeshProUGUI startText;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu" && Input.anyKeyDown)
        {
            GoToMainMenu();
        }
        else if (SceneManager.GetActiveScene().name == "CharacterSelection")
        {
            HandleCharacterSelection ();
        }
        else if (isOnGameOverScreen)
        {
            continueTimer -= Time.deltaTime;

            if (continueTimer <= 0)
            {
                GoToMainMenu();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(1); //add start game object
            }
        }
        if (hasSpecialItem && Input.GetKeyDown(KeyCode.W)) //Change victory from button press to item pick up
        {
            GoToVictoryScreen();
        }
        continueTimer -= Time.deltaTime;
        if (continueTimer <= 0)
        {
            GoToMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            GoToMainMenu();
        }
    }

    private void HandleCharacterSelection()
    {
        PlayerController player = gameObject.GetComponent<PlayerController>();
        
        startText.enabled = false;        

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            startText.enabled = true;
            upArrow.rectTransform.sizeDelta = new Vector2(200, 100);
            downArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            rightArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            leftArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            
            selectedCharacter=0; //Knight
            player.MenuSelectResult(0);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            upArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            downArrow.rectTransform.sizeDelta = new Vector2(200, 100);
            rightArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            leftArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            startText.enabled = true;

            selectedCharacter=1; //Fae
            player.MenuSelectResult(1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            upArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            downArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            rightArrow.rectTransform.sizeDelta = new Vector2(200, 100);
            leftArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            startText.enabled = true;

            selectedCharacter=2; //Witch
            player.MenuSelectResult(2);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            upArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            downArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            rightArrow.rectTransform.sizeDelta = new Vector2(100, 100);
            leftArrow.rectTransform.sizeDelta = new Vector2(200, 100);
            startText.enabled = true;   
                     
            selectedCharacter=3; //Nephilim
            player.MenuSelectResult(3);
        }
        else if (Input.GetKeyDown(KeyCode.F)) //confirm
        {
            SceneManager.LoadScene("Level 1"); //add start game object
        }

    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void EnterGameOverScreen()
    {
        SceneManager.LoadScene("Game Over Screen");
        isOnGameOverScreen = true;
        continueTimer = 10f;
    }
    public void GoToVictoryScreen()
    {
        SceneManager.LoadScene("Victory Screen");
        hasSpecialItem = false; //this is the end the game scene
    }
    public void AcquireSpecialItem()
    {
        hasSpecialItem = true;
    }
}

