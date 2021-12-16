using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsSoul_Logic : MonoBehaviour
{
    public Transform target;
    [SerializeField] float speed = 10.0f;
    
    [SerializeField] float minDistance = 0.1f;
    [SerializeField] float maxDistance = 3f;

    [SerializeField] float speedUp;
    [Range(0, .5f)]
    public float acceleration;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);

        if ((target.position - transform.position).magnitude > maxDistance)
        {
            transform.Translate(0, 0, (target.position - transform.position).magnitude - maxDistance);
        }

        if ((target.position - transform.position).magnitude > minDistance)
        {
            PerfomFollowing();
            speedUp += acceleration * Time.deltaTime;
        }
        else
        {
            speedUp -= acceleration * Time.deltaTime;
            if(speedUp < 0) { speedUp = 0f; }
        }
    }

    private void PerfomFollowing()
    {
        transform.Translate(0, 0, (speed + speedUp) * Time.deltaTime);
    }
}
