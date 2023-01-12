using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
    [SerializeField]
    private static GameObject selectedObject;
    private float holdTime = 0;

    private void Update()
    {
        CheckObject();
        if (Input.GetMouseButton(0))
        {
            holdTime += 1 * Time.deltaTime;
        }
    } 

    void CheckObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (holdTime < 0.5f)
                {
                    if (hit.transform.TryGetComponent(out ITracable trb))
                    {
                        trb.OnSelect();
                    }
                }
                    holdTime = 0;
            }
        }
    }
}
