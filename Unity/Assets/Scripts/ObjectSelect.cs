using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
    private GameObject selectedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
            

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
              
         CheckObject();
        }

    }

    void CheckObject()
    {
         int layer_maskt = LayerMask.GetMask("Pion");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000,layer_maskt))
        {
            Debug.Log(hit.transform.name);
            selectedObject = hit.transform.gameObject;
            Debug.Log("hit");
        }
    }
}
