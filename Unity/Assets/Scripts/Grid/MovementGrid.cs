using Piece;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{

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
        /// Called when object is detached from the grid.
        /// </summary>
        public void OnRemove();

        /// <summary>
        /// Called when the object is attached to the grid.
        /// </summary>
        public void OnAttach();

        /// <summary>
        /// The curent coordinate of the playpiece.
        /// </summary>
        public IntVector2 CurrentCoordinate { get; set; }

        /// <summary>
        /// The Grid the object is attached to.
        /// </summary>
        public MovementGrid Grid { get; set; }

        /// <summary>
        /// Returns a reference to this gameobject.
        /// </summary>
        public GameObject This { get; }
    }

    public class MovementGrid : MonoBehaviour
    {
        private void Start()
        {
            InstantiateTiles();
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
                m_Tiles[i].Coordinate = GetCoordinateFromIndex(i);

                m_Tiles[i].OnTileSelected += ((tile) => OnTileSelected(tile));
                m_Tiles[i].OnTileHovered += ((tile) => OnTileHovered(tile));
            }
        }

        /// <summary>
        /// Get the position in world space from grid coordinate space.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Vector3 GetPositionWithCoordinate(IntVector2 coordinate)
        {
            if (!IsValidCoordinate(coordinate))
                return Vector3.zero;

            // Calculates position.
            return transform.position + new Vector3(coordinate.x * GridDistance, 0, coordinate.y * GridDistance);
        }

        /// <summary>
        /// Get grid world position by grid index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector3 GetPositionWithIndex(int index)
        {
            if (!IsValidIndex(index))
                return Vector3.zero;

            return GetPositionWithCoordinate(GetCoordinateFromIndex(index));
        }

        /// <summary>
        /// Returns the Grid coordinate based on index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IntVector2 GetCoordinateFromIndex(int index)
        {
            return new IntVector2(index % GridSize.x, index / GridSize.x);
        }

        /// <summary>
        /// Get IGridMovable with grid coordinate.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public IGridMovable GetGridMovableWithCoordinate(IntVector2 coordinate)
        {
            if (!IsValidCoordinate(coordinate))
                return null;

            return m_Movables[coordinate.x * GridSize.x + coordinate.y];
        }

        /// <summary>
        /// Assign IGridMovable to coordinate on the grid.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="movable"></param>
        public void AddGridMovableToGridWithCoordinate(IntVector2 coordinate, IGridMovable movable)
        {
            if (!IsValidCoordinate(coordinate))
                return;

            TryInitializingGridMovableArray();

            // Return false if coordinate is already assigned.
            if (m_Movables[coordinate.x * GridSize.x + coordinate.y] != null)
            {
                Debug.LogError($"Movable already at position: {coordinate}");
                return;
            }

            // Attach movable to grid.
            movable.OnAttach();
            movable.Grid = this;
            m_Movables[coordinate.x * GridSize.x + coordinate.y] = movable;

            // Update coordinate and request movement.
            movable.CurrentCoordinate = coordinate;
            movable.OnRequestMoveToPosition(GetPositionWithCoordinate(coordinate));
        }

        /// <summary>
        /// Remove IGridMovable from coordinate on the grid.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="movable"></param>
        public void RemoveGridMovableFromGridWithCoordinate(IntVector2 coordinate)
        {
            if (!IsValidCoordinate(coordinate))
                return;

            TryInitializingGridMovableArray();

            // Clear movable object from grid.
            m_Movables[coordinate.x * GridSize.x + coordinate.y].Grid = null;
            m_Movables[coordinate.x * GridSize.x + coordinate.y].OnRemove();
            m_Movables[coordinate.x * GridSize.x + coordinate.y] = null;
        }

        /// <summary>
        /// Request an IGridMovable to move to a position.
        /// </summary>
        /// <param name="movable"></param>
        /// <param name="coordinate"></param>
        public bool SetGridMovablePositionWithCoordinate(IntVector2 coordinate, IGridMovable movable)
        {
            if (!IsValidCoordinate(coordinate))
                return false;

            TryInitializingGridMovableArray();

            // Return false if coordinate is already assigned.
            if (m_Movables[coordinate.x * GridSize.x + coordinate.y] != null)
            {
                Debug.LogError($"Movable already at position: {coordinate}");
                return false;
            }

            // Move placable to right coordinate in memory.
            m_Movables[movable.CurrentCoordinate.x * GridSize.x + movable.CurrentCoordinate.y] = null;
            m_Movables[coordinate.x * GridSize.x + coordinate.y] = movable;

            // Update coordinate and request movement.
            movable.CurrentCoordinate = coordinate;
            movable.OnRequestMoveToPosition(GetPositionWithCoordinate(coordinate));

            return true;
        }
            
        /// <summary>
        /// Get tile reference from grid coordinate.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public GridTile GetTileWithCoordinate(IntVector2 coordinate)
        {
            if (!IsValidCoordinate(coordinate))
                return null;

            return m_Tiles[coordinate.x * GridSize.x + coordinate.y];
        }

        /// <summary>
        /// Set tile type from grid coordinate.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="tileType"></param>
        public void SetTileTypeWithCoordinate(IntVector2 coordinate, int tileType)
        {
            if (!IsValidCoordinate(coordinate))
                return;

            GetTileWithCoordinate(coordinate).SetTileType(tileType);
        }

        /// <summary>
        /// Resets all the tile types to 0.
        /// </summary>
        public void ResetTileTypes()
        {
            // Set all tile types to 0;
            foreach(var tile in m_Tiles)
            {
                tile.SetTileType(0);
            }
        }

        /// <summary>
        /// Returns if the specified coordinate fits inside of the GridSize.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool IsValidCoordinate(IntVector2 coordinate)
        {
            // Check if coordinate fits inside of the grid size.
            if (coordinate.x >= GridSize.x || coordinate.y >= GridSize.y)
            {
                Debug.LogError($"Coordinate is higher than grid size. Coordinate: {coordinate}", this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Changes the tile type of specific erea.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="center"></param>
        public void AddTilePatternToTiles(TilePattern pattern, IntVector2 center)
        {
            // Calculate begin and end values.
            int xBegin = (int)Mathf.Round(center.x - (pattern.Size / 2));
            int xEnd = (int)Mathf.Round(center.x + (pattern.Size / 2));

            int yBegin = (int)Mathf.Round(center.y - (pattern.Size / 2));
            int yEnd = (int)Mathf.Round(center.y + (pattern.Size / 2));

            // Itterate through every pattern value.
            int i = 0;

            // Loop through every coordinate and update the tile based on the tiletypelist.
            for (int x = xBegin; x <= xEnd; x++)
            {
                // Skip if index is out of range.
                if (Mathf.Clamp(x, 0, GridSize.x) != x)
                {
                    i += pattern.Size;
                    continue;
                }

                for (int y = yBegin; y <= yEnd; y++)
                {
                    // Skip if index is out of range.
                    if (Mathf.Clamp(y, 0, GridSize.y) != y)
                    {
                        i++;
                        continue;
                    }

                    SetTileTypeWithCoordinate(new IntVector2(y, x), pattern.TileTypesList[i]);
                    i++;
                }
            }
        }

        /// <summary>
        ///  Returns if the tile on the pattern is not zero.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="center"></param>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool IsCoordinateInsidePattern(TilePattern pattern, IntVector2 center, IntVector2 coordinate)
        {
            // Calculate begin and end values.
            int xBegin = (int)Mathf.Round(center.x - (pattern.Size / 2));
            int xEnd = (int)Mathf.Round(center.x + (pattern.Size / 2));

            int yBegin = (int)Mathf.Round(center.y - (pattern.Size / 2));
            int yEnd = (int)Mathf.Round(center.y + (pattern.Size / 2));

            // Calculate index inside of the pattern.
            int xIndex = (xEnd - xBegin) + (coordinate.x - xBegin);
            int yIndex = (yEnd - yBegin) + (coordinate.y - yBegin);
            int index = xIndex + yIndex;

            // If the index is out of the array size return false.
            if (Mathf.Clamp(index, 0, pattern.Size * pattern.Size) != index)
            {
                return false;
            }

            return pattern.TileTypesList[index] != 0;
        }

        /// <summary>
        /// Returns an array of all movables inside of the pattern.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public List<IGridMovable> GetMovablesInsidePattern(TilePattern pattern, IntVector2 center)
        {
            // Calculate begin and end values.
            int xBegin = (int)Mathf.Round(center.x - (pattern.Size / 2));
            int xEnd = (int)Mathf.Round(center.x + (pattern.Size / 2));

            int yBegin = (int)Mathf.Round(center.y - (pattern.Size / 2));
            int yEnd = (int)Mathf.Round(center.y + (pattern.Size / 2));

            // Itterate through every pattern value.
            int i = 0;

            List<IGridMovable> movables = new List<IGridMovable>();

            // Loop through every coordinate and update the tile based on the tiletypelist.
            for (int x = xBegin; x <= xEnd; x++)
            {
                // Skip if index is out of range.
                if (Mathf.Clamp(x, 0, GridSize.x) != x)
                {
                    i += pattern.Size;
                    continue;
                }

                for (int y = yBegin; y <= yEnd; y++)
                {
                    // Skip if index is out of range.
                    if (Mathf.Clamp(y, 0, GridSize.y) != y)
                    {
                        i++;
                        continue;
                    }

                    // Add Imovable at coordinate if its not null.
                    IGridMovable movable = GetGridMovableWithCoordinate(new IntVector2(x, y));
                    if (movable != null &&  pattern.TileTypesList[i] != 0)
                    {
                        movables.Add(movable);
                    }

                    i++;
                }
            }

            return movables;
        }

        /// <summary>
        /// Returns if the index fits inside of the Grid.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsValidIndex(int index)
        {
            // Check if the index fits in the gridsize.
            if (GridSize.x * GridSize.y < index)
            {
                Debug.LogError($"Index is higher than grid size. Index: {index}", this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create IGridMovable array with GridSize if it does not already exist.
        /// </summary>
        public void TryInitializingGridMovableArray()
        {
            // Initialize array if it is null or empty.
            if (m_Movables == null || m_Movables.Length == 0)
            {
                m_Movables = new IGridMovable[GridSize.x * GridSize.y];
            }
        }

        // Public variables.

        [Header("Used to define the size of the grid.")]
        public IntVector2 GridSize;

        [Header("Used to define the distance between tiles.")]
        public float GridDistance;

        [Space]

        [Header("Prefab that will be used as tiles.")]
        public GridTile TilePrefab;

        public event Action<GridTile> OnTileSelected = delegate { };
        public event Action<GridTile> OnTileHovered = delegate { };

        // Private variables.

        private GridTile[] m_Tiles;
        private IGridMovable[] m_Movables;
    }
}