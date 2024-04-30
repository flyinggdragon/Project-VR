using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GyroTouchControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    private Vector2 touchStartPos;
    private Vector2 touchDelta;

    public GameObject lixo; // Referência ao objeto "Lixo"

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
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

        cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        rot = new Quaternion(0, 0, 1, 0);

        return true;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }

        HandleTouchInput();

    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Trash"))
                        {
                            GameManager.score++;
                            Debug.Log("Lixo Coletado!");
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    touchDelta = touch.position - touchStartPos;
                    cameraContainer.transform.Rotate(Vector3.up, -touchDelta.x * 0.1f, Space.World);
                    cameraContainer.transform.Rotate(Vector3.right, touchDelta.y * 0.1f, Space.World);
                    touchStartPos = touch.position;
                    break;

                default:
                    break;
            }
        }
    }
}

/*

GIROSCÓPIO E MOVIMENTAÇÃO COM TOUCHSCREEN FUNCIONANDO

public class GyroTouchControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    private Vector2 touchStartPos;
    private Vector2 touchDelta;

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
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

        cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        rot = new Quaternion(0, 0, 1, 0);

        return true;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }

        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchDelta = touch.position - touchStartPos;
                    cameraContainer.transform.Rotate(Vector3.up, -touchDelta.x * 0.1f, Space.World);
                    cameraContainer.transform.Rotate(Vector3.right, touchDelta.y * 0.1f, Space.World);
                    touchStartPos = touch.position;
                    break;

                default:
                    break;
            }
        }
    }
}

*/

/*

GIROSCÓPIO FUNCIONANDO

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {

        gyro = Input.gyro;
        gyro.enabled = true;

        cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        rot = new Quaternion(0, 0, 1, 0);

        return true;

    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }
}
*/