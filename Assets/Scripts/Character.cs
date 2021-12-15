using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    //Jump
    [SerializeField] float jumpVelocity;
    [SerializeField] float jumpForce = 10.0f;

    [SerializeField] float movementSpeed;
    [SerializeField] float SpeedGrabbingSomethin = 3f;
    [SerializeField] float dashSpeed;

    [Range(0, 10)]
    public float speedRotation;

    [Range(0, 1)]
    public float dashTime;

    [Range(0, 2)]
    public float dashCoolDown;

    public Vector3 groundMovement;
    //Variables de movimiento
    public float actualSpeed = 0f;
    private float gravityValue = 14.0f;

    //Boleanas de movimiento
    bool jump = false;
    bool dash;
    bool groundedPlayer;
    bool onPauseButton = false;

    //Vectores
    private Vector2 moveVector;

    Collider col;

    //referencias
    CharacterController controller;
    PickUpObjects picking;
    Animator characterAnimatorController;

    //referencias Scripts
    GodMode godMode;

    Character character;
    PickUpObjects pickUpObjects;
    Elementos2 elementos2;
    Flames_FP flames_FP;


    bool selectButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        picking = GetComponent<PickUpObjects>();
        characterAnimatorController = GetComponent<Animator>();
        //godMode.enabled = false;

        col = GetComponent<Collider>();
        col.enabled = false;
    }
        
    private void FixedUpdate()
    {
        //Guardamos la info de la boleana isGrounded que tiene implementada el CharacterController en la variable groundedPlayer.
        groundedPlayer = controller.isGrounded;
        characterAnimatorController.SetBool("isGrounded", groundedPlayer);

        //if(groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        //Asignamos la main camera al nombre de "Camera".
        Transform camera = Camera.main.transform;

        //Pasamos los valores recibidos del Vector2 del New System Input a un Vector3.
        groundMovement = Vector3.zero;
        groundMovement.x = moveVector.x;
        groundMovement.z = moveVector.y;

        //Reasignamos el vector del mundo al mismo que nuestra camara.
        Vector3 movementDirection = camera.TransformDirection(groundMovement);
        Vector3 movementDirectionOnPlane = Vector3.ProjectOnPlane(movementDirection, Vector3.up);
        movementDirection.Normalize();

        //Aplicamos movimiento con el CharacterController.
        controller.Move(movementDirectionOnPlane * actualSpeed * Time.deltaTime);
        movementDirection.x -= gravityValue * Time.deltaTime;
        characterAnimatorController.SetFloat("characterSpeed", actualSpeed);

        //Aplicamos rotacion al personaje.
        Vector3 desiredDirection = movementDirectionOnPlane;
        Vector3 currentDirection = transform.right;

        float angle = Vector3.SignedAngle(currentDirection, desiredDirection, Vector3.up);

        Quaternion rotationApply = Quaternion.AngleAxis(angle * Time.deltaTime * speedRotation, Vector3.up);
        transform.rotation = rotationApply * transform.rotation;

        //Logica salto
        if (groundMovement == new Vector3(0, 0, 0))
        {
            actualSpeed = 0f;                 
        }

        if (groundedPlayer)
        {
            jumpVelocity = -gravityValue * Time.deltaTime;
            if (jump)
            {
                jumpVelocity = jumpForce;
                jump = false;
            }
        }
        else
        {
            jumpVelocity -= gravityValue * Time.deltaTime;
        }

        Vector3 jumpVector = new Vector3(0, jumpVelocity, 0);
        controller.Move(jumpVector * Time.deltaTime);

        //Cuando se activa el dash llamamos a la funcion DashCoroutine la cual contiene la logica de este.
        if (dash)
        {
            StartCoroutine(DashCoroutine());
        }

        if (Keyboard.current.f12Key.wasPressedThisFrame)
        {
            godMode.enabled = true;

            character.enabled = false;
            pickUpObjects.enabled = false;
            elementos2.enabled = false;
            flames_FP.enabled = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("AHHHHHHHHG");
        }

        if (other.CompareTag("Victory"))
        {
            SceneManager.LoadScene("Victory_Scene");
        }
        else if (other.CompareTag("Loosing"))
        {
            SceneManager.LoadScene("Losing_Scene");
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
