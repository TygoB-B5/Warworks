using UnityEngine;

namespace Grid
{

    public class GridTile : MonoBehaviour
    {
        /// <summary>
        /// Set the tile color with index.
        /// </summary>
        /// <param name="type"></param>
        public void SetTileType(int type)
        {
            Material material = GetComponent<MeshRenderer>().material;

            // Temporary.
            switch (type)
            {
                case 0: material.SetColor("_Color", Color.white); return;
                case 1: material.SetColor("_Color", Color.blue); return;
                case 2: material.SetColor("_Color", Color.red); return;
                case 3: material.SetColor("_Color", new Color(225, 225, 0)); return;
            }
        }
    }
}
