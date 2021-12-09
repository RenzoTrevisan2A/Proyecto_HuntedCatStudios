using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float jumpSpeed;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float movementSpeed;
    [SerializeField] float SpeedGrabbingSomethin = 3f;
    [SerializeField] float dashSpeed;

    [Range(0, 10)]
    public float speedRotation;

    [Range(0, 1)]
    public float dashTime;

    [Range(0, 2)]
    public float dashCoolDown;


    //Variables de movimiento
    public float actualSpeed = 0f;
    private float gravityValue = -9.81f;

    //Boleanas de movimiento
    bool jump = false;
    bool dash;
    bool groundedPlayer;
    bool onPauseButton = false;

    //Vectores
    private Vector2 moveVector;
    public Vector3 playerVelocity;

    //referencias
    CharacterController controller;
    PickUpObjects picking;
    Animator characterAnimatorController;

    bool selectButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        picking = GetComponent<PickUpObjects>();
        characterAnimatorController = GetComponent<Animator>();
    }
        
    private void FixedUpdate()
    {
        //Guardamos la info de la boleana isGrounded que tiene implementada el CharacterController en la variable groundedPlayer.
        groundedPlayer = controller.isGrounded;
        characterAnimatorController.SetBool("isGrounded", groundedPlayer);

        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

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
        characterAnimatorController.SetFloat("characterSpeed", actualSpeed);

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

        //mecanica dede Salto.
        if (jump && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            characterAnimatorController.SetTrigger("Jumping");
        }
        else if (jump && !groundedPlayer)
        {
            jump = false;
        }

        //le aplicamos gravedad al salto.
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        characterAnimatorController.SetFloat("characterSpeedY", playerVelocity.y);

        //Cuando se activa el dash llamamos a la funcion DashCoroutine la cual contiene la logica de este.
        if (dash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    //Funcion con la logica del dash.
    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time; // need to remember this to know how long to dash

        while (Time.time < startTime + dashTime)
        {
            controller.Move(transform.right * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }
        while (Time.time < startTime + dashCoolDown)
        {
            dash = false;
            yield return null;
        }
    }

    //Las siguientes funciones son las que reciben la info de los inputs.
    private void OnMove(InputValue input)
    {
        moveVector = input.Get<Vector2>();

        if (picking.iHaveARock != true && picking.ImDraggingSomething == true)
        {
            actualSpeed = SpeedGrabbingSomethin;
        }
        else if (picking.iHaveARock == true && picking.ImDraggingSomething != true)
        {
            actualSpeed = SpeedGrabbingSomethin;
        }
        else
        {
            actualSpeed = movementSpeed;
        }

        actualSpeed = movementSpeed;
    }

    private void OnDash(InputValue input)
    {
        dash = input.isPressed;
    }

    private void OnPauseButton(InputValue input)
    {
        onPauseButton = input.isPressed;
    }

    private void OnJump(InputValue input)
    {
        if (groundedPlayer)
        {
            jump = input.isPressed;
        }
    }

    private void OnSelectButton(InputValue input)
    {
        selectButtonPressed = input.isPressed;
    }

    private void OnAnimatorMove()
    {
        
    }
}
