using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class ThirdPersonController : MonoBehaviour
{
    //these set the names of the perameters to something consistent "const string" = "consitent word"
    private const string speedParamName = "Speed";
    private const string jumpParamName = "Jump";
    private const string groundedParamName = "Grounded";
    private const string fallingParamName = "Falling";
    // Doesn't allow for super tiny movements or preventing jitter
    private const float lookThreshold = 0.01f;
    
    
    [Header("Cinemachine")]
    [SerializeField] //GameObject with camera attached
    private Transform cameraTarget;

    [SerializeField] // Angle limits for how far camera can tilt up
    private float topClamp = 70.0f;

    [SerializeField] // Angle limits for how far camera can tilt down
    private float bottomClamp = -30.0f;
    
    [Header("Grounded")]
    [SerializeField] //GameObject that checks if on ground
    private Transform groundCheckPoint;

    [Header("Jump")]
    [SerializeField] //how powerful jump is
    private float jumpStrength = 7f;
    [SerializeField] //time after jumpng to jump again
    private float jumpDowntime = 1f;

    [SerializeField]
    private float groundCheckRadius = 0.2f;

    [SerializeField] //new layer (probably will use ground as layer to check for things)
    private LayerMask groundLayer;

    [Header("Speed")]
    [SerializeField]
    private float lookSpeed = 10f;

    [SerializeField]
    private float moveSpeed = 3f;
    
    
    // Get components, set Vector2 for moving and looking
   
    private Rigidbody body;
    private Animator animator;
    private Vector2 move;
    private Vector2 look;
    private float currentSpeed;

    //keeps track of looking and where camera faces. Yaw is for 'left and right'. Pitch is for 'up and down'.
    private float yaw;
    private float pitch;
   
    //Bools, states of player
    private bool isRunning;
    private bool isGrounded = true;
    private bool canJump = true;

    
    private void Awake()
    {
        //instantiaze the Rigidbody and the Animator
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
      
    }

    private void Update()
    {
        GroundedCheck();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }
    // the goal of this method is to start the player movement and then accelerate it to the max movespeed value. Feels nicer than just being at top speed.
    private void Move()
    { // finding target speed, if the player is running then then target speed will be 2x the movementspeed or will just be regular value
        // have to multiply by the magnitude of the move vector so moving diagonally is not faster than straight movement
        float targetSpeed = (isRunning ? moveSpeed * 2f : moveSpeed) * move.magnitude;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.fixedDeltaTime * 8f);

        Vector3 forward = cameraTarget.forward;
        Vector3 right = cameraTarget.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
       right.Normalize();

        Vector3 moveDirection = (forward * move.y + right * move.x).normalized;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);

            Vector3 currentVelocity = body.linearVelocity;
            body.linearVelocity = new Vector3(moveDirection.x * currentSpeed, currentVelocity.y, moveDirection.z * currentSpeed);
        }
        else
        {
            Vector3 currentVelocity = body.linearVelocity;
            body.linearVelocity = new Vector3(0, currentVelocity.y, 0);
        }

        float normalizedAnimSpeed = currentSpeed / (moveSpeed * 2f);
        animator.SetFloat(speedParamName, normalizedAnimSpeed);
        animator.SetBool(fallingParamName, !isGrounded && body.linearVelocity.y < -0.01f);
    }
    private void Jump()
    {
        if (!isGrounded ||  !canJump)
        {
            return;
        }


        //add upwards force to body multiplied by the jumpStrength, then after jumping, make it so you can't jump and animaton trigger.
        body.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        canJump = false;
        //after jumping start the jump delay coroutine.
        StartCoroutine(JumpDowntimeCoroutine());

        animator.SetTrigger(jumpParamName);
    }

   //Co-routine that allows for a delay of the isGrounded state. Co-routines allow you to set delays for code execution.
    private IEnumerator JumpDowntimeCoroutine()
    {
       //delay for player to be off the ground after co-routine starts
        yield return new WaitForSeconds(0.25f);

        //waits until state "IsGrounded" to be true, then waits for the jumpDowntime (which is 1f) and then makes it
        //so you can jump again
        var waitForGrounded = new WaitUntil(() => isGrounded);
        yield return new WaitForSeconds(jumpDowntime);
        canJump = true;
    }

    private void Look()
    { 
        // check if any meaningful input (checks to see if input is higher than the minimum lookThreshold)
        if(look.sqrMagnitude >= lookThreshold)
        { 
            // calculate how much to rotate per frame * the lookspeed
            float deltaTimeMultipier = Time.deltaTime * lookSpeed;
            yaw += look.x * deltaTimeMultipier;
            pitch += look.y * deltaTimeMultipier;
        }
        // Clamp both Yaw and Pitch, clamping Yaw with max and min float values
        // Clapm Pitch with bottom and top set earlier
        yaw = ClampAngle(yaw, float.MinValue, float.MaxValue);
        pitch = ClampAngle(pitch, bottomClamp, topClamp);
        // set new rotation on the camera target object
        cameraTarget.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    // New Method that clamps angles within certain ranges. Clamps restrict values within a specified min and max range.
    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        // lfAngle = local float angle
        // these if statements make it so that if the lfAngle goes under -360 degrees or above 360 degrees, sets it back to 360 degrees
        if (lfAngle < -360f)
        {
            lfAngle += 360f;
        }

        if (lfAngle > 360f)
        {
            lfAngle -= 360f;
        }
        // clamp within certain range and return it!
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    private void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
        animator.SetBool(groundedParamName, isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        if(groundCheckPoint == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckPoint.position, groundCheckRadius);
    }
    private void OnMove(InputValue inputValue)
    {
        move = inputValue.Get<Vector2>();

    }

    private void OnJump()
    {
        Jump();
    }

    private void OnRun(InputValue inputValue)
    {
        isRunning = inputValue.isPressed;
    }

    private void OnLook(InputValue inputValue)
    {
        look = inputValue.Get<Vector2>();
    }
}
