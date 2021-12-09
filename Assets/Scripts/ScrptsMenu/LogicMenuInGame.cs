using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicMenuInGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var keepBetweenScenes = FindObjectsOfType<LogicMenuInGame>();
        if(keepBetweenScenes.Length > 1)
        {
            Destroy(keepBetweenScenes[1]);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
