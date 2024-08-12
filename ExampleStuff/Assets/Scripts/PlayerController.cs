using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = 15f;
    
    private CharacterController controller;
    private float velocityY;

    private void Awake()
    {
        //try get reference to CharacterController component
        if (TryGetComponent(out controller) == false)
        {
            ///print message if no CharacterController found
            ///on GameObject this script is attached to
            Debug.LogWarning($"{gameObject.name} requires a CharacterController component!");
        }
    }

    private void FixedUpdate()
    {
        if(controller != null)
        {
            if(controller.isGrounded == true)
            {
                velocityY = -gravity * Time.fixedDeltaTime;
            }
            else
            {
                velocityY -= gravity * Time.fixedDeltaTime;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(controller != null)
        {
            if(controller.isGrounded == true && Input.GetButtonDown("Jump") == true)
            {
                velocityY = jumpForce;
            }

            ApplyMoveStep();
        }
    }

    private void ApplyMoveStep()
    {
        Vector3 moveFrameStep = Vector3.zero;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveFrameStep += transform.forward * verticalInput;
        moveFrameStep += transform.right * horizontalInput;
        moveFrameStep = moveFrameStep.normalized * moveSpeed;
        moveFrameStep.y += velocityY;
        controller.Move(moveFrameStep * Time.deltaTime);
    }
}
