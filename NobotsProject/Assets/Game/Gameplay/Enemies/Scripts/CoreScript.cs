using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour
{
	public GameManager gm;

	public Material[] material;
	Renderer rend;

	public float hitCd = 5;
	private float invul;

	public bool recentlyHit;

	void Start()
	{
		//Para cambiar material
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[0];

		recentlyHit = false;
	}

	void Update()
	{
		//Cooldown entre golpes
		if(recentlyHit == true)
		{
			invul += Time.deltaTime;
			if(invul >= hitCd)
			{
				recentlyHit = false;
				rend.sharedMaterial = material[0];
				invul = 0;
			}
		}
	}

	//Registrar cuando se ha impactado
	public void CoreHit()
	{
		if (recentlyHit == false)
		{
			rend.sharedMaterial = material[1];
			gm.CoreDamage(10);
			//BallController.currentSpeed = currentSpeed * Vector3 back;
			recentlyHit = true;
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
			CoreHit();
        }
    }
}
