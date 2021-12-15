using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToLookAt : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    public Transform target;

    public float retard;
    private float originalRetard;

    [SerializeField] float minDistance = 0.1f;
    [SerializeField] float maxDistance = 3f;
    [SerializeField] float speedUp;

    Character character;
    private void Start()
    {
        originalRetard = retard;
    }

    void FixedUpdate()
    {
        transform.LookAt(target.position);

        Vector3 direction = target.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        float distance = direction.magnitude;

        if (distance > maxDistance)
        {
            Debug.DrawRay(transform.position, direction.normalized * (distance - maxDistance), Color.green);
            transform.Translate(direction.normalized * (distance - maxDistance));
        }

        if (distance > minDistance)
        {
            PerformFollowing();
            speedUp += 0.1f * Time.deltaTime;
        }
        else
        {
            //retard = originalRetard;
            speedUp -= 0.1f * Time.deltaTime;
            if (speedUp < 0f) { speedUp = 0f; }
        }
    }

    private void PerformFollowing()
    {
        //retard -= Time.deltaTime;

        //if (retard <= 0.0f) 
        //{
            //transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * (speed + speedUp) * Time.deltaTime);
        //}
    }
}
