using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Quaternion playerRot;
    private Quaternion cameraRot;
    private Transform playerTransform;
    private Transform cameraTransform;
    public bool smooth;
    public float smoothTime = 5;
    private Vector2 mouseDirection;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
        cameraTransform = Camera.main.transform;

        playerRot = playerTransform.localRotation;
        cameraRot = cameraTransform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        playerRot *= Quaternion.Euler(new Vector3(0, mouseDirection.x, 0));
        cameraRot *= Quaternion.Euler(new Vector3(-mouseDirection.y, 0, 0));

        if (smooth)
        {
            playerTransform.localRotation = Quaternion.Slerp(playerTransform.localRotation,
                playerRot,
                smoothTime);
            cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation,
                cameraRot,
                smoothTime);
        }
        else
        {
            playerTransform.localRotation = playerRot;
            cameraTransform.localRotation = cameraRot;
        }
    }

    public void SetMouseDirection(Vector2 direction)
    {
        mouseDirection = direction;
    }
}
