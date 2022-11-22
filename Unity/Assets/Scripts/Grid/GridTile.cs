using UnityEngine;

namespace Grid
{

    public class GridTile : MonoBehaviour
    {
        public void SetTypeType(int type)
        {
            Material material = GetComponent<MeshRenderer>().sharedMaterial;

            // Temporary.
            switch (type)
            {
                case 0: material.SetColor("_Color", Color.blue); return;
                case 1: material.SetColor("_Color", Color.red); return;
                case 2: material.SetColor("_Color", new Color(225, 225, 0)); return;
            }
        }
    }
}
