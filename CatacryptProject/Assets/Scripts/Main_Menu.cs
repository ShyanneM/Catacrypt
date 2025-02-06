using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public AudioClip menuSound;

    /*void Start ()
    {
        PlaySound();
    }*/
    
    void Update ()
    {
        //Start Game
        if(Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("CharacterSelection");
        } 

        //Settings Menu
        if(Input.GetKeyDown(KeyCode.P))
        {
            settingsMenu.gameObject.SetActive(!settingsMenu.gameObject.activeSelf);
            mainMenu.gameObject.SetActive(!settingsMenu.gameObject.activeSelf);
        }
        //Quit Game
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Game ended.");
            Application.Quit();
        }
    }

}
