using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float pollution;
    public float power;
    [SerializeField] public static int cost;
    public int price;
    public Phase2Manager phase2;
    void Start()
    {
        //snap to match grid
        phase2 = FindObjectOfType<Phase2Manager>();
        Helper.SnapToGrid(this.transform);
        cost = price;
        Phase2Manager.currency -= cost;
        phase2.UpdateText();
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
