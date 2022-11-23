using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{

    float holdTime = 0;
    bool longClick = false;
    bool shortClick = false;

    private void Update()
    {
        //PressHoldDetection();
        //CustomRotate();


        if (Input.GetKey(KeyCode.Mouse0))
        {
            //if begin position is same as end position = click
            //if begin position is diffrent by 0.2f = drag
            //if = drag run void CustomRotate();
        }







    }

    //void PressHoldDetection()
    //{
    //    if (Input.GetKey(KeyCode.Mouse0))
    //    {
    //        holdTime = holdTime + 1 * Time.deltaTime;
    //    }

    //    if (Input.GetKeyDown(KeyCode.Mouse0) && holdTime > 0.4f)
    //    {
    //        longClick = true;
    //        Debug.Log("Longclick = true");
    //        holdTime = 0;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Mouse0) && holdTime < 0.4f)
    //    {
    //        shortClick = true;
    //        Debug.Log("ShortClick = true");
    //        holdTime = 0;
    //    }

    //    if (Input.GetKeyUp(KeyCode.Mouse0))
    //    {
    //        shortClick = false;
    //        longClick = false;
    //    }
    //}

    //void CustomRotate()
    //{
    //    if (longClick)
    //    {
    //        float x = Input.mousePosition.x;

    //        Quaternion rotation = Quaternion.Euler(0, x, 0);
    //        transform.rotation = rotation;
    //        //Debug.Log(x);

    //    }
    //}










}
