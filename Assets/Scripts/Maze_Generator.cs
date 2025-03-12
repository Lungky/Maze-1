/* Maze Generator using Depth-first Search Algorithm
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Maze_Generator : MonoBehaviour
{
    [SerializeField]
    private Maze_Cell _mazeCellPrefab; // Cell prefab

    [SerializeField]
    private int _mazeWidth; // Maze width

    [SerializeField]
    private int _mazeDepth; // Maze depth

    [SerializeField]
    private Maze_Cell[,] _mazeGrid; // Grid of cells

    // Chance to remove an extra wall between cells to create loops
    [SerializeField]
    private float _loopProbability = 0.03f;

    // NEW: Cell size multiplier (set to 3 for your case)
    [SerializeField]
    private float _cellSize = 5f;

    void Start()
    {
        _mazeGrid = new Maze_Cell[_mazeWidth, _mazeDepth];

        for (int i = 0; i < _mazeWidth; i++)
        {
            for (int j = 0; j < _mazeDepth; j++)
            {
                // Multiply the grid indices by _cellSize so the cells are spaced properly.
                Vector3 cellPosition = new Vector3(i * _cellSize, 0, j * _cellSize);
                Maze_Cell newCell = Instantiate(_mazeCellPrefab, cellPosition, Quaternion.identity);
                _mazeGrid[i, j] = newCell;
            }
        }
        GenerateMaze(null, _mazeGrid[0, 0]);
        AddLoops();

        // Set entry and exit cells
        Maze_Cell entryCell = _mazeGrid[0, 0];
        Maze_Cell exitCell = _mazeGrid[_mazeWidth - 1, _mazeDepth - 1];
        entryCell.ClearLeftWall();
        exitCell.ClearRightWall();
    }

    // Recursive Depth-First Search Maze Generation
    private void GenerateMaze(Maze_Cell previousCell, Maze_Cell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        Maze_Cell nextCell;
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);
            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private Maze_Cell GetNextUnvisitedCell(Maze_Cell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(0, 10)).FirstOrDefault();
    }

    // IMPORTANT: Convert world position to grid index by dividing by _cellSize.
    private IEnumerable<Maze_Cell> GetUnvisitedCells(Maze_Cell currentCell)
    {
        int x = (int)(currentCell.transform.position.x / _cellSize);
        int z = (int)(currentCell.transform.position.z / _cellSize);

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];
            if (!cellToRight.IsVisited)
                yield return cellToRight;
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];
            if (!cellToLeft.IsVisited)
                yield return cellToLeft;
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];
            if (!cellToFront.IsVisited)
                yield return cellToFront;
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];
            if (!cellToBack.IsVisited)
                yield return cellToBack;
        }
    }

    // Clear the walls between two adjacent cells.
    private void ClearWalls(Maze_Cell previousCell, Maze_Cell currentCell)
    {
        if (previousCell == null)
            return;

        // Compare world positions directly (they still indicate relative placement)
        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
        }
        else if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
        }
        else if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
        }
        else if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
        }
    }

    // Add random loops by clearing extra walls between adjacent cells.
    private void AddLoops()
    {
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                Maze_Cell currentCell = _mazeGrid[x, z];

                // Check right neighbor
                if (x + 1 < _mazeWidth && Random.value < _loopProbability)
                {
                    currentCell.ClearRightWall();
                    _mazeGrid[x + 1, z].ClearLeftWall();
                }

                // Check front neighbor
                if (z + 1 < _mazeDepth && Random.value < _loopProbability)
                {
                    currentCell.ClearFrontWall();
                    _mazeGrid[x, z + 1].ClearBackWall();
                }
            }
        }
    }
}
