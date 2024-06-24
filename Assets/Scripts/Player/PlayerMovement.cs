using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Quaternion rot;
    private Gyroscope gyro;
    private bool gyroEnabled;

    private float minVerticalAngle = -80f; // Ângulo mínimo para a rotação vertical
    private float maxVerticalAngle = 80f;  // Ângulo máximo para a rotação vertical

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found.");
            return;
        }

        gyroEnabled = EnableGyro();

        if (!gyroEnabled)
        {
            Debug.LogWarning("Gyroscope not enabled or not supported on this device.");
        }
        else
        {
            Debug.Log("Gyroscope enabled successfully.");
        }

        rot = new Quaternion(0, 0, 1, 0);

        Debug.Log("PlayerMovement script started.");
    }

    private bool EnableGyro()
    {
        if (!SystemInfo.supportsGyroscope)
        {
            Debug.Log("Gyroscope not supported on this device.");
            return false;
        }

        gyro = Input.gyro;
        gyro.enabled = true;

        Debug.Log("Gyroscope enabled.");

        return true;
    }

    void Update()
    {
        if (gyroEnabled)
        {
            mainCamera.transform.localRotation = gyro.attitude * rot;
            ClampVerticalRotation();
        }

        HandleTouchInput();
        HandleMouseInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    RaycastHit hit;
                    Ray ray = mainCamera.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Fish"))
                        {
                            Debug.Log("Fish touched!");
                        }
                        else if (hit.collider.CompareTag("Trash"))
                        {
                            Debug.Log("Trash touched!");
                            GameManager.score++;
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    Vector2 touchDelta = touch.deltaPosition;
                    mainCamera.transform.Rotate(Vector3.up, -touchDelta.x * 0.1f, Space.World);
                    mainCamera.transform.Rotate(Vector3.right, touchDelta.y * 0.1f, Space.World);
                    ClampVerticalRotation();
                    break;
            }
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            mainCamera.transform.Rotate(Vector3.up, mouseX * 5f, Space.World);
            mainCamera.transform.Rotate(Vector3.right, -mouseY * 5f, Space.World);
            ClampVerticalRotation();
        }
    }

    private void ClampVerticalRotation()
    {
        Vector3 eulerAngles = mainCamera.transform.localEulerAngles;
        float clampedX = ClampAngle(eulerAngles.x, minVerticalAngle, maxVerticalAngle);
        mainCamera.transform.localEulerAngles = new Vector3(clampedX, eulerAngles.y, eulerAngles.z);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -180f)
        {
            angle += 360f;
        }
        if (angle > 180f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }
}


