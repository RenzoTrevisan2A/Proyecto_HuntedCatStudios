using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    public float actualSpeed = 0f;
    [SerializeField] float movementSpeed;

    [Range(0, 10)]
    public float speedRotation;

    private float gravityValue = -9.81f;

    //Vectores
    private Vector2 moveVector;
    public Vector3 playerVelocity;

    CharacterController controller;
    Collider col;

    //referencias Scripts
    GodMode godMode;

    Character character;
    PickUpObjects pickUpObjects;
    Elementos2 elementos2;
    Flames_FP flames_FP;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    private void FixedUpdate()
    {
        //Asignamos la main camera al nombre de "Camera".
        Transform camera = Camera.main.transform;

        //Pasamos los valores recibidos del Vector2 del New System Input a un Vector3.
        Vector3 groundMovement = Vector3.zero;
        groundMovement.x = moveVector.x;
        groundMovement.z = moveVector.y;

        //Reasignamos el vector del mundo al mismo que nuestra camara.
        Vector3 movementDirection = camera.TransformDirection(groundMovement);
        Vector3 movementDirectionOnPlane = Vector3.ProjectOnPlane(movementDirection, Vector3.up);
        movementDirection.Normalize();

        //Aplicamos movimiento con el CharacterController.
        controller.Move(movementDirectionOnPlane * actualSpeed * Time.deltaTime);
        movementDirection.x += gravityValue * Time.deltaTime;

        if (Keyboard.current.spaceKey.isPressed)
        {
            controller.Move(Vector3.up * actualSpeed * Time.deltaTime);
        }
        else if (Keyboard.current.shiftKey.isPressed)
        {
            controller.Move(Vector3.up * -actualSpeed * Time.deltaTime);
        }

        //Aplicamos rotacion al personaje.
        Vector3 desiredDirection = movementDirectionOnPlane;
        Vector3 currentDirection = transform.right;

        float angle = Vector3.SignedAngle(currentDirection, desiredDirection, Vector3.up);

        Quaternion rotationApply = Quaternion.AngleAxis(angle * Time.deltaTime * speedRotation, Vector3.up);
        transform.rotation = rotationApply * transform.rotation;

        //Setea la velocidad a 0 si no nos estamos moviendo.
        if (groundMovement == new Vector3(0, 0, 0))
        {
            actualSpeed = 0f;
        }

        if (Keyboard.current.f12Key.wasPressedThisFrame)
        {
            godMode.enabled = false;

            character.enabled = true;
            pickUpObjects.enabled = true;
            elementos2.enabled = true;
            flames_FP.enabled = true;
        }
    }
}
