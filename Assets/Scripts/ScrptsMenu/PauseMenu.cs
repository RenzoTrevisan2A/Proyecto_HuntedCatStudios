using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenuCanvas;
    public OptionsController optionsPanel;

    private void Start()
    {
        optionsPanel = GameObject.FindGameObjectWithTag("Options").GetComponent<OptionsController>();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            pauseMenuCanvas.gameObject.SetActive(true);
        }

        if (pauseMenuCanvas.isActiveAndEnabled || optionsPanel.canvasOptions.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu_Scene");
    }

    public void OnShowOptions()
    {
        pauseMenuCanvas.gameObject.SetActive(false);
        optionsPanel.canvasOptions.SetActive(true);
    }

    public void OnResumeGame()
    {
        optionsPanel.canvasOptions.SetActive(false);
        pauseMenuCanvas.gameObject.SetActive(false);
    }
}