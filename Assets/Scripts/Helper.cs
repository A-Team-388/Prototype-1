﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
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

    //Draws a line
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        lr.startWidth = .02f;
        lr.endWidth = .02f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        //GameObject.Destroy(myLine, duration);
    }
}
