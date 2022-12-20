using Grid;
using System;
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
            OnPieceMove();
        }

        public void OnRemove()
        {
            // Delete this playpiece.
            Destroy(gameObject);
        }

        public void OnAttach()
        {

        }

        /// <summary>
        /// Set player owner index.
        /// </summary>
        public void SetOwnerIndex(uint index)
        {
            OwnerIndex = (int)index;
            OnOwnerIndexChanged(OwnerIndex);
        }

        /// <summary>
        /// Get player owner index.
        /// </summary>
        /// <returns></returns>
        public int GetOwnerIndex()
        {
            return OwnerIndex;
        }

        [Header("The element the playpiece will have.")]
        public ElementType Element;

        [Header("The player that owns this piece.")]
        [SerializeField] private int OwnerIndex;

        public IntVector2 CurrentCoordinate { get; set; }
        public MovementGrid Grid { get; set; }

        public event Action<int> OnOwnerIndexChanged = delegate { };
        public event Action OnPieceMove = delegate { };
    }

}