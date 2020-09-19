﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarScript : MonoBehaviour
{
    //is true when powered or provides power
    public bool powered = true;

    //the cost of this item
    public static int cost = 20;

    //the max amount of providable power
    public uint maxPower = 10;

    //the current amount of providable power
    public uint power = 10;

    //phase 2 script
    public Phase2Manager phase2;

    void Awake()
    {
        //find the phase 2 manager script
        phase2 = FindObjectOfType<Phase2Manager>();

        //snap to match grid
        Helper.SnapToGrid(this.transform);

        //remove the cost of this item from the total currency
        Phase2Manager.currency -= cost;

        //update the currency ui element
        phase2.UpdateCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        checkIfDead();
    }

    //Determine if this object should be deleted
    void checkIfDead()
    {
        if (null == BuildFunctions.playArea[(int)transform.position.x, (int)transform.position.y])
        {
            Destroy(this.gameObject);
        }
    }
}
