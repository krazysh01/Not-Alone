using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobileMenuController : MonoBehaviour
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
        NetworkManager.StartClient();
        MainMenu.SetActive(false);
        Waiting.SetActive(true);
    }
    
    public void GameConnected(ServerResponse response)
    {
        if (NetworkClient.isConnected)
        {
            SceneManager.LoadScene(gameScene.name);
        }
    }
}
