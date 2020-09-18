using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScript : MonoBehaviour
{
    //locations of connected objects
    public GameObject[] connectedObjects = new GameObject[20];

    public uint amountOfConnectedObjects = 0;

    public Vector2 connectedObjectLocation;

    public bool powered;


    // Start is called before the first frame update
    void Start()
    {
        //snap to match grid
        Helper.SnapToGrid(this.transform);

        powered = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfDead();

        /*
        if (powered == false)
        {

            searchForConnections();
            if (amountOfConnectedObjects!=0)
            {
                determineIfPowered();
            }
        }
        */
    }

    void checkIfDead()
    {
        if (null == BuildFunctions.playArea[(int)transform.position.x, (int)transform.position.y])
        {
            Destroy(this.gameObject);
        }
    }

    void determineIfPowered()
    {
        for (int i = 0; i < amountOfConnectedObjects; i++)
        {
            if (connectedObjects[i].name == "smallllines(Clone)")
            {
                if (connectedObjects[i].GetComponent<SmallLineScript>().powered == true)
                {
                    powered = true;
                }
                else
                {
                    powered = false;
                }
            }
            else if (connectedObjects[i].name == "solar(Clone)")
            {
                if (connectedObjects[i].GetComponent<SolarScript>().powered == true)
                {
                    powered = true;
                }
                else
                {
                    powered = false;

                }
            }
            else if (connectedObjects[i].name == "turbine(Clone)")
            {
                if (connectedObjects[i].GetComponent<TurbineScript>().powered == true)
                {
                    powered = true;
                }
                else
                {
                    powered = false;
                }
            }
            else if (connectedObjects[i].name == "coalCoolingTower(Clone)")
            {
                if (connectedObjects[i].GetComponent<CoalScript>().powered == true)
                {
                    powered = true;
                }
                else
                {
                    powered = false;
                }
            }
            else if (connectedObjects[i].name == "NaturalGasPlant(Clone)")
            {
                if (connectedObjects[i].GetComponent<NaturalGasScript>().powered == true)
                {
                    powered = true;
                }
                else
                {
                    powered = false;
                }
            }
        }
    }

    void searchForConnections()
    {
        amountOfConnectedObjects = 0;
        Vector2 myLocation = new Vector2(transform.position.x, transform.position.y);
        for (int i = 0; i <= BuildMenuFunctions.lineNumber; i++)
        {

            if (myLocation == BuildMenuFunctions.lineLocations[i])
            {
 

                //get location of other object
                if (i % 2 == 0)
                {
                    connectedObjectLocation = BuildMenuFunctions.lineLocations[i + 1];
                    connectedObjects[amountOfConnectedObjects] = Helper.GetObjectFromLocation2d(connectedObjectLocation);
                    amountOfConnectedObjects++;
                }
                else if (i % 2 != 0)
                {
                    connectedObjectLocation = BuildMenuFunctions.lineLocations[i - 1];

                    Helper.GetObjectFromLocation2d(connectedObjectLocation);
                    connectedObjects[amountOfConnectedObjects] = Helper.GetObjectFromLocation2d(connectedObjectLocation);
                    amountOfConnectedObjects++;
                }
            }
        }
    }
}
