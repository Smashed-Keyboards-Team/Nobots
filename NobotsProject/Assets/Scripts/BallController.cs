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

		Vector3 forward = transform.position - cameraPosition.position;
        forward.y = 0;
        Vector3 right = Vector3.Cross(Vector3.up, forward);


        moveDirection = (forward * axis.y) + (right * axis.x);



        Debug.DrawRay(transform.position, forward, Color.blue);
		Debug.DrawRay(transform.position, right, Color.green);
        Debug.DrawRay(transform.position, Vector3.up, Color.red);

        //Vector de movimiento
        //moveDirection = new Vector3 (dir.normalized.x * axis.x, 0.0f, axis.y);

        //Añadir fuerza al Rigidbody(movimiento basado en fisicas)
        rb.AddForce(moveDirection * currentSpeed);
		//myTransform.Translate(moveDirection * speed, Space.Self);
	}
}
