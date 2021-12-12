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
        AudioManager.PlayerSound("click");
    }

    public void OnOptions()
    {
        optionsMenu.SetActive(true);
        AudioManager.PlayerSound("click");
    }

    public void OnExit()
    {
        Application.Quit();
        AudioManager.PlayerSound("click");
    }

    public IEnumerator Trasitioning(string scene)
    {
        transitionPanel.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
