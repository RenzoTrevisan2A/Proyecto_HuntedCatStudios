using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjects : MonoBehaviour
{
    public bool isPickable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerInteractionZone"))
        {
            other.GetComponentInParent<PickUpObjects>().objectToPickUp = this;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerInteractionZone"))
        {
            other.GetComponentInParent<PickUpObjects>().objectToPickUp = null;
        }
    }
}
