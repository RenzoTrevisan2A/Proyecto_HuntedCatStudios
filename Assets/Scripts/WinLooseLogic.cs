using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinLooseLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Victory"))
        {
            SceneManager.LoadScene("Victory_Scene");
        }
        else if (other.CompareTag("Victory"))
        {
            SceneManager.LoadScene("Loosing_Scene");
        }
    }

}
