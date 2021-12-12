using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;

public class VDScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu_Scene");
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene("MainGame_Scene");
    }
}
