using Grid;
using Piece;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            // Singleton design pattern.
            if (s_Instance == null)
            {
                s_Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

        }

        private void Start()
        {
            m_Grid = FindObjectOfType<MovementGrid>();

            // Spawn all play pieces and attach object selected event to it.
            List<PlayPiece> pieces = FillGrid.SpawnPieces();
            pieces.ForEach(p => { p.OnObjectSelected += OnPieceSelected; });

            // Attach tile selected event.
            m_Grid.OnTileSelected += OnTileSelected;

            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            m_ActivePlayer = 0;

            while (true)
            {
                yield return new WaitForEndOfFrame();

                // Select pawn.
                m_SelectedPiece = null;
                yield return new WaitUntil(() => m_SelectedPiece);

                // Select tile
                m_SelectedTile = null;
                yield return new WaitUntil(() => m_SelectedTile);

                bool isValidMove = true;

                // Try to move the object if it is movable. Else reset the tile list.
                if(!m_Grid.SetGridMovablePositionWithCoordinate(m_SelectedTile.Coordinate, m_SelectedPiece))
                {
                    isValidMove = false;
                    m_Grid.ResetTileTypes();
                }

                if(isValidMove)
                {
                    ProcessAttack();
                    SwitchTurns();
                }
            }
        }

        private void SwitchTurns()
        {
            m_ActivePlayer = m_ActivePlayer == 0 ? 1 : 0;
        }

        private void ProcessAttack()
        {
            List<IGridMovable> movables = m_Grid.GetMovablesInsidePattern(m_SelectedPiece.GetComponent<PlayPiecePattern>().AttackPattern, m_SelectedTile.Coordinate);

            // Loop through all movables that are in the tile pattern.
            foreach (IGridMovable movable in movables)
            {
                // If movable is from opposite team attack it.
                if (movable.This.GetComponent<PlayPiece>().GetOwnerIndex() != ActivePlayer)
                {
                    Kill(movable);
                }
            }
        }

        private void Kill(IGridMovable movable)
        {
            m_Grid.RemoveGridMovableFromGridWithCoordinate(movable.CurrentCoordinate);
        }

        private void OnPieceSelected(PlayPiece piece)
        {
            // If the owner is the piece is the current active player set it as the selected piece.
            if(piece.GetOwnerIndex() == ActivePlayer)
            {
                m_SelectedPiece = piece;
                m_SelectedPiece.GetComponent<PlayPiecePattern>().PlaceMovementPattern();
            }
        }

        private void OnTileSelected(GridTile tile)
        {
            // Ignore if piece isnt selected.
            if(!m_SelectedPiece)
            {
                return;
            }

            if (m_Grid.IsCoordinateInsidePattern(m_SelectedPiece.GetComponent<PlayPiecePattern>().MovementPattern, m_SelectedPiece.CurrentCoordinate, tile.Coordinate))
            {
                m_SelectedTile = tile;
                m_SelectedPiece.GetComponent<PlayPiecePattern>().PlaceAttackPattern(m_SelectedTile.Coordinate);
            }
        }

        public static int ActivePlayer => s_Instance.m_ActivePlayer;
        private int m_ActivePlayer;
        private PlayPiece m_SelectedPiece;
        private GridTile m_SelectedTile;
        private MovementGrid m_Grid;


        private static GameManager s_Instance;
    }
}