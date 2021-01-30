using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public NetworkManager NetworkManager;
    public GameObject MainMenu;
    public GameObject Waiting;
    public Scene gameScene;

    private void Start()
    {
        MainMenu.SetActive(true);
        Waiting.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        NetworkManager.StartHost();
        MainMenu.SetActive(false);
        Waiting.SetActive(true);
    }

    public void GameConnected()
    {
        SceneManager.LoadScene(gameScene.name);
    }
    
}
