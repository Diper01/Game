using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    [Header("Components")]
    public Rigidbody rb;
    public Transform cameraTransform;
    public Inventory inventory;

    private Vector3 movement;
    private float mouseX;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (cameraTransform == null) cameraTransform = Camera.main.transform;
        if (inventory == null) inventory = GetComponent<Inventory>();

        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
       
        GetInput();

       
        RotateWithCamera();
    }

    void FixedUpdate()
    {
        
        MovePlayer();
    }

    void GetInput()
    {
      
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

       
        movement = new Vector3(horizontal, 0, vertical).normalized;

       
        mouseX = Input.GetAxis("Mouse X");
    }

    void MovePlayer()
    {
        if (rb != null && movement.magnitude >= 0.1f)
        {
           
            Vector3 moveDirection = cameraTransform.TransformDirection(movement);
            moveDirection.y = 0;
            moveDirection.Normalize();

            
            Vector3 moveVelocity = moveDirection * moveSpeed;
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        }
        else if (rb != null)
        {
           
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void RotateWithCamera()
    {
       
        if (cameraTransform != null)
        {
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;

            if (cameraForward.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    rotationSpeed * Time.deltaTime);
            }
        }
    }

    
    void OnDrawGizmos()
    {
       
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);

        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
