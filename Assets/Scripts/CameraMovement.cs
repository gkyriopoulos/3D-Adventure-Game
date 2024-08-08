using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float rotationY;
    private float rotationX;
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField]
    private float mouseSensitivity = 1f;

    [SerializeField]
    private Transform target;

    /**
    For third person camera:
    1. distanceFromTarget: 3.5f;
    2. heightFromTarget: 1f;
    3. smoothTime: 0.01f;
    4. rotationMin: -25;
    5. rotationMax: 25;
    For first person camera:
    1. distanceFromTarget: 0f;
    2. heightFromTarget: 1.5f;
    3. smoothTime: 0.01f;
    4. rotationMin: -25;
    5. rotationMax: 25;
    **/
    [SerializeField]
    private float distanceFromTarget;

    [SerializeField]
    private float heightFromTarget;

    [SerializeField]
    private float smoothTime = 0.01f;

    [SerializeField]
    private int rotationMin = -25;
    [SerializeField]
    private int rotationMax = 25;
    
    void Start(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;   
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, rotationMin, rotationMax);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);

        transform.localEulerAngles = currentRotation;

        //Calculate distance for target and set the camera
        transform.position = target.position - transform.forward * distanceFromTarget;
        //Calculate height for target and set the camera
        transform.position = transform.position + Vector3.up * heightFromTarget;

    }
}