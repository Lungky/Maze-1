using UnityEngine;

public class Maze_Cell : MonoBehaviour
{
    [SerializeField]    // This attribute is used to make a variable visible in the inspector   
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _frontwall;

    [SerializeField]
    private GameObject _backWall;

    [SerializeField]
    private GameObject _unvisitedBlock;

    [SerializeField]
    private GameObject _topWall;

    public bool IsVisited { get; private set; } // This property is used to get the value of IsVisited and set it only in this class

    public void Visit() // This method is used to set the value of IsVisited to true and deactivate the unvisited block
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);
        _topWall.SetActive(true);
    }

    public void ClearLeftWall() // This method is used to deactivate the left wall
    {
        _leftWall.SetActive(false);
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        _frontwall.SetActive(false);
    }

    public void ClearBackWall()
    {
        _backWall.SetActive(false);
    }

}
