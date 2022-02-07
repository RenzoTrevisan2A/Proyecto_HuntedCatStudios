using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicMenuInGame : MonoBehaviour
{
    static LogicMenuInGame instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
