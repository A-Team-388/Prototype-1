using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    //selected placable objects sprite renderer
    SpriteRenderer selectedObjectSpriteRenderer;

    //this objects sprite renderer
    SpriteRenderer mouseSpriteRenderer;

    //Start is called on the first frame
    void Start()
    {
        //getting sprite renderer for this object
        mouseSpriteRenderer = GetComponent<SpriteRenderer>();

        //setting the sprites transparency
        mouseSpriteRenderer.color = new Color(1, 1, 1, .25f);
    }

    // Update is called once per frame
    void Update()
    {
        //set position to rounded mouse position
        transform.position = new Vector3(Helper.getMousePositionFromWorldRounded().x, Helper.getMousePositionFromWorldRounded().y, -.5f);

        //object selected switch
        if (BuildMenuFunctions.selectedGameObject != null)
        {
            //get selected sprite from selected game object
            selectedObjectSpriteRenderer = BuildMenuFunctions.selectedGameObject.GetComponent<SpriteRenderer>();

            //set mouse child sprite to selected object
            mouseSpriteRenderer.sprite = selectedObjectSpriteRenderer.sprite;
        }
        else if (BuildMenuFunctions.selectedGameObject == null)
        {
            //set mouse child sprite to null
            mouseSpriteRenderer.sprite = null;
        }
    }
}
