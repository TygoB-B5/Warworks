using UnityEngine;

namespace Grid
{

    [ExecuteInEditMode]
    [RequireComponent(typeof(MovementGrid))]
    public class GridDebugView : MonoBehaviour
    {
        private void Awake()
        {
            ClearChildren();
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                UpdateDebugGrid();
            }
        }

        private void OnDisable()
        {
            if (!Application.isPlaying)
            {
                ClearChildren();
            }
        }

        private void OnDestroy()
        {
            if (!Application.isPlaying)
            {
                ClearChildren();
            }
        }

        /// <summary>
        /// Updates the debug tile instances and resets them.
        /// </summary>
        private void UpdateDebugGrid()
        {
            // Get grid instance.
            MovementGrid grid = GetComponent<MovementGrid>();

            // Instantiate Tile prefabs for debugging if not null.
            if (grid.TilePrefab)
            {
                ClearChildren();
                grid.InstantiateTiles();
            }
        }


        /// <summary>
        /// Deletes all child objects inside of the grid objct.
        /// </summary>
        private void ClearChildren()
        {
            // Destroy all child objects of this transform.
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }
}