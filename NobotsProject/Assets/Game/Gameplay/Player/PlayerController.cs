using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool isBall;

	private bool dead = false;

    public Bouncer bounce;

	private Vector2 mouseDirection;
	private Vector2 axis;
	private bool pause;
	public Mesh[] meshes;

    private Transform cameraPosition;
	private Transform myTransform;
	public Rigidbody rb;
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
    public float gravity = Physics.gravity.y;
    public float gravityMagnitude = 1;

    [Header("Ball Mode")]
	public float baseSpeed;
	public float maxSpeed;
	public float speedIncrease;
	public float speedDecay;
	public float currentSpeed;
	public float turboBoost = 30;
	public float accelerationPlatMaxSpeed;
	public float accelerationPlatSpeedIncrease;
	private bool onAccelerationPlat = false;
	public float turboDuration = 3;
	public float turboCd = 1000000000000000;
	private float nextTurbo = 0;

	private float turboCurrentDuration = 0;

    private float moveHorizontal;
	private float moveVertical;
	private Vector3 ballMoveDirection;

	[Header("God Mode")]
	public GameObject godPanel;
	public bool godInvulnerable = false;
	public bool godFreeMovement = false;

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
        //Inputs de movimiento en X y Y
		axis.x = Input.GetAxisRaw("Horizontal");														
		axis.y = Input.GetAxisRaw("Vertical");

		//Pillar direccion del raton
		mouseDirection.x = Input.GetAxis("Mouse X");
		mouseDirection.y = Input.GetAxis("Mouse Y");
		
		//CONTROL MODO CENTINELA
        if (isBall == false && godFreeMovement == false)
		{
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
        }
		//CONTROL MODO BOLA UPDATE
		else if(isBall)
		{
			//Funcionamiento del turbo
			if(Time.time> nextTurbo && Input.GetKeyDown(KeyCode.Space))
			{
				if (gm.turbo >= 25)
				{
					gm.turbo -= 25;
					nextTurbo = Time.time + turboCd;
					//gm.TurboBarRefresh();
					currentSpeed += turboBoost;
					Debug.Log("he salido de turbo");
				}
			}
					
			if(currentSpeed >= baseSpeed && onAccelerationPlat == false)
			{
                currentSpeed -= speedDecay;
			}
			else if(currentSpeed >= maxSpeed && onAccelerationPlat == true)
			{
				currentSpeed -= (speedDecay * 2);
			}
			
			if(onAccelerationPlat == true)
			{
				currentSpeed += accelerationPlatSpeedIncrease;
			}

			//Ajustes de velocidad maxima y minima
			if (currentSpeed > maxSpeed && onAccelerationPlat == false)
			{
				currentSpeed = maxSpeed;
			}
			else if (currentSpeed > accelerationPlatMaxSpeed && onAccelerationPlat == true)
			{
				currentSpeed = accelerationPlatMaxSpeed;
			}
			else if (currentSpeed < baseSpeed)
			{
				currentSpeed = baseSpeed;
			}
			
		}

        //Cambio de forma
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isBall)
            {
                isBall = false;
                rb.isKinematic = true;
                meshFilter.mesh = meshes[1];
            }
            else
            {
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

		//ENTRAR EN GODMODE
		if (Input.GetKeyDown(KeyCode.F10))
        {
            Time.timeScale = 0f;
			hud.mouseLock.ShowCursor();
            godPanel.SetActive(true);
        }
	}

    void FixedUpdate ()
	{	
		//CONTROL MODO BOLA FIXEDUPDATE
		if (isBall)
		{
			//Vectores para determinar vector de direccion
			//Vector3 forward = transform.position;
			Vector3 forward = transform.position - cameraPosition.position;
			forward.y = 0;
			Vector3 right = Vector3.Cross(Vector3.up, forward);

			//Vector de direccion
			ballMoveDirection = (forward * axis.y) + (right * axis.x);

			//Añadir fuerza al Rigidbody(movimiento basado en fisicas)
            rb.AddForce(ballMoveDirection.normalized * currentSpeed);
																																							/*
																																							Debug.DrawRay(transform.position, forward, Color.blue);
																																							Debug.DrawRay(transform.position, right, Color.green);
																																							Debug.DrawRay(transform.position, Vector3.up, Color.red);
																																							*/
		}
	}

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Funcion del uso del turbo
	public void Turbo()
	{
		turboCurrentDuration = 0;
		while(turboCurrentDuration < turboDuration)
		{
			turboCurrentDuration += Time.deltaTime;
			currentSpeed += speedIncrease;
		}
		Debug.Log("he salido de turbo");
	}
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Impacto con balas
	private void OnTriggerEnter(Collider collision)
	{
        if (collision.tag == "Bullet" && gm.win == false && godInvulnerable == false)
        {
			PlayerDead();
        }      
    }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Colisiones
    private void OnCollisionEnter(Collision collision)
    {
        //Bouncer
		if (collision.transform.tag == "BouncerUp")
        {
            GameObject.FindGameObjectsWithTag("BouncerUp");
            //Rebote (solo vale para 1 cara) modificar
            if (collision.contactCount == 1)
            {
                Vector3 normal = collision.contacts[0].normal;
                Debug.DrawRay(collision.transform.position, normal, Color.green, 5f);
                rb.AddForce(normal * bounce.bounceForce);
            }
        }

		//Plataforma de aceleracion
		if (collision.transform.tag == "TurboPlatform")
        {
            onAccelerationPlat = true;
        }
		else
		{
			onAccelerationPlat = false;
		}
    }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void SetAxis(Vector2 direction)
    {
        axis = direction;
    }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void Jump()
    {
        if (jump || !characterController.isGrounded)
            return;

        jump = true;
        centinelMoveDirection.y = jumpForce;
    }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void PlayerDead()
	{
		dead = true;
		hud.mouseLock.ShowCursor();
		gm.GameOver(dead);
		Destroy(gameObject);
	}
}
