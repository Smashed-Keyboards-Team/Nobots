using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool isBall;

	private Vector2 mouseDirection;
    private MouseLock mouseLock;
	private Vector2 axis;
	private bool pause;
	public float gravity = Physics.gravity.y;
	public float gravityMagnitude = 1;

	private Transform cameraPosition;
	private Transform myTransform;
	private Rigidbody rb;
	private CharacterController characterController;
	public GameManager gm;

	[Header("Centinel Mode")]
	public float centinelSpeed;
	public float jumpForce = 5;

	private bool jump;
	private Vector3 centinelMoveDirection;
    private Vector3 desiredDirection;

    [Header("Ball Mode")]
	public float baseSpeed;
	public float maxSpeed;
	public float speedIncrease;
	public float speedDecay;
	public float currentSpeed;

	private float moveHorizontal;
	private float moveVertical;
	private Vector3 ballMoveDirection;

	void Start ()
	{
		//Pillar Rigidbody
		rb = GetComponent<Rigidbody>();
		characterController = GetComponent<CharacterController>();
		mouseLock = GetComponent<MouseLock>();

        mouseLock.LockCursor();

		cameraPosition = Camera.main.transform;

		myTransform = transform;

		//isBall = false;

		currentSpeed = baseSpeed;
	}

    void FixedUpdate ()
	{	
		//CONTROL MODO BOLA
		if (isBall)
		{
			//Funcionamiento del turbo
			if(Input.GetButton("Jump"))
			{
                if (currentSpeed <= maxSpeed && gm.turbo > 1)
				{
                    currentSpeed = currentSpeed + speedIncrease;
					//Debug.Log("turbo on");
					gm.turboCurrentCd = 0;
					gm.Turbo();
				}
			}
			else
			{
				if(currentSpeed >= baseSpeed)
				{
                    currentSpeed = currentSpeed - speedDecay;
					//Debug.Log("turbo off");
				}
			}

			//Ajustes de velocidad maxima y minima
			if (currentSpeed > maxSpeed)
			{
				currentSpeed = maxSpeed;
			}
			if (currentSpeed < baseSpeed)
			{
				currentSpeed = baseSpeed;
			}
		
			//Inputs de movimiento en X y Y
			axis.x = Input.GetAxisRaw("Horizontal");														
			axis.y = Input.GetAxisRaw("Vertical");

			//Vectores para determinar vector de direccion
			Vector3 forward = transform.position - cameraPosition.position;
			forward.y = 0;
			Vector3 right = Vector3.Cross(Vector3.up, forward);

			//Vector de direccion
			ballMoveDirection = (forward * axis.y) + (right * axis.x);

			/*
			Debug.DrawRay(transform.position, forward, Color.blue);
			Debug.DrawRay(transform.position, right, Color.green);
			Debug.DrawRay(transform.position, Vector3.up, Color.red);
			*/

			//Añadir fuerza al Rigidbody(movimiento basado en fisicas)
			rb.AddForce(ballMoveDirection.normalized * currentSpeed);
		}
		//CONTROL MODO CENTINELA
		else
		{
			axis.x = Input.GetAxis("Horizontal");
			axis.y = Input.GetAxis("Vertical");

			mouseDirection.x = Input.GetAxis("Mouse X");
			mouseDirection.y = Input.GetAxis("Mouse Y");

			if (!jump && characterController.isGrounded)
			{
				centinelMoveDirection.y = gravity;
			}
			else
			{
				jump = false;
				centinelMoveDirection.y += gravity * gravityMagnitude * Time.deltaTime;
				Debug.Log("ae");
			}

			desiredDirection = transform.right * axis.x * centinelSpeed + transform.forward * axis.y * centinelSpeed;

			centinelMoveDirection = new Vector3(desiredDirection.x, centinelMoveDirection.y, desiredDirection.z);

			characterController.Move(centinelMoveDirection * Time.deltaTime);

			SetAxis(axis);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
        {

            mouseLock.ShowCursor();
            pause = !pause;

            gm.SetPause(pause);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseLock.LockCursor();
        }
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
        centinelMoveDirection.y = jumpForce;
    }
}
