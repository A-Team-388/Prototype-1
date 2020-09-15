using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineScript : MonoBehaviour
{
    public bool powered = true;

    public static int cost = 15;

    // Start is called before the first frame update
    public float power;
    void Start()
    {
        //snap to match grid
        Helper.SnapToGrid(this.transform);

        Phase2Manager.currency -= cost;
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
