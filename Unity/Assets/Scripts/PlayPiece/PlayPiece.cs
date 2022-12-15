using Grid;
using System.Collections.Generic;
using UnityEngine;

namespace Piece
{   

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
            StartCoroutine(ObjectTransitioner.MoveObject(transform, position, 0.5f));
        }

        public void OnRemove()
        {
            // Delete this playpiece.
            Destroy(gameObject);
        }

        public void OnAttach()
        {

        }

        [Header("The element the playpiece will have.")]
        public ElementType Element;

        [Header("The player that owns this piece.")]
        public int OwnerPlayerIndex;

        public IntVector2 CurrentCoordinate { get; set; }
        public MovementGrid Grid { get; set; }
    }

}