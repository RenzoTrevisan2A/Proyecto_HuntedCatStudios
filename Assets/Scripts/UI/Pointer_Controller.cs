using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer_Controller : MonoBehaviour
{
    public bool monolitoActivo;
    public GameObject canvasMonolito;
    public Texture2D cursorPointer;

    public Canvas pauseMenuCanvas;
    public OptionsController optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.SetCursor(cursorPointer, Vector2.zero, CursorMode.Auto);
        optionsPanel = GameObject.FindGameObjectWithTag("Options").GetComponent<OptionsController>();
    }

    // Update is called once per frame
    void Update()
    {
        monolitoActivo = canvasMonolito.activeSelf;
        
        if (monolitoActivo || pauseMenuCanvas.isActiveAndEnabled || optionsPanel.canvasOptions.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //if (pauseMenuCanvas.isActiveAndEnabled || optionsPanel.canvasOptions.activeSelf == true)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //}
    }
}
