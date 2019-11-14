using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private BipedMove bipedMove;
    private Vector2 axis;
    private CameraMove cameraMove;
    private Vector2 mouseDirection;
    private MouseLock mouseLock;
    private Camera myCamera;
    private bool pause;
    public GameManager gm;
    private Coroutine smoothFOV;

	// Use this for initialization
	void Start () {
        bipedMove = GameObject.FindGameObjectWithTag("Player").
            GetComponent<BipedMove>();

        cameraMove = GameObject.FindGameObjectWithTag("Player").
            GetComponent<CameraMove>();


        mouseLock = GetComponent<MouseLock>();

        mouseLock.LockCursor();

        myCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        axis.x = Input.GetAxis("Horizontal");
        axis.y = Input.GetAxis("Vertical");

        mouseDirection.x = Input.GetAxis("Mouse X");
        mouseDirection.y = Input.GetAxis("Mouse Y");


        if(Input.GetButtonDown("Run"))
        {
            bipedMove.SetRun();
            if(smoothFOV == null)
            {
                smoothFOV = StartCoroutine(SmoothFOV(80));
            }
           else{
               StopCoroutine(smoothFOV);
               smoothFOV = StartCoroutine(SmoothFOV(80));
           }
        }

        if(Input.GetButtonUp("Run"))
        {
            bipedMove.SetWalk();
            if(smoothFOV == null)
            {
                smoothFOV = StartCoroutine(SmoothFOV(60));
            }
           else{
               StopCoroutine(smoothFOV);
               smoothFOV = StartCoroutine(SmoothFOV(60));
           }
        }


		if(Input.GetButtonDown("Fire1"))                             
        {

        }


        cameraMove.SetMouseDirection(mouseDirection);

        bipedMove.SetAxis(axis);

		
        if (Input.GetKeyDown(KeyCode.Escape))														//Pausa
        {
            mouseLock.ShowCursor();
            pause = !pause;
            
            gm.SetPause(pause);
        }
		
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseLock.LockCursor();
        }

        if (Input.GetButton("Jump"))
            bipedMove.Jump();
    }

    private IEnumerator SmoothFOV(float finalFOV)
    {
        float t = 0;

        while(true)
        {
            if(myCamera.fieldOfView != Mathf.Epsilon + finalFOV)
            {
                myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, finalFOV, t);
                t += 0.5f * Time.deltaTime;
            }
            else
            {
                smoothFOV = null;

                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
    
}
