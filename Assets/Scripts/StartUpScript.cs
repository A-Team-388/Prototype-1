using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpScript : MonoBehaviour
{
    public uint startingNumberOfHouses = 10;
    public uint startingNumberOfTrees = 10;
    public uint startingHeight = 16;
    public uint startingWidth = 28;

    [SerializeField]public GameObject house;
    [SerializeField] public GameObject tree;

    // Start is called before the first frame update
    void Start()
    {
        int xPos = 0;
        int yPos = 0;
        while (startingNumberOfHouses > 0)
        {
            xPos = Random.Range(0 , (int)startingWidth);
            yPos = Random.Range(0 , (int)startingHeight);
            if (GameManager.isGridSpaceEmpty(new Vector3(xPos, yPos, 0)))
            {
                Instantiate(house, new Vector3(xPos, yPos, 0), transform.rotation);
                Debug.Log("house");
                GameManager.setGridSpace(house, new Vector3(xPos, yPos, 0));
                startingNumberOfHouses--;
            }
        }
        while (startingNumberOfTrees > 0)
        {
            xPos = Random.Range(0, (int)startingWidth);
            yPos = Random.Range(0, (int)startingHeight);
            if (GameManager.isGridSpaceEmpty(new Vector3(xPos, yPos, 0)))
            {
                Instantiate(tree, new Vector3(xPos, yPos, 0), transform.rotation);
                Debug.Log("tree");
                GameManager.setGridSpace(tree, new Vector3(xPos, yPos, 0));
                startingNumberOfTrees--;
            }
        }
    }
}
