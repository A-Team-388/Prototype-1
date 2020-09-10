using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCircleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-2f, -2f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (BuildMenuFunctions.position1 != new Vector2(0, 0))
        {
            transform.position = new Vector3(BuildMenuFunctions.position1.x, BuildMenuFunctions.position1.y,.5f);
        }
        else
        {
            transform.position = new Vector3(-2f,-2f,.5f);
        }
    }
}
