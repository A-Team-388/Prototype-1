using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeIconScript : MonoBehaviour
{
    HomeScript parentScript;
    SpriteRenderer sprRend;
    public Sprite noPowerSprite;
    bool toggleDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = transform.parent.GetComponent<HomeScript>();
        sprRend = gameObject.GetComponent<SpriteRenderer>();
        sprRend.sprite = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {



        if (parentScript.neededPower != parentScript.MaxneededPower)
        {
            sprRend.sprite = noPowerSprite;
            if (toggleDirection == true)
            {
                transform.localPosition += new Vector3(0, .01f, 0);
                if (transform.localPosition.y >= 8.5f)
                {
                    toggleDirection = false;
                }
            }
            else
            {
                transform.localPosition += new Vector3(0, -.01f, 0);
                if (transform.localPosition.y <= 7)
                {
                    toggleDirection = true;
                }

            }
        }
        else
        {
            sprRend.sprite = null;
        }
    }
}
