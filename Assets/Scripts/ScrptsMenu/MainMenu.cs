using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public GameObject transitionPanel;
    //Main Menu Functions
    public void OnPlay()
    {
        StartCoroutine(Trasitioning("MainGame_Scene"));
    }

    public void OnOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public IEnumerator Trasitioning(string scene)
    {
        transitionPanel.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
