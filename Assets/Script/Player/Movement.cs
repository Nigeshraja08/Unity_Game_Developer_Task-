using System;
using Unity.Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform groundCheck;
    [SerializeField] float radius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpSpeed;
    [SerializeField] float mouseSentivity;
    
    Animator animator;
    PlayerInputs playerInputs;
    Rigidbody rb;
    GameManager gameManager;
 
    Vector3 upDirection; // Character's new "up"

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerInputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.isGameActive) return;
        ChangeGravity();
        ChangeMovement();
        Jump();
        Look();
       
    }

    private void ChangeMovement()
    {
        float horizontal = playerInputs.move.x;
        float vertical = playerInputs.move.y;

        if (horizontal == 0 && vertical == 0)
        {
            animator.SetBool("IsRunning", false);
            return;
        }

        Vector3 gravityDir = Physics.gravity.normalized;
        Vector3 upDirection = -gravityDir; // Character's new "up"
        Vector3 forward = Vector3.Cross(transform.right, upDirection); // Forward based on gravity

        Vector3 moveDirection = (horizontal * transform.right + vertical * forward).normalized;

        // Rotate character smoothly while maintaining gravity alignment
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, upDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

        if(IsGrounded())
        {
            // Move character
            transform.position += moveDirection * Time.deltaTime * 5f;
            // Play running animation
            animator.SetBool("IsRunning", true);
        }             
    }

    void Jump()
    {
        if(IsGrounded())
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
               
                rb.AddForce(upDirection * jumpSpeed , ForceMode.Impulse);
            }
        }
    }

    private void ChangeGravity()
    {
        Vector3 gravityDir = Physics.gravity.normalized; // Get gravity direction
         upDirection = -gravityDir; // Opposite of gravity (new "up")

        // Preserve forward direction while aligning to new gravity
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, upDirection) * transform.rotation;

        // Smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);

        // Update Cinemachine Camera to match new gravity
        CinemachineBrain cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        if (cinemachineBrain != null)
        {
            cinemachineBrain.WorldUpOverride = transform;
        }
    }

    bool IsGrounded()
    {
        if( Physics.CheckSphere(groundCheck.position, radius, groundLayer))
        {
            animator.SetBool("IsGrounded", true);
            return true;
        }
        animator.SetBool("IsGrounded", false);
        return false;

    }

    private void OnDrawGizmos()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.red; // Red color for visibility
            Gizmos.DrawWireSphere(groundCheck.position, radius);
        }
    }


    void Look()
    {
        float mouseX = playerInputs.look.x * mouseSentivity * Time.deltaTime;
        float mouseY = playerInputs.look.y * mouseSentivity * Time.deltaTime;

        // Rotate player left/right
        transform.Rotate(Vector3.up * mouseX);

       
    }

}
