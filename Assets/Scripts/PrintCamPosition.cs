using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;

public class PrintCamPosition : MonoBehaviour
{
    public Camera cam;
    //public TextMeshPro text;
    public TextMeshProUGUI text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText($"{cam.transform.position}");
    }
}
