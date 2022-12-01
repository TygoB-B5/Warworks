using Grid;
using System.Collections.Generic;
using UnityEngine;

namespace Piece
{

    /// <summary>
    /// GridFileTypeList class used to change areas of tiles.
    /// </summary>
    [System.Serializable]
    public class TilePattern
    {
        [Header("The size of the tile type map.")]
        public int Size;

        [Header("The data of the tile map.")]
        public List<int> TileTypesList;
    }

    public class PlayPiece : MonoBehaviour, IGridMovable
    {
        /// <summary>
        /// Element types a playpiece can have.
        /// </summary>
        public enum ElementType
        {
            Air, Water, Earth, Fire
        }

        public void OnRequestMoveToPosition(Vector3 position)
        {
            Debug.Log("pos changed");
            transform.position = position;
        }

        public void OnRemove()
        {
            // Delete this playpiece.
            Destroy(gameObject);
        }

        public void OnAttach()
        {

        }

        // TEMPT
        private void Update()
        {
            if(Input.GetKey(KeyCode.A))
            {
                Grid?.ResetTileTypes();
                Grid?.AddTilePatternToTiles(AttackPattern, CurrentCoordinate);
                return;
            }

            if (Input.GetKey(KeyCode.B))
            {
                Grid?.ResetTileTypes();
                Grid?.AddTilePatternToTiles(MovementPattern, CurrentCoordinate);
                return;
            }
        }

        [Header("The element the playpiece will have.")]
        public ElementType Element;

        public TilePattern AttackPattern;
        public TilePattern MovementPattern;

        public IntVector2 CurrentCoordinate { get; set; }
        public MovementGrid Grid { get; set; }
    }

}