using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalGasScript : MonoBehaviour
{
    public bool powered = true;

    public float pollution;
    public float power;
    public static int cost = 100;
    public int price;

    public Phase2Manager phase2;
    void Awake()
    {
        phase2 = FindObjectOfType<Phase2Manager>();
        //snap to match grid
        Helper.SnapToGrid(this.transform);
        cost = price;
        Phase2Manager.currency -= cost;
        phase2.UpdateCurrency();
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
}
