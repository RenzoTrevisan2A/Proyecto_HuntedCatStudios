using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToLookAt : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    public Transform target;

    public const float EPSILON = 0.1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(target.position);

        if ((transform.position - target.position).magnitude > EPSILON)
        {
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }
    }
}
