using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalScript : MonoBehaviour
{
    public bool powered = true;

    public int pollution = -10;

    public static int cost = 10;

    //the max amount of providable power
    public uint maxPower = 20;

    //the amount of providable power
    public uint power = 20;

    public Phase2Manager phase2;
    void Awake()
    {
        phase2 = FindObjectOfType<Phase2Manager>();
        //snap to match grid
        Helper.SnapToGrid(this.transform);
        //cost = price;
        Phase2Manager.currency -= cost;
        phase2.UpdateCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        checkIfDead();

        if (BuildFunctions.simulationReset)
        {

        }
        else
        {
            SimulationReset();
        }
    }

    void checkIfDead()
    {
        if (null == BuildFunctions.playArea[(int)transform.position.x, (int)transform.position.y])
        {
            Destroy(this.gameObject);
        }
    }
    void SimulationReset()
    {
        power = maxPower;
    }
}
