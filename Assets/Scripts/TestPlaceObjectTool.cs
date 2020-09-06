using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TestPlaceObjectTool : MonoBehaviour
{
    //toggle for if place object tool is in use
    //toggled in DropDownMenuHandler function
    public bool toolInUse = false;

    //stored gameobjects that can be placed

    //small lines
    [SerializeField] GameObject selection1;
    //large lines
    [SerializeField] GameObject selection2;
    //transfomers
    [SerializeField] GameObject selection3;
    //coal plant
    [SerializeField] GameObject selection4;
    //natural gas plant
    [SerializeField] GameObject selection5;
    //solar panel
    [SerializeField] GameObject selection6;
    //nuclear plant
    [SerializeField] GameObject selection7;
    //tree
    [SerializeField] GameObject selection8;
    //house
    [SerializeField] GameObject selection9;

    //selected gameobject
    [SerializeField] GameObject selectedGameObject;

    public void Start()
    {
    }

    public void Update()
    {
        //if an object has been selected then the tool is active
        //therefore the function will run
        if (toolInUse == true)
        {
            PlaceObjectTool();
        }
    }
    public void PlaceObjectTool()
    {
        //check for input and check if click is not on ui
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //check to make sure grid is empty
            if (Helper.isGridSpaceEmpty(Helper.getMousePositionFromWorldRounded()))
            {
                //create object on location
                Instantiate(selectedGameObject, Helper.getMousePositionFromWorld(), transform.rotation);
                //fill grid space with object data
                //CREATE A HELPER FUNCTION TO REMOVE OBJECT FROM GRID
                //CREATE A TOOL TO REMOVE OBJECT FROM LEVEL/GRID
                Helper.setGridSpace(selectedGameObject,Helper.getMousePositionFromWorldRounded());
            }
        }
    }

    //determines what placeable object is selected on drop down menu
    public void DropDownMenuHandler(int selection)
    {
        if (selection == 0)
        {
            //none
            toolInUse = false;

            //set selected game object to the selected game object
            selectedGameObject = null;
        }
        if (selection == 1)
        {
            //small lines
            toolInUse = true;

            //set selected game object to the selected game object
            selectedGameObject = selection1;
        }
        else if (selection == 2)
        {
            //large lines
            toolInUse = true;

            //set selected game object to the selected game object
            selectedGameObject = selection2;
        }
        else if (selection == 3)
        {
            //transformers
            toolInUse = true;

            //set selected game object to the selected game object
            selectedGameObject = selection3;
        }
        else if (selection == 4)
        {
            //coal plant
            toolInUse = true;

            //set selected game object to the selected game object
            selectedGameObject = selection4;
        }
        else if (selection == 5)
        {
            //natural gas plant
            toolInUse = true;

            //set selected game object to the selected game object
            selectedGameObject = selection5;
        }
        else if (selection == 6)
        {
            //solar power
            toolInUse = true;

            //set selected game object to the selected game object
            selectedGameObject = selection6;
        }
        else if (selection == 7)
        {
            //nuclear plant
            toolInUse = true;

            //set selected game object to the selected game object
            selectedGameObject = selection7;
        }
        else if (selection == 8)
        {
            //tree
            toolInUse = true;

            //set selected game object to the selected game object
            selectedGameObject = selection8;
        }
        else if (selection == 9)
        {
            //house
            toolInUse = true;

            //set selected game object to the selected game object
            selectedGameObject = selection9;
        }

    }

}
