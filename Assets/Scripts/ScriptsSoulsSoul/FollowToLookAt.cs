using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToLookAt : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10.0f;
    [SerializeField] float speedBase = 4f;
    float actualSpeed = 0f;
    public Transform target;

    [SerializeField] float minDistance = 0.1f;
    [SerializeField] float maxDistance = 3f;
    //[SerializeField] float speedUp;

    [SerializeField] float acceleration;
    private void Start()
    {
        actualSpeed = speedBase;
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
            transform.Translate(direction.normalized * (distance - maxDistance), Space.World);
        }

        if (distance > minDistance)
        {
            PerformFollowing();
        }
        else if(distance < minDistance)
        {
            actualSpeed = speedBase;
        }
    }

    private void PerformFollowing()
    {
        Vector3 direction = target.position - transform.position;

        if (actualSpeed < maxSpeed)
        {
            actualSpeed += acceleration * Time.deltaTime;
        }

        transform.Translate(actualSpeed * Time.deltaTime * direction.normalized, Space.World);
    }
}
