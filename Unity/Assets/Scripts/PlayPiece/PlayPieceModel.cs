using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piece
{

    [RequireComponent(typeof(PlayPiece))]
    public class PlayPieceModel : MonoBehaviour
    {
        private void Start()
        {
            LoadModel();
        }

        /// <summary>
        /// Load playpiece model based on player owner index.
        /// </summary>
        public void LoadModel()
        {
            // Delete model if exists.
            if(m_CurrentModel)
            {
                Destroy(m_CurrentModel);
            }

            // Log error if index is out of range.
            if (ModelsForPlayerIndex.Length < GetComponent<PlayPiece>().OwnerPlayerIndex)
            {
                Debug.LogError($"Models can not be loaded for Player Owner Index. Index: {GetComponent<PlayPiece>().OwnerPlayerIndex}", this);
            }

            // Get model gameObjectl
            GameObject model = ModelsForPlayerIndex[GetComponent<PlayPiece>().OwnerPlayerIndex];

            // Spawn model gameObject.
            m_CurrentModel = Instantiate(model, transform);
        }

        [Header("Models used per player index.")]
        public GameObject[] ModelsForPlayerIndex;

        private GameObject m_CurrentModel;
    }

}