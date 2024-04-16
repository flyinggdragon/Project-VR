using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMoviment : MonoBehaviour {
    /*
    private GameObject cameraContainer;
    private Quaternion rot;
    private Gyroscope gyro;

    void Start() {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyro = Input.gyro;

        cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        rot = new Quaternion(0, 0, 1, 0);
    }

    void Update() {
        transform.localRotation = gyro.attitude * rot;
        for (int i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Fish")
                    {
                        Debug.Log("Fish touched!");
                    } else if (hit.collider.tag == "Trash")
                    {
                        Debug.Log("Trash touched!");
                    }
                }
            }
        }
    }
    */
}