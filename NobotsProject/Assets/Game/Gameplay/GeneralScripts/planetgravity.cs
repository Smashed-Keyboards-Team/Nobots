using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetgravity : MonoBehaviour
{
    [SerializeField] GameObject planet;
    [SerializeField] float gravityForce = 9.81f;

    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce((rb.position - planet.transform.position).normalized * gravityForce);
    }
}
