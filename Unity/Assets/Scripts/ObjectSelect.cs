using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
    private static GameObject selectedObject;
    private static GameObject hoveredObject;

    private void Update()
    {
        CheckObject();
    }

    void CheckObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (Input.GetMouseButtonDown(0))
            {
                selectedObject = hit.transform.gameObject;
            }

            hoveredObject = hit.transform.gameObject;
        }
        else
        {
            hoveredObject = null;
        }
    }

    public static GameObject GetSelectedObject() => selectedObject;
    public static GameObject GetHoveredObject() => hoveredObject;
}
