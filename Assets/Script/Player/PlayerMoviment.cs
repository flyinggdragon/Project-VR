using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMoviment : MonoBehaviour
{
    [SerializeField]private float velocity = 0.5f;
    [SerializeField]private float rotateX=0.5f, rotateY=0.5f, rotateZ=0.5f;

    //[SerializeField]private GameObject playerCam;

    private Vector3 currentRotation;

    void Start()
    {
        currentRotation = new Vector3(this.transform.rotation.x,this.transform.rotation.y,this.transform.rotation.z);
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(Screen.width/2-50,Screen.height/2-50,100,100),"Aim here!");
    }

    

    void Update()
    {
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            //transform.Rotate(0, -rotateY, 0, Space.Self);
            currentRotation += new Vector3(0,-rotateY,0); 
        }
        else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            //transform.Rotate(0, rotateY, 0, Space.Self);
            currentRotation += new Vector3(0, rotateY, 0);
        }
        else if (Input.GetKey("w") || Input.GetKey("up"))
        {
            //transform.Rotate(-rotateX, 0, 0, Space.Self);
            currentRotation += new Vector3(-rotateX,0,0);
        }
        else if (Input.GetKey("s") || Input.GetKey("down"))
        {
            //transform.Rotate(rotateX, 0, 0, Space.Self);
            currentRotation += new Vector3(rotateX, 0, 0);
        }
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        /*
        if (Input.GetMouseButton(0))
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
           


            
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit,Mathf.Infinity,layerMask))
                {
                if (hit.transform.tag == "Fish")
                {
                    Debug.Log("fish caught!");
                }
                else
                {
                    Debug.Log("fail!");
                }
            }
            else
            {
                Debug.Log("fail to aim!");
            }
            
        }*/


    }
}
