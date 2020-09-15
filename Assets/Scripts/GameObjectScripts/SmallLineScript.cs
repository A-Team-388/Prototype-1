using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLineScript : MonoBehaviour
{
    //locations of connected objects
    public GameObject[] connectedObjects = new GameObject[20];

    public uint amountOfConnectedObjects = 0;

    public Vector2 connectedObjectLocation;

    // Start is called before the first frame update
    void Start()
    {
        //snap to match grid
        Helper.SnapToGrid(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        checkIfDead();

        if(Input.GetKeyDown(KeyCode.A))
        {
            searchForConnections();
        }
    }

    void checkIfDead()
    {
        if (null == BuildMenuFunctions.playArea[(int)transform.position.x, (int)transform.position.y])
        {
            Destroy(this.gameObject);
        }
    }

    void searchForPowerSuppliers()
    {

    }

    void searchForConnections()
    {
        amountOfConnectedObjects = 0;
        Vector2 myLocation = new Vector2(transform.position.x,transform.position.y);
        for (int i = 0; i <= BuildMenuFunctions.lineNumber; i++)
        {
            Debug.Log("my location = " + myLocation);
            Debug.Log("location to check = " + BuildMenuFunctions.lineLocations[i]);
            if (myLocation == BuildMenuFunctions.lineLocations[i])
            {
                Debug.Log("my location matching = " + myLocation);
                Debug.Log("location to check matching = " + BuildMenuFunctions.lineLocations[i]);

                //get location of other object
                if(i%2 == 0)
                {
                    Debug.Log("myLocation is Even");
                    connectedObjectLocation = BuildMenuFunctions.lineLocations[i + 1];
                    Debug.Log("location of connected object = " + BuildMenuFunctions.lineLocations[i - 1]);
                    connectedObjects[amountOfConnectedObjects] = Helper.GetObjectFromLocation2d(connectedObjectLocation);
                    Debug.Log("connected object = " + connectedObjects[amountOfConnectedObjects]);
                    amountOfConnectedObjects++;
                }
                else if(i%2 != 0)
                {
                    Debug.Log("myLocation is Odd");
                    connectedObjectLocation = BuildMenuFunctions.lineLocations[i - 1];

                    Helper.GetObjectFromLocation2d(connectedObjectLocation);
                    connectedObjects[amountOfConnectedObjects] = Helper.GetObjectFromLocation2d(connectedObjectLocation);
                    Debug.Log("connected object = " + connectedObjects[amountOfConnectedObjects]);
                    amountOfConnectedObjects++;
                }
                else
                {
                    Debug.Log("my location is neither odd nor even. Something has gone horribly wrong");
                }
            }
        }
    }
}
