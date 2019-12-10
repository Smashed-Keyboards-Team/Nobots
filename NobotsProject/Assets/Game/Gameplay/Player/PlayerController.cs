﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool isBall;

	private bool dead = false;

	public GameObject godPanel;
	public bool godInvulnerable = false;
	public bool godFreeMovement = false;

    public BouncerUp bounceUp;

	private Vector2 mouseDirection;
	private Vector2 axis;
	private bool pause;
	public float gravity = Physics.gravity.y;
	public float gravityMagnitude = 1;
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
        if (isBall == false && godFreeMovement == false)
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

        //MOVIMIENTO FREE GODMODE
        else if (godFreeMovement == true)
		{
			axis.x = Input.GetAxis("Horizontal");
			axis.y = Input.GetAxis("Vertical");
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
		//CONTROL MODO BOLA
		if (isBall)
		{
			//Funcionamiento del turbo
			if(Input.GetButton("Jump"))
			{
                if (currentSpeed <= maxSpeed && gm.turbo > 1)
				{
                    currentSpeed += speedIncrease;
					gm.turboCurrentCd = 0;
					gm.Turbo();
				}
			}
			else
			{
				if(currentSpeed >= baseSpeed)
				{
                    currentSpeed -= speedDecay;
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
	}

	private void OnTriggerEnter(Collider collision)
	{
        if (collision.tag == "Bullet" && gm.win == false && godInvulnerable == false)
        {
			PlayerDead();
        }

       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "BouncerUp")
        {
            GameObject.FindGameObjectsWithTag("BouncerUp");
            Debug.Log("deberiasaltar");
            //Rebote (solo vale para 1 cara) modificar
            if (collision.contactCount == 1)
            {
                Vector3 normal = collision.contacts[0].normal;
                Debug.DrawRay(collision.transform.position, normal, Color.green, 5f);
                rb.AddForce(normal * bounceUp.bounceForce);
            }
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

	public void PlayerDead()
	{
		dead = true;
		hud.mouseLock.ShowCursor();
		gm.GameOver(dead);
		Destroy(gameObject);
	}
}
