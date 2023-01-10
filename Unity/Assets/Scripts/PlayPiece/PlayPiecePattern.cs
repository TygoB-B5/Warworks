using Piece;
using System.Collections;
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

        /// <summary>
        /// Checks if the TilePattern data is valid.
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            // Calculate if sizes match.
            if(Mathf.Pow(Size, 2) != TileTypesList.Count)
            {
                Debug.LogError($"Tile pattern size is invalid. Size: {Size}. does not match. TileTypeList: {TileTypesList.Count}");
                return false;
            }

            return true;
        }
    }


    [RequireComponent(typeof(PlayPiece))]
    public class PlayPiecePattern : MonoBehaviour
    {
        private void Start()
        {
            m_PlayPiece = GetComponent<PlayPiece>();

            // Validate if patterns are right size.
            AttackPattern.Valid();
            MovementPattern.Valid();

        }

        private void Update()
        {
            m_PlayPiece.Grid?.ResetTileTypes();

            if(ObjectSelect.GetSelectedObject() == transform.GetChild(0).gameObject)
            {
                m_PlayPiece.Grid?.AddTilePatternToTiles(MovementPattern, m_PlayPiece.CurrentCoordinate);
            }
        }


        public TilePattern AttackPattern;
        public TilePattern MovementPattern;

        private PlayPiece m_PlayPiece;
    }

}