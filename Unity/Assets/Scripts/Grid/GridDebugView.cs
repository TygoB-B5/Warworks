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

                // Get grid instance.
                MovementGrid grid = GetComponent<MovementGrid>();

                if (grid.TilePrefab)
                {
                    ClearChildren();
                    grid.InstantiateTiles();
                }
            }
        }

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