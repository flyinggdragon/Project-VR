using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;

    private float rotationSpeed = 5f; // Velocidade de rotação da câmera

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
    }

    void Update()
    {
        HandleTouchInput();
        HandleMouseInput();

        if (Input.GetKeyDown(KeyCode.I)) {
            GameManager.instance.collection.Toggle();
        }
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
                            GameManager.instance.score++;
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    break;
            }
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Rotacionar em torno do eixo vertical (Y) com o movimento do mouse horizontal
            transform.Rotate(Vector3.up, mouseX, Space.World);

            // Rotacionar em torno do eixo horizontal (X) invertido com o movimento do mouse vertical
            mainCamera.transform.Rotate(Vector3.right, -mouseY, Space.World);

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


