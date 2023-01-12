using Grid;
using Piece;
using System.Collections.Generic;
using UnityEngine;
namespace Gameplay
{
    public class FillGrid : MonoBehaviour
    {
        private void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
            }
        }

        /// <summary>
        /// Spawns the gameplay play pieces on to the grid.
        /// </summary>
        public static List<PlayPiece> SpawnPieces()
        {
            List<PlayPiece> pieces = new List<PlayPiece>();

            // Spawns and puts play pieces in to a simple pattern
            for (int i = 0; i < 10; i++)
            {
                // Bottom 2 rows.
                {
                    PlayPiece firePiece = Instantiate(s_Instance.m_FireballMovable, s_Instance.transform);
                    PlayPiece earthPiece = Instantiate(s_Instance.m_EarthMovable, s_Instance.transform);

                    FindObjectOfType<MovementGrid>().AddGridMovableToGridWithCoordinate(new IntVector2(i, 0), firePiece);
                    FindObjectOfType<MovementGrid>().AddGridMovableToGridWithCoordinate(new IntVector2(i, 1), earthPiece);

                    firePiece.SetOwnerIndex(0);
                    earthPiece.SetOwnerIndex(0);

                    pieces.Add(firePiece);
                    pieces.Add(earthPiece);
                }

                // Top 2 rows.
                {
                    PlayPiece firePiece = Instantiate(s_Instance.m_FireballMovable, s_Instance.transform);
                    PlayPiece earthPiece = Instantiate(s_Instance.m_EarthMovable, s_Instance.transform);

                    FindObjectOfType<MovementGrid>().AddGridMovableToGridWithCoordinate(new IntVector2(i, 9), firePiece);
                    FindObjectOfType<MovementGrid>().AddGridMovableToGridWithCoordinate(new IntVector2(i, 8), earthPiece);

                    firePiece.SetOwnerIndex(1);
                    earthPiece.SetOwnerIndex(1);

                    pieces.Add(firePiece);
                    pieces.Add(earthPiece);
                }
            }

            return pieces;
        }

        private static FillGrid s_Instance;
        public PlayPiece m_FireballMovable, m_EarthMovable;
    }
}