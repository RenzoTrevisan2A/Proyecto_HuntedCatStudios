using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BurningGO : MonoBehaviour
{
    public VisualEffect burningFlames;

    [Range(1, 5)]
    public float burningTime;


    void Start()
    {
        burningFlames.Stop();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Burning());
    }

    private IEnumerator Burning()
    {
        float startTime = Time.time;

        while (Time.time < startTime + burningTime)
        {
            burningFlames.Play();
            yield return null;
        }

        Destroy(gameObject);
    }
}
