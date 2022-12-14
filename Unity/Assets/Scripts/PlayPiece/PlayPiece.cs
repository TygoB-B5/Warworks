using Grid;
using UnityEngine;
using System;

namespace Piece
{   

    public class PlayPiece : MonoBehaviour, IGridMovable, ITracable
    {
        /// <summary>
        /// Element types a playpiece can have.
        /// </summary>
        public enum ElementType
        {
            Air, Water, Earth, Fire
        }

        private void Start()
        {
            // Create new collider and attach it.
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.size = ColliderSize;
            collider.center = ColliderCenter;
        }

        public void OnRequestMoveToPosition(Vector3 position)
        {
            StartCoroutine(ObjectTransitioner.MoveObject(transform, position, 0.5f));
            OnPieceMove(this);
        }

        public void OnRemove()
        {
            // Yeet it off the map. >:3
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 15 + new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f)) * 15, ForceMode.Impulse);

            // Destroy the object after a small delay so it doesn't clutter up the playfield if it lands on it.
            Destroy(gameObject, 3.5f);
        }

        public void OnAttach()
        {

        }

        /// <summary>
        /// Runs when object is selected.
        /// </summary>
        public void OnSelect()
        {
            OnObjectSelected(this);
        }

        /// <summary>
        /// Runs when object is being hovered over.
        /// </summary>
        public void OnHover()
        {
            OnObjectHovered(this);
        }

        /// <summary>
        /// Set player owner index.
        /// </summary>
        public void SetOwnerIndex(uint index)
        {
            OwnerIndex = (int)index;
            OnOwnerIndexChanged(this, OwnerIndex);
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

        [Header("The size of the collider.")]
        [SerializeField] private Vector3 ColliderSize;
        [SerializeField] private Vector3 ColliderCenter;

        public IntVector2 CurrentCoordinate { get; set; }
        public MovementGrid Grid { get; set; }
        public GameObject This => gameObject;

        public event Action<PlayPiece, int> OnOwnerIndexChanged = delegate { };
        public event Action<PlayPiece> OnPieceMove = delegate { };

        public event Action<PlayPiece> OnObjectSelected = delegate { };
        public event Action<PlayPiece> OnObjectHovered = delegate { };
    }

}