using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    //Main Menu Functions
    public void OnPlay()
    {
        SceneManager.LoadScene("MainGame_Scene");
    }

    public void OnOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
