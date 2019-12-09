using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool isBall;

	private bool dead = false;

	private Vector2 mouseDirection;
	private Vector2 axis;
	private bool pause;
	public float gravity = Physics.gravity.y;
	public float gravityMagnitude = 1;
	public Mesh[] meshes;

	private Transform cameraPosition;
	private Transform myTransform;
	private Rigidbody rb;
	private MeshFilter meshFilter;
	private CharacterController characterController;
	public GameManager gm;
	public HUD hud;

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
		//Pillar Componentes
		rb = GetComponent<Rigidbody>();
		characterController = GetComponent<CharacterController>();
		meshFilter = GetComponent<MeshFilter>();

        hud.mouseLock.LockCursor();

		cameraPosition = Camera.main.transform;

		myTransform = transform;

		currentSpeed = baseSpeed;
	}

	void Update ()
	{
		//CONTROL MODO CENTINELA
		if(isBall == false)
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
			}

			desiredDirection = transform.right * axis.x * centinelSpeed + transform.forward * axis.y * centinelSpeed;

			centinelMoveDirection = new Vector3(desiredDirection.x, centinelMoveDirection.y, desiredDirection.z);

			characterController.Move(centinelMoveDirection * Time.deltaTime);

			SetAxis(axis);

			//Cambio de forma
			if(Input.GetButtonDown("Swap"))
			{
				Debug.Log("otako culio");
				isBall = true;
				rb.isKinematic = false;
				meshFilter.mesh = meshes[0];
			}
		}
		
		
		//Función de pausa
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            hud.mouseLock.ShowCursor();
            pause = !pause;

            gm.SetPause(pause);
        }
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
					gm.turboCurrentCd = 0;
					gm.Turbo();
				}
			}
			else
			{
				if(currentSpeed >= baseSpeed)
				{
                    currentSpeed = currentSpeed - speedDecay;
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

			//Cambio de forma
			if(Input.GetButtonDown("Swap"))
			{
				isBall = false;
				rb.isKinematic = true;
				Debug.Log("pinche");
				meshFilter.mesh = meshes[1];
			}
		}
	}

	private void OnTriggerEnter(Collider collision)
	{
        if (collision.tag == "Bullet")
        {
			dead = true;
			hud.mouseLock.ShowCursor();
			gm.GameOver(dead);
			Destroy(gameObject);
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
