using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> WaterBallVFX = new List<GameObject>();
    public Elementos2 elementos2;

    private GameObject effectToSpawn;

    private float timeToFire = 0f;


    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = WaterBallVFX[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (elementos2.poderDeAguaActivo)
        {
            if (Mouse.current.leftButton.isPressed && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / effectToSpawn.GetComponent<WaterBallMove>().fireRate;
                SpawnWaterBall();
            }
        }
    }

    private void SpawnWaterBall()
    {
        GameObject ball = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
        ball.transform.localRotation = gameObject.transform.localRotation;
        AudioManager.PlayerSound("waterball");
    }
}
