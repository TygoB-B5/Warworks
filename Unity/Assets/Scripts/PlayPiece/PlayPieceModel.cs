using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace Piece
{

    [RequireComponent(typeof(PlayPiece))]
    public class PlayPieceModel : MonoBehaviour
    {

        /// <summary>
        /// Attach event.
        /// </summary>
        private void OnEnable()
        {
            PlayPiece piece = GetComponent<PlayPiece>();

            if (piece)
            {
                piece.OnOwnerIndexChanged += LoadModel;
            }
        }

        /// <summary>
        /// Detach event.
        /// </summary>
        private void OnDisable()
        {
            PlayPiece piece = GetComponent<PlayPiece>();

            if (piece)
            {
                piece.OnOwnerIndexChanged -= LoadModel;
            }
        }

        /// <summary>
        /// Load playpiece model based on player owner index.
        /// </summary>
        public void LoadModel(PlayPiece piece, int ownerIndex)
        {
            // Delete model if exists.
            if(m_CurrentModel)
            {
                Destroy(m_CurrentModel);
            }

            // Log error if index is out of range.
            if (ModelsForPlayerIndex.Length < ownerIndex)
            {
                Debug.LogError($"Models can not be loaded for Player Owner Index. Index: {ownerIndex}", this);
            }

            // Get model gameObject.
            GameObject model = ModelsForPlayerIndex[ownerIndex];

            // Spawn model gameObject.
            m_CurrentModel = Instantiate(model, transform);

            // Set layermask to pawn layer.
            m_CurrentModel.layer = 3;
        }

        [Header("Models used per player index.")]
        public GameObject[] ModelsForPlayerIndex;

        private GameObject m_CurrentModel;
    }

}