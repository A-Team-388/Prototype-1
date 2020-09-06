using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this is a 2d grid used to store locations of objects on the game board
    //[0,0] in the array = (0,0) in the game world
    //objects names are added to this array via the setGridSpace function found below
    //you can check if a space in the grid is occupied via the isGridSpaceEmpty function found below
    public static string[,] playArea = new string[101, 101];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //checks array to see if space is null
    public static bool isGridSpaceEmpty(Vector3 locationToCheck)
    {
        if (playArea[(int)locationToCheck.x, (int)locationToCheck.y] == null)
        {
            //return true if object exists in location
            return true;
        }
        else
        {
            //return false if object does not exist in location
            return false;
        }
    }

    //set a grid space
    public static void setGridSpace(GameObject entry, Vector3 location)
    {
        playArea[(int)location.x, (int)location.y] = (string)entry.name;
    }
}
