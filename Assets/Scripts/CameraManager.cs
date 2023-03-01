using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 cameraRotation;
    public float mouseSensitivity = 100.0f;
    public Vector2 maxminAngle = new Vector2(-90.0f, 90.0f);

    void Awake()
    {
        cameraTransform = this.transform;
        // cameraRotation = Vector3.zero;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the mouse cursor in the game window
        Cursor.visible = false; // Hide the mouse cursor
    }

    void Update()
    {
        // Get the mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate the camera rotation based on the mouse input
        cameraRotation.x -= mouseY;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, maxminAngle.x, maxminAngle.y); // Clamp the rotation angle
        cameraRotation.y += mouseX;

        // Apply the camera rotation to the camera transform
        cameraTransform.localRotation = Quaternion.Euler(cameraRotation);
    }
}
