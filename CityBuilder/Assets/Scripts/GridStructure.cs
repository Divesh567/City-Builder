using System;
using UnityEngine;

[Serializable]
public class GridStructure
{
    public int cellSize;
    public Cell[,] grid;
    private int width, length;
    public GridStructure(int cellSize, int width, int length)
    {
        this.cellSize = cellSize;
        this.width = width;
        this.length = length;
        grid = new Cell[this.width, this.length];

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                grid[row, col] = new Cell();
                
            }
        }
    }
    public Vector3 CalculateGridPosition(Vector3 inputPositon)
    {
        int x = Mathf.FloorToInt((float)(inputPositon.x / cellSize));
        int z = Mathf.FloorToInt((float)(inputPositon.z / cellSize));


        return new Vector3(x * cellSize, 0, z * cellSize);

    }

    private Vector2Int CalculateGridIndex(Vector3 gridPosition)
    {
        return new Vector2Int((int)(gridPosition.x / cellSize), (int)(gridPosition.z / cellSize));
    }

    public bool IsCellTaken(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        if (CheckIndexValidity(cellIndex))
            return grid[cellIndex.y, cellIndex.x].IsTaken;
        throw new IndexOutOfRangeException("No index " + cellIndex + " in grid");
    }

    public void PlaceStructureOnTheGrid(GameObject structure, Vector3 gridPosition, StructureBaseSO structureData)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        if (CheckIndexValidity(cellIndex))
            grid[cellIndex.y, cellIndex.x].SetConstruction(structure, structureData);
    }

    public StructureBaseSO GetStructureDataFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        return grid[cellIndex.y, cellIndex.x].GetStructureData();
    }

    public GameObject GetStructureFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        return grid[cellIndex.y, cellIndex.x].GetStructure();
    }

    public void RemoveStructureFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        grid[cellIndex.y, cellIndex.x].RemoveStructure();
    }

    private bool CheckIndexValidity(Vector2Int cellIndex)
    {
        if (cellIndex.x >= 0 && cellIndex.x < grid.GetLength(1) && cellIndex.y >= 0 && cellIndex.y < grid.GetLength(0))
            return true;
        return false;
    }

    public Vector3Int? GetPositionOfTheNeighbourIfExists(Vector3 gridPosition, Direction direction)
    {
        Vector3Int? neighbourPosition = Vector3Int.FloorToInt(gridPosition);
        switch (direction)
        {
            case Direction.Up:
                neighbourPosition += new Vector3Int(0, 0, cellSize);
                break;
            case Direction.Right:
                neighbourPosition += new Vector3Int(cellSize, 0, 0);
                break;
            case Direction.Down:
                neighbourPosition += new Vector3Int(0, 0, -cellSize);
                break;
            case Direction.Left:
                neighbourPosition += new Vector3Int(-cellSize, 0, 0);
                break;
        }
        var index = CalculateGridIndex(neighbourPosition.Value);
        if (CheckIndexValidity(index) == false)
        {
            return null;
        }
        return neighbourPosition;
    }

    public enum Direction
    {
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }
}


