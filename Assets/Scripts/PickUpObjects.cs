using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField] float ballforce;


    public PickableObjects objectToPickUp;
    public PickableObjects pickedObject;
    public Transform interactionZone;

    [Header("References to interaction Zones")]
    public Transform interactionZonePick;
    public Transform interactionZoneDrag;

    private float gravityValue = -9.81f;

    public bool iHaveARock = false;
    public bool ImDraggingSomething = false;

    bool powerActionPressed = false;
    bool selectButtonPressed = false;
    public bool isPickable = false;
    public bool monolitoActivo = false;

    Elementos2 elementos2;
    public GameObject canvasMonolito;
    public SimpleMonolito monolitosSiple;
    

    private void Awake()
    {
        elementos2 = GetComponent<Elementos2>();
    }

    private void Start()
    {
        ballforce += gravityValue * Time.deltaTime;
        Cursor.visible = false;
    }

    public Texture2D cursorPointer;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) { powerActionPressed = true; }
    }

    private void FixedUpdate()
    {
        monolitoActivo = canvasMonolito.activeSelf;

        if (!monolitoActivo)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (monolitoActivo)
        {
            Cursor.SetCursor(cursorPointer, Vector2.zero, CursorMode.Auto);
            Cursor.visible = true;  
            Cursor.lockState = CursorLockMode.None;
        }

        if (objectToPickUp == null)
        {
            if (powerActionPressed)
            {
                powerActionPressed = false;
            }
            else if (selectButtonPressed)
            {
                selectButtonPressed = false;
            }
        }



        if (elementos2.poderDeTierraActivo == true)
        {
            if (pickedObject != null)
            {
                if (pickedObject.CompareTag("PickableObject"))
                {
                    iHaveARock = true;
                }
                else if (pickedObject.CompareTag("DraggingObject"))
                {
                    ImDraggingSomething = true;
                }
            }

            if (objectToPickUp != null && objectToPickUp.isPickable == true && pickedObject == null)
            {
                if(objectToPickUp.CompareTag("PickableObject") || objectToPickUp.CompareTag("DraggingObject"))
                {
                    PickingUpObjects();
                }
            }
            
            if (pickedObject != null && pickedObject.CompareTag("PickableObject"))
            {
                ThrowingObjects();
            }
            else if (pickedObject != null && pickedObject.CompareTag("DraggingObject"))
            {
                LettingDownObjects();
            }
        }
        else if (elementos2.poderDeTierraActivo != true)
        {
            if (powerActionPressed)
            {
                powerActionPressed = false;
            }

            LettingDownObjects();
        }


        if (objectToPickUp != null)
        {
            if (objectToPickUp.CompareTag("Monolito") && selectButtonPressed)
            {
                SetMonolito();
                LockScripts();
            }
        }

        if (objectToPickUp != null && selectButtonPressed)
        {
            SetSimpleMonolito();
        }

    }

    private void PickingUpObjects()
    {
        if (powerActionPressed)
        {
            interactionZone = SelectInteractionZoneFromObject(objectToPickUp);
            interactionZoneDrag.GetComponent<Collider>().enabled = false;

            pickedObject = objectToPickUp;
            pickedObject.isPickable = false;
            pickedObject.transform.SetParent(interactionZone);
            pickedObject.transform.position = interactionZone.position;
            pickedObject.GetComponent<Rigidbody>().useGravity = false;
            pickedObject.GetComponent<Rigidbody>().isKinematic = true;

            powerActionPressed = false;
        }
    }

    private Transform SelectInteractionZoneFromObject(PickableObjects objectToPickUp)
    {
        Transform zone = null;

        if (objectToPickUp.CompareTag("PickableObject"))
        {
            zone = interactionZonePick;
        }
        else if (objectToPickUp.CompareTag("DraggingObject"))
        {
            zone = interactionZoneDrag;
        }

        return zone;

        // Demonstrative - does the same as the code above
        //return
        //    objectToPickUp.CompareTag("PickableObject") ? interactionZonePick :
        //    objectToPickUp.CompareTag("DraggingObject") ? interactionZoneDrag :
        //    null;
    }

    private void ThrowingObjects()
    {
        if (powerActionPressed)
        {
            Rigidbody rb = pickedObject.GetComponent<Rigidbody>();

            pickedObject.isPickable = true;
            pickedObject.transform.SetParent(null);

            rb.useGravity = true;
            rb.isKinematic = false;
            rb.AddForce(transform.up * ballforce + transform.right * (ballforce / 2.5f), ForceMode.Impulse);

            pickedObject = null;
            interactionZoneDrag.GetComponent<Collider>().enabled = true;
            powerActionPressed = false;
            iHaveARock = false;
        }
    }

    private void LettingDownObjects()
    {
        if (pickedObject != null) {
            if (powerActionPressed || elementos2.poderDeTierraActivo != true)
            {
                Rigidbody rb = pickedObject.GetComponent<Rigidbody>();

                pickedObject.isPickable = true;
                pickedObject.transform.SetParent(null);

                rb.useGravity = true;
                rb.isKinematic = false;

                pickedObject = null;
                interactionZoneDrag.GetComponent<Collider>().enabled = true;
                powerActionPressed = false;
                ImDraggingSomething = false;
                iHaveARock = false;
            }
        }
    }

    
    private void SetMonolito()
    {
        if (!monolitoActivo)
        {
            if (objectToPickUp.CompareTag("Monolito"))
            {
                canvasMonolito.SetActive(true);
                
                selectButtonPressed = false;
            }
        }
        else
        {
            canvasMonolito.SetActive(false);
            selectButtonPressed = false;
        }
    }

    private void SetSimpleMonolito()
    {
        if (objectToPickUp.CompareTag("Fuego") || objectToPickUp.CompareTag("Agua"))
        {
            if (elementos2.elementoPrincipal == Elementos2.Elementos.Ninguno)
            {
                if (objectToPickUp.CompareTag("Fuego"))
                {
                    elementos2.elementoPrincipal = Elementos2.Elementos.Fuego;
                }
                else if (objectToPickUp.CompareTag("Agua"))
                {
                    elementos2.elementoPrincipal = Elementos2.Elementos.Agua;
                }
                selectButtonPressed = false;
            }
            else
            {
                elementos2.elementoSecundario = elementos2.elementoPrincipal;
                if (objectToPickUp.CompareTag("Fuego"))
                {
                    elementos2.elementoPrincipal = Elementos2.Elementos.Fuego;
                }
                else if (objectToPickUp.CompareTag("Agua"))
                {
                    elementos2.elementoPrincipal = Elementos2.Elementos.Agua;
                }
                selectButtonPressed = false;
            }
        }
        
    }

    private void LockScripts()
    {
        if (!monolitoActivo)
        {
            gameObject.GetComponent<Character>().enabled = false;
            gameObject.GetComponent<Flames_FP>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Flames_FP>().enabled = true;
            gameObject.GetComponent<Character>().enabled = true;
        }
    }

    public void OnSelectButton(InputValue input)
    {
        if (!selectButtonPressed)
        {
            selectButtonPressed = input.isPressed;
        }
    }
}
