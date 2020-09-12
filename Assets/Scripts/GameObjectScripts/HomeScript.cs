using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScript : MonoBehaviour
{
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
    }

    void checkIfDead()
    {
        if (null == BuildMenuFunctions.playArea[(int)transform.position.x, (int)transform.position.y])
        {
            Destroy(this.gameObject);
        }
    }

    void checkIfPowered()
    {

    }
    /*
    GameObject findSelfInGrid()
    {

        Vector2 myLocation = new Vector2();
        for(int i = 0;i <= BuildMenuFunctions.lineNumber;i++)
        {
            if()
            {

            }
        }
    }
    */
}
