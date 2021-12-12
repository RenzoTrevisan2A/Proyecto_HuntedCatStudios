using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public float lifetime;

    private float startTime;

    WaterBall waterBall;
    private void Awake()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log($"Time.Time = {Time.time}");
        Debug.Log($" start time = {startTime}");

        if (Time.time < (startTime + lifetime))
        {
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;
        Destroy(gameObject);
    }
}
