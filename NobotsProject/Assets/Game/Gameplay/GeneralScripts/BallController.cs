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
		moveDirection = (forward * axis.y) + (right * axis.x);

        /*
		Debug.DrawRay(transform.position, forward, Color.blue);
		Debug.DrawRay(transform.position, right, Color.green);
        Debug.DrawRay(transform.position, Vector3.up, Color.red);
		*/

        //Añadir fuerza al Rigidbody(movimiento basado en fisicas)
        rb.AddForce(moveDirection.normalized * currentSpeed);
	}
}
