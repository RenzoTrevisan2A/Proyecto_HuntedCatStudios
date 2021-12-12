using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip clickMenuSound, flamesSound, waterBallSound, pickingarockSound, rockHittingSound;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        clickMenuSound = Resources.Load<AudioClip>("ClickMenu");
        flamesSound = Resources.Load<AudioClip>("Flames");
        waterBallSound = Resources.Load<AudioClip>("water1");
        pickingarockSound = Resources.Load<AudioClip>("PickingRock");
        rockHittingSound = Resources.Load<AudioClip>("Rock 3");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayerSound(string clip)
    {
        switch (clip)
        {
            case "flames":
                audioSource.PlayOneShot(flamesSound);
                break;
            case "click":
                audioSource.PlayOneShot(clickMenuSound);
                break;
            case "waterball":
                audioSource.PlayOneShot(waterBallSound);
                break;
            case "pickingrock":
                audioSource.PlayOneShot(pickingarockSound);
                break;
            case "rockhit":
                audioSource.PlayOneShot(rockHittingSound);
                break;
        }
    }
}
