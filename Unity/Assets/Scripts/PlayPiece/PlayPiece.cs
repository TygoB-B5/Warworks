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
            transform.position = position;
        }

        public TilePattern GetGridTileTypeList()
        {
            return AttackPattern;
        }

        public void OnRemove()
        {
            Destroy(gameObject);
        }

        public void OnSpawn()
        {
            // TODO
        }

        [Header("The element the playpiece will have.")]
        public ElementType Element;

        public TilePattern AttackPattern;
        public TilePattern MovementPattern;

        public IntVector2 CurrentCoordinate { get; set; }
    }

}