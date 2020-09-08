using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    //this is a 2d grid used to store locations of objects on the game board
    //[0,0] in the array = (0,0) in the game world
    //objects names are added to this array via the setGridSpace function found below
    //you can check if a space in the grid is occupied via the isGridSpaceEmpty function found below
    //positions horizontal or vertical of [0,0] remain empty
    //this prevents errors when using the checkSurroundingGridSpaces function below
    public static string[,] playArea = new string[101, 101];

    //determines if a position contains an instance of a specific game object
    public static bool isGridSpaceMatching(GameObject objectToCompare, Vector3 location)
    {
       return (objectToCompare.name == playArea[(int)location.x, (int)location.y]) ;
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

    //check surrounding grid spaces for specific object
    public static bool checkSurroundingGridSpaces(Vector3 PositionToCheck,GameObject objectToCheckFor)
    {       
        for (int x = -1 ; x<=1 ; x++)
        {
            for (int y = -1 ; y<=1 ; y++)
            {
                if (!(x == 0 && y == 0))
                {
                    if (isGridSpaceMatching(objectToCheckFor, new Vector3(PositionToCheck.x+x,PositionToCheck.y+y,0)))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}
