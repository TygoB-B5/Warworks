using Gameplay;
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
        private void OnEnable()
        {
            // Validate if patterns are right size.
            AttackPattern.Valid();
            MovementPattern.Valid();

            m_Piece = GetComponent<PlayPiece>();
        }

        /// <summary>
        /// Place movement pattern on grid with piece coordinate.
        /// </summary>
        public void PlaceMovementPattern()
        {
            PlaceMovementPattern(m_Piece.CurrentCoordinate);
        }

        /// <summary>
        /// Place attack pattern on grid with piece coordinate.
        /// </summary>
        public void PlaceAttackPattern()
        {
            PlaceAttackPattern(m_Piece.CurrentCoordinate);
        }

        /// <summary>
        /// Place movement pattern on grid with coordinate.
        /// </summary>
        public void PlaceMovementPattern(IntVector2 overrideCoordinate)
        {
            // Only update pattern if the piece is owned by the active player.
            if (GameManager.ActivePlayer == m_Piece.GetOwnerIndex())
            {
                m_Piece.Grid?.ResetTileTypes();
                m_Piece.Grid?.AddTilePatternToTiles(MovementPattern, overrideCoordinate);
            }
        }

        /// <summary>
        /// Place attack pattern on grid with coordinate.
        /// </summary>
        public void PlaceAttackPattern(IntVector2 overrideCoordinate)
        {
            // Only update pattern if the piece is owned by the active player.
            if (GameManager.ActivePlayer == m_Piece.GetOwnerIndex())
            {
                m_Piece.Grid?.ResetTileTypes();
                m_Piece.Grid?.AddTilePatternToTiles(AttackPattern, overrideCoordinate);
            }
        }

        private PlayPiece m_Piece;

        public TilePattern MovementPattern;
        public TilePattern AttackPattern;
    }

}