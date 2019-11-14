using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	private float moveHorizontal;
	private float moveVertical;
	private Vector2 axis;
	private Rigidbody rb;
	private Vector3 movement;

	private Vector3 moveDirection;
    private Vector3 desiredDirection;

	public float speed;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		desiredDirection = transform.right * axis.x * speed;

        /*
		moveDirection = new Vector3(desiredDirection.x, 
            moveDirection.y,
            desiredDirection.z);
			*/
	}

    void FixedUpdate ()
	{
		//Movimiento
		
		axis.x = Input.GetAxis("Horizontal");														
		axis.y = Input.GetAxis("Vertical");

		//movement = new Vector3 (axis.x, 0.0f, axis.y);
		movement = new Vector3 (desiredDirection.x, 0.0f, desiredDirection.z);

		rb.AddForce(movement * speed);

		
	}
}
