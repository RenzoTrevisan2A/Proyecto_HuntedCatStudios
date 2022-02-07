using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class Pointer_Controller : MonoBehaviour
{
    public bool monolitoActivo;
    public GameObject canvasMonolito;

    public GameObject mouseCanvas;
    public Image cursorPointer;

    public Canvas pauseMenuCanvas;
    public OptionsController optionsPanel;

    private void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
        optionsPanel = GameObject.FindGameObjectWithTag("Options").GetComponent<OptionsController>();
        mouseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        monolitoActivo = canvasMonolito.activeSelf;
        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (monolitoActivo || pauseMenuCanvas.isActiveAndEnabled || optionsPanel.canvasOptions.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.None;
            mouseCanvas.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseCanvas.SetActive(false);
        }


        if (Cursor.lockState != CursorLockMode.Locked)
        {
            cursorPointer.transform.position = mousePos;
        }

    }
}
