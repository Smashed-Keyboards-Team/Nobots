using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BipedMove : MonoBehaviour
{
    private CharacterController characterController;

    public float walkSpeed = 5;
    public float runSpeed = 15;
    private float speed;
    private Vector3 moveDirection;
    private Vector3 desiredDirection;
    private Vector2 axis;

    private bool jump;
    public float gravity = Physics.gravity.y;
    public float gravityMagnitude = 1;
    public float jumpForce = 5;


	// Use this for initialization
	void Start ()
    {
        characterController = GetComponent<CharacterController>();
        SetWalk();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!jump && characterController.isGrounded)
        {
            moveDirection.y = gravity;
        }
        else
        {
            jump = false;
            moveDirection.y += gravity * gravityMagnitude * Time.deltaTime;
        }

        desiredDirection = transform.right * axis.x * speed
            + transform.forward * axis.y * speed;

        moveDirection = new Vector3(desiredDirection.x, 
            moveDirection.y,
            desiredDirection.z);

        characterController.Move(moveDirection * Time.deltaTime);
	}

    public void SetAxis(Vector2 direction)
    {
        axis = direction;
    }

    public void Jump()
    {
        if (jump || !characterController.isGrounded)
            return;

        jump = true;
        moveDirection.y = jumpForce;
    }

    public void SetRun()
    {
        speed = runSpeed;
    }

    public void SetWalk()
    {
        speed = walkSpeed;
    }
}
