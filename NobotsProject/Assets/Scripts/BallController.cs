using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	private float moveHorizontal;
	private float moveVertical;
	private Vector2 axis;
	private Rigidbody rb;
	private Vector3 moveDirection;

	private Transform cameraPosition;
	private Transform myTransform;

	public GameManager gm;

	private bool rotating;

	public float baseSpeed;
	public float maxSpeed;
	public float speedIncrease;
	public float speedDecay;
	public float currentSpeed;

	public float gravity = Physics.gravity.y;

	void Start ()
	{
		//Pillar Rigidbody
		rb = GetComponent<Rigidbody>();

		cameraPosition = Camera.main.transform;

		myTransform = transform;

		currentSpeed = baseSpeed;
	}

    void FixedUpdate ()
	{	
		if(Input.GetButton("Jump"))
		{
			if (currentSpeed <= maxSpeed && gm.turbo > 1)
			{
				currentSpeed = currentSpeed + speedIncrease;
				Debug.Log("turbo on");
				gm.Turbo();
			}
		}
		else
		{
			if(currentSpeed >= baseSpeed)
			{
				currentSpeed = currentSpeed - speedDecay;
				Debug.Log("turbo off");
			}
		}
		
		//Inputs de movimiento en X y Y
		axis.x = Input.GetAxisRaw("Horizontal");														
		axis.y = Input.GetAxisRaw("Vertical");

		//CamDirection();
		//Move();

		Vector3 dir = transform.position - cameraPosition.position;
		dir.y = 0;

		if(dir.normalized != transform.forward.normalized)
		{
			if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			{
				transform.rotation = Quaternion.LookRotation(dir);
				rotating = true;
			}
			else if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
			{
				rotating = false;
			}
		}
		/*
		IsJumping();

		if (!IsJumping)
		{
			Jump();
		}
		else
		{
			jumpSpeed -= gravity * Time.deltaTime;
		}
		*/

		Debug.DrawRay(transform.position, dir, Color.blue);
		Debug.DrawRay(transform.position, transform.forward, Color.green);

		//Vector de movimiento
		moveDirection = new Vector3 (dir.normalized.x * axis.x, 0.0f, axis.y);

		//Añadir fuerza al Rigidbody(movimiento basado en fisicas)
		rb.AddForce(moveDirection * currentSpeed);
		//myTransform.Translate(moveDirection * speed, Space.Self);
	}
}
