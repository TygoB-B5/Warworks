using System.Collections.Generic;
using UnityEngine;

namespace Grid
{

    /// <summary>
    /// GridFileTypeList class used to change areas of tiles.
    /// </summary>
    public class GridTileTypeList
    {
        [Header("Center coordinate of the color map.")]
        public IntVector2 Center;

        [Header("The size of the tile type map.")]
        public int Size;

        [Header("The data of the tile map.")]
        public List<int> TileTypesList;
    }


    /// <summary>
    /// IGridMovable interface used for objects that should be movable on the grid.
    /// </summary>
    public interface IGridMovable
    {
        /// <summary>
        /// Called by grid system on request move to position.
        /// </summary>
        /// <param name="position"></param>
        public void OnRequestMoveToPosition(Vector3 position);

        /// <summary>
        /// Return the Tile type list. Tile type lists are used to specify which tiles should change upon doing an action.
        /// </summary>
        /// <returns></returns>
        public GridTileTypeList GetGridTileTypeList();
    }

    public class MovementGrid : MonoBehaviour
    {
        private void Start()
        {
            InstantiateTiles();
            InstantiateGridMovables();
        }

        public void InstantiateGridMovables()
        {
            // TODO
            m_Movables = new IGridMovable[GridSize.x * GridSize.y];
        }

        public void InstantiateTiles()
        {
            // Don't run if grid size is invalid
            if (GridSize.x * GridSize.y < 0)
                return;

            m_Tiles = new GridTile[GridSize.x * GridSize.y];

            // Instantiate tiles for the grid.
            for (int i = 0; i < m_Tiles.Length; i++)
            {
                m_Tiles[i] = Instantiate(TilePrefab, GetPositionWithIndex(i), Quaternion.identity, transform);
                m_Tiles[i].name = $"Tile{i}";
            }
        }

        /// <summary>
        /// Get the position in world space from grid coordinate space.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Vector3 GetPositionWithCoordinate(IntVector2 coordinate)
        {

            // Assert if coordinate is higher than the maximum grid size.
            if (coordinate.x >= GridSize.x || coordinate.y > GridSize.y)
            {
                Debug.LogError("Index is higher than grid size.");
                return Vector3.zero;
            }

            return transform.position + new Vector3(coordinate.x * GridDistance, 0, coordinate.y * GridDistance);
        }

        /// <summary>
        /// Get grid world position by grid index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector3 GetPositionWithIndex(int index)
        {

            // Assert if coordinate is higher than the maximum grid size.
            if (index >= GridSize.x * GridSize.y)
            {
                Debug.LogError("Index is higher than grid size.");
                return Vector3.zero;
            }

            return GetPositionWithCoordinate(new IntVector2(index % GridSize.x, index / GridSize.x));
        }

        /// <summary>
        /// Get IGridMovable with grid coordinate.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public IGridMovable GetGridMovableWithCoordinate(IntVector2 coordinate)
        {

            // Assert if coordinate is higher than the maximum grid size.
            if (coordinate.x >= GridSize.x || coordinate.y > GridSize.y)
            {
                Debug.LogError("Index is higher than grid size.");
                return null;
            }

            return m_Movables[coordinate.x * GridSize.x + coordinate.y];
        }

        /// <summary>
        /// Assign IGridMovable to coordinate on the grid.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="movable"></param>
        public void SetGridMovablePositionWithCoordinate(IntVector2 coordinate, IGridMovable movable)
        {

            // Assert if coordinate is higher than the maximum grid size.
            if (coordinate.x >= GridSize.x || coordinate.y > GridSize.y)
            {
                Debug.LogError("Index is higher than grid size.");
                return;
            }

            m_Movables[coordinate.x * GridSize.x + coordinate.y] = movable;
        }

        /// <summary>
        /// Request an IGridMovable to move to a position.
        /// </summary>
        /// <param name="movable"></param>
        /// <param name="coordinate"></param>
        public void MoveGridMovableToCoordinate(IGridMovable movable, IntVector2 coordinate)
        {
            movable.OnRequestMoveToPosition(GetPositionWithCoordinate(coordinate));
        }

        /// <summary>
        /// Get tile reference from grid coordinate.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public GridTile GetTileWithCoordinate(IntVector2 coordinate)
        {
            if (coordinate.x >= GridSize.x || coordinate.y > GridSize.y)
            {
                Debug.LogError("Index is higher than grid size.");
                return null;
            }

            return m_Tiles[coordinate.x * GridSize.x + coordinate.y];
        }

        /// <summary>
        /// Set tile type from grid coordinate.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="tileType"></param>
        public void SetTileTypeWithCoordinate(IntVector2 coordinate, int tileType)
        {

            // Assert if coordinate is higher than the maximum grid size.
            if (coordinate.x >= GridSize.x || coordinate.y > GridSize.y)
            {
                Debug.LogError("Index is higher than grid size.");
                return;
            }

            GetTileWithCoordinate(coordinate).SetTypeType(tileType);
        }

        // Public variables.

        [Header("Used to define the size of the grid.")]
        public IntVector2 GridSize;

        [Header("Used to define the distance between tiles.")]
        public float GridDistance;

        [Space]

        [Header("Prefab that will be used as tiles.")]
        public GridTile TilePrefab;

        // Private variables.

        private GridTile[] m_Tiles;
        private IGridMovable[] m_Movables;
    }
}