﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarScript : MonoBehaviour
{
    public float power;
    // Start is called before the first frame update
    void Start()
    {
        //snap to match grid
        Helper.SnapToGrid(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
