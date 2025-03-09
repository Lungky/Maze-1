/* Maze Generator using Depth-first Search Algorithm
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Maze_Generator : MonoBehaviour
{
    [SerializeField]
    private Maze_Cell _mazeCellPrefab; // This variable is used to store the cell prefab

    [SerializeField]
    private int _mazeWidth; // This variable is used to store the width of the maze

    [SerializeField]
    private int _mazeDepth; // This variable is used to store the height of the maze

    [SerializeField]
    private Maze_Cell[,] _mazeGrid; // This variable is used to store the grid of cells

    // Chance to remove an extra wall between cells to create loops (e.g., 3% chance by default)
    [SerializeField]
    private float _loopProbability = 0.03f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mazeGrid = new Maze_Cell[_mazeWidth, _mazeDepth]; // This line creates a new grid of cells with the width and height of the maze

        for (int i = 0; i < _mazeWidth; i++) // This loop iterates through the width of the maze
        {
            for (int j = 0; j < _mazeDepth; j++) // This loop iterates through the height of the maze
            {
                Maze_Cell newCell = Instantiate(_mazeCellPrefab, new Vector3(i, 0, j), Quaternion.identity); // This line creates a new cell at the current position in the grid
                _mazeGrid[i, j] = newCell; // This line assigns the new cell to the current position in the grid
            }
        }
        GenerateMaze(null, _mazeGrid[0, 0]); // This line calls the GenerateMaze method with the first cell in the grid
    //   yield return GenerateMaze(null, _mazeGrid[0, 0]); // Use this if you want to generate the maze using coroutine method (IEnumerator)

        AddLoops(); // Add random loops by clearing extra walls between adjacent cells.
        // Once maze generation is complete, designate the entry and exit points.
        // Here, we use the top-left cell as the entry and the bottom-right cell as the exit.
        Maze_Cell entryCell = _mazeGrid[0, 0];
        Maze_Cell exitCell = _mazeGrid[_mazeWidth - 1, _mazeDepth - 1];        
        entryCell.ClearLeftWall(); // Open an external wall for the entry cell (for example, the left wall)
        exitCell.ClearRightWall(); // Open an external wall for the exit cell (for example, the right wall)

        // (Optional) You could also change the cell's color or add an icon to indicate entry/exit.
        // entryCell.SetColor(Color.green);
        // exitCell.SetColor(Color.red);
    }

    // This method is used to generate the maze using coroutine method
    // This ensures that this method is called recursively and the maze is generated step by step until every cell is visited
    private void GenerateMaze(Maze_Cell previousCell, Maze_Cell currentCell) 
    {
        currentCell.Visit(); // This line marks the current cell as visited
        ClearWalls(previousCell, currentCell); // This line clears the walls between the current cell and the previous cell

        //   yield return new WaitForSeconds(0.05f); // This line waits for 0.05 seconds before continuing (Use this if you want to use coroutine method)

        Maze_Cell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell); // This line gets the next unvisited cell around the current cell
            if (nextCell != null) // This line checks if there is an unvisited cell around the current cell
            {
                GenerateMaze(currentCell, nextCell); // This line calls the GenerateMaze method recursively with the current cell and the next cell
            }
        } while (nextCell != null); // This line repeats the loop until there are no more unvisited cells around the current cell

        /*
        var nextCell = GetNextUnvisitedCell(currentCell); // This line gets the next unvisited cell around the current cell

        if (nextCell != null) // This line checks if there is an unvisited cell around the current cell
        {
            yield return GenerateMaze(currentCell, nextCell); // This line calls the GenerateMaze method recursively with the current cell and the next cell
        }
        */
    }

    private Maze_Cell GetNextUnvisitedCell(Maze_Cell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell); // This line gets all the unvisited cells around the current cell
        return unvisitedCells.OrderBy(_ => Random.Range(0, 10)).FirstOrDefault(); // This line shuffles the unvisited cells and returns the first cell
    }

    // This method is used to get all the unvisited cells around the current cell
    private IEnumerable <Maze_Cell> GetUnvisitedCells(Maze_Cell currentCell) // This method returns an IEnumerable of Maze_Cell
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazeWidth) // This line checks if the cell to the right of the current cell is within the maze
        {
            var cellToRight = _mazeGrid[x + 1, z]; // This line gets the cell to the right of the current cell

            if (cellToRight.IsVisited == false) // This line checks if the cell to the right of the current cell is unvisited
            {
                yield return cellToRight; // This line returns the cell to the right of the current cell
            }
        }

        if (x - 1 >= 0) // This line checks if the cell to the left of the current cell is within the maze
        {
            var cellToLeft = _mazeGrid[x - 1, z]; // This line gets the cell to the left of the current cell
            if (cellToLeft.IsVisited == false) // This line checks if the cell to the left of the current cell is unvisited
            {
                yield return cellToLeft; // This line returns the cell to the left of the current cell
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];
            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];
            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    // This method is used to clear the walls between the current cell and the previous cell
    private void ClearWalls(Maze_Cell previousCell, Maze_Cell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }
        else if (previousCell.transform.position.x < currentCell.transform.position.x) // This line checks if the previous cell is to the left of the current cell
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }
        else if (previousCell.transform.position.x > currentCell.transform.position.x) // This line checks if the previous cell is to the right of the current cell
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }
        else if (previousCell.transform.position.z < currentCell.transform.position.z) // This line checks if the previous cell is to the front of the current cell
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        else if (previousCell.transform.position.z > currentCell.transform.position.z) // This line checks if the previous cell is to the back of the current cell
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    // Iterates over each cell and randomly clears walls to create loops.
    private void AddLoops()
    {
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                Maze_Cell currentCell = _mazeGrid[x, z];

                // Check the right neighbor (if it exists).
                if (x + 1 < _mazeWidth)
                {
                    if (Random.value < _loopProbability)
                    {
                        currentCell.ClearRightWall();
                        _mazeGrid[x + 1, z].ClearLeftWall();
                    }
                }

                // Check the front neighbor (if it exists).
                if (z + 1 < _mazeDepth)
                {
                    if (Random.value < _loopProbability)
                    {
                        currentCell.ClearFrontWall();
                        _mazeGrid[x, z + 1].ClearBackWall();
                    }
                }
            }
        }
    }
}
