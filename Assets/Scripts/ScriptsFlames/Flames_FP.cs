using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Flames_FP : MonoBehaviour
{
    public VisualEffect flames;
    public Elementos2 elementos2;
    public Collider flamesCO;
    public bool flamesAudioPlayed = false;

    [Range(0, 1)]
    public float flamesTimeOn = 0.5f;

    [Range(0, 1)]
    public float flamesCD = 0.25f;

    public bool powerActionPressed = false;

    void Start()
    {
        flamesCO.enabled = false;
        flames.Stop();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) { powerActionPressed = true; }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(powerActionPressed && !elementos2.poderDeFuegoActivo)
        {
            powerActionPressed = false;
            flamesCO.enabled = false;
        }

        if (elementos2.poderDeFuegoActivo)
        {
            StartCoroutine(StartFlames());
        }
        else if (elementos2.poderDeFuegoActivo == false)
        {
            flames.Stop();
        }
    }

    private IEnumerator StartFlames()
    {
        float startTime = Time.time; // need to remember this to know how long to dash

        if (powerActionPressed)
        {
            while (Time.time < startTime + flamesTimeOn && !flamesAudioPlayed)
            {
                flamesCO.enabled = true;
                flames.Play();

                AudioManager.PlayerSound("flames");
                flamesAudioPlayed = true;
                
                yield return null;
            }

            while (Time.time < startTime + flamesCD)
            {
                flamesCO.enabled = false;
                flames.Stop();
                powerActionPressed = false;
                yield return null;
            }
            flamesCO.enabled = false;
            flames.Stop();
            flamesAudioPlayed = false;
            powerActionPressed = false;
        }
    }
}
