using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float continueTimer = 10f;
    private bool isOnContinueScreen = false;
    private int selectedCharacter = 0;
    private string[] characters = {"Valkrie", "Warrior", "Wizard","Elf"};
    private bool hasSpecialItem = false; //special item to clear the game

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && Input.anyKeyDown)
        {
            GoToCharacterSelect();
        }
        else if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            HandleCharacterSelection ();
        }
        else if (isOnContinueScreen)
        {
            continueTimer -= Timer.deltaTime;
            if (continueTimer <= 0)
            {
                GoToMainMenu();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                StartGame();
            }
        }
        if (hasSpecialItem && Input.GetKeyDown(KeyCode.W))
        {
            GoToRoomCleared();
        }
    }

    private void HandleCharacterSelection()
    {
        if (Input.GetKeyDown(Keycode.UpArrow))
        {
            selectedCharacter=0; //valkrie
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedCharacter=1; //warrior
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedCharacter=2; //elf
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedCharacter=3; //wizard
        }
        else if (Input.GetKeyDown(KeyCode.F)) //confirm
        {
            StartGame();
        }
    }
    private void Update()
    {
        continueTimer -= continueTimer.deltaTime;
        if (continueTimer <= 0)
        {
            GoToMainMenu();
        }
        else if (Input.GetKeyDown(Keycode.F))
        {
            GoToCharacterSelect();
        }
    }
    public void GoToCharacterSelect()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void EnterContinueScreen()
    {
        isOnContinueScreen = true;
        continueTimer = 10f;
    }
    public void GoToRoomCleared()
    {
        SceneManager.LoadScene("RoomCleared");
        hasSpecialItem = false; //this is the end the game scene
    }
    public void AcquireSpecialItem()
    {
        hasSpecialItem = true;
    }
}