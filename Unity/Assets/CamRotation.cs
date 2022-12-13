using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    private Transform cam;
    private Transform piviot;
    private Vector3 localRotation = new Vector3(0,35,0); //(Y,X,Z)
    private Vector3 localPosition = new Vector3(0, 0, 0);
    float cameraDistance;

    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private float scrollSensitvity = 2f;
    [SerializeField] private float rotationDampening = 10f;
    [SerializeField] private float scrollDampening = 6f;

    private void Start()
    {
        cam = transform;
        piviot = transform.parent;
        cameraDistance = Vector3.Distance(cam.position, piviot.position);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) RotateBoard();
        if (Input.GetKey(KeyCode.Mouse1)) MoveBoard();
        ZoomBoard();

       
        //if (Input.GetKey(KeyCode.P))
        //{
        //    localRotation = new Vector3(180, 35, 0); //(Y,X,Z)
        //}

        //if (Input.GetKey(KeyCode.O))
        //{
        //    localRotation = new Vector3(0, 35, 0); //(Y,X,Z)
        //}


        //set camera rotation
        Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        piviot.rotation = Quaternion.Lerp(piviot.rotation, QT, Time.deltaTime * rotationDampening);

        //set camera zoom
        if (cam.localPosition.z != Mathf.Round(cameraDistance) * -1f)
        {
            cam.localPosition = new Vector3(0f, 0f, Mathf.Lerp(cam.localPosition.z, cameraDistance * -1f, Time.deltaTime * scrollDampening));
        }

        //set camera position
        piviot.position = new Vector3(localPosition.x, 0f, localPosition.y);

    }


    void RotateBoard()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            //get mouse position
            localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
            localRotation.y += -Input.GetAxis("Mouse Y") * mouseSensitivity;

            //Clamp the y Rotation to horizon and not flipping over at the top
            if (localRotation.y < 15f)
                localRotation.y = 15f;
            else if (localRotation.y > 90f)
                localRotation.y = 90f;
        }
        
    }

    void MoveBoard()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            localPosition.x += Input.GetAxis("Mouse X") * mouseSensitivity;
            localPosition.y += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

    }

    void ZoomBoard()
    {
        //Zooming Input from our Mouse Scroll Wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitvity;

            ScrollAmount *= cameraDistance * 0.3f;

            cameraDistance += ScrollAmount * -1f;

            cameraDistance = Mathf.Clamp(cameraDistance, 4f, 30f);
        }
    }





}
