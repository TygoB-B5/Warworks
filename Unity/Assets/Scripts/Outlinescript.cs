using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlinescript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor = 0.0f;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;
    private GameObject outlineObject;
    private Renderer rend;

    void Start()
    {
        outlineObject = Instantiate(this.gameObject, transform.position, Quaternion.identity, transform);
        rend = outlineObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        outlineRenderer.enabled = true;
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", -scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<Outlinescript>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;
    }
}
