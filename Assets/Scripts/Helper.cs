using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Helper
{
    //creates a 2d grid spanning from 0,0 to 100,100
    public static string[,] playArea = new string[101, 101];

    //Snap object to grid
    public static void SnapToGrid(Transform transform)
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
    }

    //gets the mouse position from the world and returns it rounded
    public static Vector3 getMousePositionFromWorldRounded()
    {
        Vector3 position = Input.mousePosition;
        position = Camera.main.ScreenToWorldPoint(position);
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }

    //gets distance between two points
    public static float getDistanceFromPosition(float x1, float y1, float x2, float y2)
    {
        float deltaX = x2 - x1;
        float deltaY = y2 - y1;

        return Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    //gets the in world mouse position
    public static Vector3 getMousePositionFromWorld()
    {
        Vector3 position = Input.mousePosition;
        position = Camera.main.ScreenToWorldPoint(position);
        return position;
    }

    //returns screen pixel location for either game window or screen
    //im not sure which and im too tired to check
    public static Vector3 getMousePositionFromScreen()
    {
        return Input.mousePosition;
    }

    //checks array to see if space is null
    public static bool isGridSpaceEmpty(Vector3 locationToCheck)
    {
        Debug.Log(playArea[(int)locationToCheck.x, (int)locationToCheck.y]);
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
