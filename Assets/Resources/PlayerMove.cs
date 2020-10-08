using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove //ATTENTION ICI C'EST pas MONOBEHAVIOUR
{
    [SerializeField] bool DFS = true;
    [SerializeField] bool Bidirectionnal = false;
    [SerializeField] bool Greedy = false;
    [SerializeField] bool Beam = false;
    [SerializeField] bool Optimal = false;
    [SerializeField] bool MoveButton = false;
    [SerializeField] int BeamLargeur = 3;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        FindSelectablesTilesDFS();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            RemoveSelectableTiles();
            FindSelectablesTilesDFS();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            RemoveSelectableTiles();
            FindSelectablesTilesDFS();
        }
        if (!moving)
        {
            //FindSelectablesTilesDFS();
            CheckMouse();
        }
        else
        {
            Move();
        }
    }

    private void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile goal = hit.collider.GetComponent<Tile>();
                    if (goal.selectable)
                    {
                        if (DFS)
                        {
                            DFSMove(goal);
                        } else if (Bidirectionnal)
                        {
                            BiDirectionnalMove(goal);
                        }
                        else if (Greedy)
                        {
                            GreedySearchMove(goal);
                        }
                        else if (Beam)
                        {
                            BeamSearchMove(goal, BeamLargeur);
                        }
                        else if (Optimal)
                        {
                            OptimalSearchBBAMove(goal);
                        }
                        else if (MoveButton)
                        {
                            MoveToTile(goal);
                        }

                    }
                }
            }
        }
    }


    public void SetDFS()
    {
        DFS = true;
        Bidirectionnal = false;
        Greedy = false;
        Beam = false;
        Optimal = false;
        MoveButton = false;
    }
    public void SetBidir()
    {
        DFS = false;
        Bidirectionnal = true;
        Greedy = false;
        Beam = false;
        Optimal = false;
        MoveButton = false;
    }
    public void SetGreedy()
    {
        DFS = false;
        Bidirectionnal = false;
        Greedy = true;
        Beam = false;
        Optimal = false;
        MoveButton = false;
    }
    public void SetBeam()
    {
        DFS = false;
        Bidirectionnal = false;
        Greedy = false;
        Beam = true;
        Optimal = false;
        MoveButton = false;
    }
    public void SetOptimal()
    {
        DFS = false;
        Bidirectionnal = false;
        Greedy = false;
        Beam = false;
        Optimal = true;
        MoveButton = false;
    }
    public void SetMove()
    {
        DFS = false;
        Bidirectionnal = false;
        Greedy = false;
        Beam = false;
        Optimal = false;
        MoveButton = true;
    }
    public void SetReset()
    {
        FindSelectablesTilesDFS();
    }
    public void SetBeamLargeur()
    {
        if (BeamLargeur == 3)
        {
            BeamLargeur = 2;
        }
        else
        {
            BeamLargeur = 3;
        }
    }
}
