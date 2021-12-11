using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public GameObject transitionPanel;

    void Update()
    {
        if (Keyboard.current.anyKey.isPressed)
        {
            StartCoroutine(Trasitioning("MainMenu_Scene"));
        }
    }

    public IEnumerator Trasitioning(string scene)
    {
        transitionPanel.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
