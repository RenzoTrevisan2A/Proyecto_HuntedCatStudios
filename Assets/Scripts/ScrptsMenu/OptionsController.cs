using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public GameObject canvasOptions;
    GameObject pauseMenu;

    private void Update()
    {
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        }
    }

    public void ReturnToPauseMenu()
    {

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            canvasOptions.SetActive(false);
        }
        else
        {
            canvasOptions.SetActive(false);
        }
    }
}
