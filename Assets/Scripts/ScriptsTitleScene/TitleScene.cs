using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.anyKey.isPressed)
        {
            SceneManager.LoadScene("MainMenu_Scene");
        }
    }
}
