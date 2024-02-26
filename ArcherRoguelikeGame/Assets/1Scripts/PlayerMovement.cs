using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float currentSpeed;
    Vector2 moveInput;
    Vector3 inputVector;
    PlayerInput playerInput;
    [SerializeField] float speed;
    [SerializeField] float diagonalMultiplier;
    [SerializeField] float verticalMultiplier;

    [SerializeField] TMP_Text speedText;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        HandleDash();
        speedText.text = rb.velocity.magnitude.ToString("F0");
    //    Debug.Log(currentSpeed);
    }

    private void FixedUpdate()
    {
        Move();
        RotateTowardsMouse();
    }

    private void GatherInput()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
       
        //     float horizontalInput = Input.GetAxisRaw("Horizontal");
        //     float verticalInput = Input.GetAxisRaw("Vertical");
        /*  if (horizontalInput != 0 && verticalInput != 0) //diagonal movement
          {
              inputVector = new Vector3(horizontalInput, 0, verticalInput).normalized;
              currentSpeed = speed * diagonalMultiplier;
          }
          else
          {
              inputVector = new Vector3(horizontalInput, 0, verticalInput * verticalMultiplier);
              currentSpeed = speed;
          } */

        /*   if (moveInput.x != 0 && moveInput.y != 0) //diagonal movement
           {
               inputVector = new Vector3(moveInput.x, 0, moveInput.y).normalized;
               currentSpeed = speed * diagonalMultiplier;
           }
           else
           {
               inputVector = new Vector3(moveInput.x, 0, moveInput.y * verticalMultiplier);
               currentSpeed = speed;
           } */

        inputVector = new Vector3(moveInput.x, 0, moveInput.y*verticalMultiplier);
        currentSpeed = speed;
    }

    private void Move()
    {
        if (!isDashing)
            rb.velocity = inputVector.ToIso() * currentSpeed;
    }

    private void RotateTowardsMouse()
    {
        Ray cameraRay;              // The ray that is cast from the camera to the mouse position
        RaycastHit cameraRayHit;    // The object that the ray hits

        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); //çalýþýyor ama raycastli
        LayerMask mask = LayerMask.GetMask("Plane");
        // If the ray strikes an object...
        if (Physics.Raycast(cameraRay, out cameraRayHit, 1000, mask))
        {

            {
                //   aimHelperTarget = cameraRayHit.point;
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
                //    aimHelper.transform.position = cameraRayHit.point;
            }
        }

        //   float smoothness = 0.5f; // Adjust the smoothness factor as needed
        //    aimHelper.transform.position = Vector3.Lerp(aimHelper.transform.position, aimHelperTarget, smoothness);

    }

    #region Dash
    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength;

    [SerializeField] private bool isDashing;
    private float timeStartedDash;
    private Vector3 dashDirection;
    [SerializeField] ParticleSystem dashEffect;
    [SerializeField] float dashCd;
    bool canDash = true;
    private bool hasDashed;
    private Vector3 dashStartPosition;
    private float dashTimer = 0f;
    private void HandleDash()
    {
        HandleDashInput();

        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            PerformDash();
        }
    }

    private void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasDashed && canDash)
        {
            StartDash();
        }
    }
    private void StartDash()
    {
        if (inputVector != Vector3.zero)
        {
            dashStartPosition = transform.position;
            dashDirection = inputVector.ToIso();

            {
                //    dashDirection = transform.forward;

            }
            //      StartCoroutine(StartDashCooldown());
            //     dashEffect.Play();
            dashTimer = 0f;
            hasDashed = true;
            isDashing = true;
            //       timeStartedDash = Time.time;
            //      Debug.Log("Started dash");
        }

    }

    private void PerformDash()
    {
        rb.velocity = dashDirection * dashSpeed;

     /*   if (Time.time >= timeStartedDash + dashLength)
        {
            EndDash();
        } */

        if (dashTimer >= dashLength)
        {
            EndDash();
        }
    }

    private void EndDash()
    {
        float distanceMoved = Vector3.Distance(transform.position, dashStartPosition);
        Debug.Log("Distance moved during dash: " + distanceMoved);
 //       Debug.Log("Stopped dash");
        isDashing = false;
        rb.velocity = Vector3.zero;
        hasDashed = false;
    }

    #endregion
}











public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}