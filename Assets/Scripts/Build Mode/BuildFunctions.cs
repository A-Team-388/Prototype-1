﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class BuildFunctions : MonoBehaviour
{
    //selected menu item
    public static uint menuSelection;

    //placeable objects
    [SerializeField] GameObject electricPole;
    [SerializeField] GameObject solarPanel;
    [SerializeField] GameObject windTurbine;
    [SerializeField] GameObject coalPlant;
    [SerializeField] GameObject gasPlant;
    [SerializeField] GameObject house;
    [SerializeField] GameObject tree;

    //amounts of power
    public static int solarAmount = 0;
    public static int turbineAmount = 0;
    public static int coalAmount = 0;
    public static int gasAmount = 0;

    //this is a 2d grid used to store locations of objects on the game board
    public static GameObject[,] playArea = new GameObject[101, 101];

    //text that appears on bottom of the screen to tell player how to stop placeing object/using tool
    public GameObject toolPromptObject;

    //text component of toolprompt object
    public Text toolPromptText;

    //dropdown ui
    public TMPro.TMP_Dropdown dropDown;

    //positions used to lay lines in run lines tool
    public static Vector2 position1 = new Vector2(0, 0);
    public static Vector2 position2 = new Vector2(0, 0);

    //line stuffs
    public static uint lineNumber = 0;
    public static Vector2[] lineLocations = new Vector2[200];
    public static GameObject[] lineObjects = new GameObject[200];

    // Start is called before the first frame update
    void Start()
    {
        //find and set the toolprompt object
        toolPromptObject = GameObject.Find("ToolPrompt");

        //find and set the toolprompt component
        toolPromptText = toolPromptObject.GetComponent<Text>();

        //find and set the drop down object
        dropDown = GameObject.Find("Dropdown").GetComponent<TMPro.TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (menuSelection)
        {
            case 0:
                PlaceObjectFunction(electricPole);
                break;
            case 1:
                PlaceObjectFunction(solarPanel);
                break;
            case 2:
                PlaceObjectFunction(windTurbine);
                break;
            case 3:
                PlaceObjectFunction(coalPlant);
                break;
            case 4:
                PlaceObjectFunction(gasPlant);
                break;
            case 5:
                RunLinesTool();
                break;
            case 6:
                RemoverTool();
                break;
        }

    }

    //place object function
    public void PlaceObjectFunction(GameObject selectedObject)
    {
        //check for input and check for click not on ui                             //might need to check if click is on game window here
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            //check if gridspaces needed are empty
            if (areGridSpacesEmpty(Helper.getMousePositionFromWorldRounded()))
            {
                //determine which object is selected
                switch (menuSelection)
                {
                    case 0:
                        Instantiate(selectedObject, Helper.getMousePositionFromWorld(), transform.rotation);
                        AddGridSpaces(selectedObject);
                        break;
                    case 1:
                        if (Phase2Manager.currency >= SolarScript.cost)
                        {
                            solarAmount++;
                            Instantiate(selectedObject, Helper.getMousePositionFromWorld(), transform.rotation);
                            AddGridSpaces(selectedObject);
                            //Phase2Manager.currency -= SolarScript.cost;
                        }
                        break;
                    case 2:
                        if (Phase2Manager.currency >= TurbineScript.cost)
                        {
                            turbineAmount++;
                            Instantiate(selectedObject, Helper.getMousePositionFromWorld(), transform.rotation);
                            AddGridSpaces(selectedObject);
                            //Phase2Manager.currency -= TurbineScript.cost;
                        }
                        break;
                    case 3:
                        if (Phase2Manager.currency >= CoalScript.cost)
                        {
                            coalAmount++;
                            Instantiate(selectedObject, Helper.getMousePositionFromWorld(), transform.rotation);
                            AddGridSpaces(selectedObject);
                            //Phase2Manager.currency -= CoalScript.cost;
                        }
                        break;
                    case 4:
                        if (Phase2Manager.currency >= NaturalGasScript.cost)
                        {
                            gasAmount++;
                            Instantiate(selectedObject, Helper.getMousePositionFromWorld(), transform.rotation);
                            AddGridSpaces(selectedObject);
                            //Phase2Manager.currency -= NaturalGasScript.cost;
                        }
                        break;
                }
            }
        }
    }

    //checks to make sure grid spaces are empty
    public bool areGridSpacesEmpty(Vector2 position)
    {
        switch (menuSelection)
        {
            case 0://power pole, single position to check
                return IsGridSpaceEmpty(position);
            case 1://solar panel, single position to check
                return IsGridSpaceEmpty(position);
            case 2://turbine, double vertical position to check
                if (IsGridSpaceEmpty(position) && IsGridSpaceEmpty(position + new Vector2(0, 1)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 3://coal plant, four positions to check
                if (IsGridSpaceEmpty(position) && IsGridSpaceEmpty(position + new Vector2(0, 1)) && IsGridSpaceEmpty(position + new Vector2(-1, 1)) && IsGridSpaceEmpty(position + new Vector2(-1, 0)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 4://natural gas, 9 positions square to check
                if (IsGridSpaceEmpty(position) && IsGridSpaceEmpty(position + new Vector2(0, 1)) && IsGridSpaceEmpty(position + new Vector2(0, 2)) && IsGridSpaceEmpty(position + new Vector2(-1, 1)) && IsGridSpaceEmpty(position + new Vector2(-1, 0)) && IsGridSpaceEmpty(position + new Vector2(-1, 2)) && IsGridSpaceEmpty(position + new Vector2(-2, 1)) && IsGridSpaceEmpty(position + new Vector2(-2, 2)) && IsGridSpaceEmpty(position + new Vector2(-2, 0)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
    }

    //checks array to see if space is null
    public static bool IsGridSpaceEmpty(Vector3 locationToCheck)
    {
        return (playArea[(int)locationToCheck.x, (int)locationToCheck.y] == null);
    }

    //set many grid spaces
    public static void AddGridSpaces(GameObject selectedObject)
    {
        switch (menuSelection)
        {
            case 0:
                //fill grid space with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded());
                break;
            case 1:
                //fill grid space with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded());
                break;
            case 2:
                //turbine
                //fill grid space with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded());
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(0, 1, 0));
                break;
            case 3:
                //fill grid space with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded());
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(0, 1, 0));
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(-1, 1, 0));
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(-1, 0, 0));
                break;
            case 4:
                //fill grid space with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded());
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(0, 1, 0));
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(0, 2, 0));
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(-1, 2, 0));
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(-1, 1, 0));
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(-1, 0, 0));
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(-2, 2, 0));
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(-2, 1, 0));
                //fill grid space above with object data
                SetGridSpace(selectedObject, Helper.getMousePositionFromWorldRounded() + new Vector3(-2, 0, 0));
                break;
        }
    }

    //set a grid space
    public static void SetGridSpace(GameObject entry, Vector3 location)
    {
        playArea[(int)location.x, (int)location.y] = entry;
    }

    //determines if a position contains an instance of a specific game object
    public static bool IsGridSpaceMatching(GameObject objectToCompare, Vector3 location)
    {
        return (objectToCompare == playArea[(int)location.x, (int)location.y]);
    }

    //convert menu selection to usable
    public void DropDownMenuHandler(int selection)
    {
        menuSelection = (uint)selection;
    }

    //tool that remove things
    public void RemoverTool()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (!IsGridSpaceEmpty(Helper.getMousePositionFromWorldRounded()))
            {
                GameObject objectToBeRemoved = Helper.GetObjectFromLocation2d(Helper.getMousePositionFromWorldRounded());

                if (objectToBeRemoved.name == electricPole.name + "(Clone)")
                {

                    //power pole
                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                }
                else if (objectToBeRemoved.name == solarPanel.name + "(Clone)")
                {
                    //solar panel
                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                }
                else if (objectToBeRemoved.name == windTurbine.name + "(Clone)")
                {
                    //turbine
                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));
                }
                else if (objectToBeRemoved.name == coalPlant.name + "(Clone)")
                {
                    //coal
                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 1, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 1, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 0, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 0, 0));
                }
                else if (objectToBeRemoved.name == gasPlant.name + "(Clone)")
                {
                    //gas
                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 2, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 2, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 1, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 1, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 0, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 0, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 2, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 2, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 1, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 1, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 0, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 0, 0));

                    ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 2, 0));
                    RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 2, 0));
                }



            }
        }
    }

    public static void RemoveExtraGridSpaces(GameObject objectToBeRemoved)
    {
        switch (menuSelection)
        {
            case 0:
                //power pole
                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                break;
            case 1:
                //solar panel
                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                break;
            case 3:
                //turbine
                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));
                break;
            case 4:
                //coal
                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 1, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 1, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 0, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 0, 0));
                break;
            case 5:
                //gas
                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 1, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 2, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(0, 2, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 1, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 1, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 0, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 0, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 2, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-1, 2, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 1, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 1, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 0, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 0, 0));

                ClearGridSpace(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 2, 0));
                RemoveLines(new Vector3(objectToBeRemoved.transform.position.x, objectToBeRemoved.transform.position.y, 0) + new Vector3(-2, 2, 0));
                break;
        }

    }

    //empty a grid space
    public static void ClearGridSpace(Vector3 locationToClear)
    {
        playArea[(int)locationToClear.x, (int)locationToClear.y] = null;
    }

    //run lines tool
    public void RunLinesTool()
    {
        if (position1 != new Vector2(0, 0))
        {
            //enable on screen text
            toolPromptText.text = "Right Click To Stop Placing Cable";
        }
        else
        {
            //enable on screen text
            toolPromptText.text = "";
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (!IsGridSpaceEmpty(Helper.getMousePositionFromWorldRounded()) && position1 == new Vector2(0, 0) && !IsGridSpaceMatching(tree, Helper.getMousePositionFromWorldRounded()))
            {
                position1 = Helper.getMousePositionFromWorldRounded();

            }
            else if (!IsGridSpaceEmpty(Helper.getMousePositionFromWorldRounded()) && position1 != new Vector2(0, 0) && position1 != new Vector2(Helper.getMousePositionFromWorldRounded().x, Helper.getMousePositionFromWorldRounded().y) && !IsGridSpaceMatching(tree, Helper.getMousePositionFromWorldRounded()))
            {
                if (Helper.getDistanceFromPosition(new Vector3(position1.x, position1.y, 0), new Vector3(Helper.getMousePositionFromWorldRounded().x, Helper.getMousePositionFromWorldRounded().y, 0)) <= 3.5)
                {
                    position2 = Helper.getMousePositionFromWorldRounded();

                    if (IsGridSpaceMatching(house, new Vector3(position1.x, position1.y, 0)) && IsGridSpaceMatching(house, new Vector3(position2.x, position2.y, 0)))
                    {
                        position1 = new Vector2(0, 0);
                        position2 = new Vector2(0, 0);
                    }
                    else
                    {
                        Helper.DrawLine(position1, position2, Color.white);

                        position1 = new Vector2(0, 0);
                        position2 = new Vector2(0, 0);
                    }
                }
            }
        }

        if(Input.GetMouseButtonDown(1) && position1 != new Vector2(0, 0))
        {
            position1 = new Vector2(0, 0);
        }
    }

    public static void RemoveLines(Vector3 pointToCheck)
    {
        for (int count = 0; count <= lineNumber; count++)
        {
            if (new Vector3(pointToCheck.x, pointToCheck.y, 0) == new Vector3(lineLocations[count].x, lineLocations[count].y, 0))
            {
                Object.DestroyImmediate(lineObjects[count]);
            }
        }
    }

    //check surrounding grid spaces for specific object
    public static bool CheckSurroundingGridSpaces(Vector3 PositionToCheck, GameObject objectToCheckFor)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (!(x == 0 && y == 0))
                {
                    if (IsGridSpaceMatching(objectToCheckFor, new Vector3(PositionToCheck.x + x, PositionToCheck.y + y, 0)))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}