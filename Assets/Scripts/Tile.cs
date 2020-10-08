using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour //Contient toutes les informations sur les tiles
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public bool inPath = false;
    public bool inPathFromStart = false;
    public bool inPathFromGoal = false;
    public bool isCommunTile = false;

    public List<Tile> adjacentList = new List<Tile>();

    //BFS (breath first search)
    public bool visited = false;
    public bool visitedFromStart = false;
    public bool visitedFromGoal = false;
    public Tile parent = null;
    public int distance = 0; //provient pas du BFS, elle donneras la distance entre chaque tile car on ne peut bouger qu'avec un certain nombres de step
    public int cost = 1;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (inPath)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (inPathFromStart && !isCommunTile && !current && !inPathFromGoal)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (inPathFromGoal && !isCommunTile && !current && !inPathFromStart)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (isCommunTile || inPathFromStart && inPathFromGoal)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        adjacentList.Clear();
        current = false;
        target = false;
        selectable = false;
        inPath = false;
        inPathFromStart = false;
        inPathFromGoal = false;
        isCommunTile = false;
        visitedFromStart = false;
        visitedFromGoal = false;

    //BFS (breath first search)
    visited = false;
        parent = null;
        distance = 0;

    }

    public void FindNeighbors(float jumpHeight)
    {
        Reset();
        CheckTile(Vector3.forward, jumpHeight);
        CheckTile(-Vector3.forward, jumpHeight);
        CheckTile(Vector3.right, jumpHeight);
        CheckTile(-Vector3.right, jumpHeight);
    }

    public void CheckTile(Vector3 direction, float jumpHeight)
    {
        Vector3 halfExtends = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f); //attention le milieu pour le jump
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null && tile.walkable)
            {
                RaycastHit hit;

                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                    adjacentList.Add(tile);

                }
            }
        }
    }
}
