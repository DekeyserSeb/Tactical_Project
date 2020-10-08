using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class MenuScript //Ce script permet de mettre directement la forme de la carte (asset graphique des tiles)
{

    [MenuItem("Tools/Assign Material")]
    public static void AssignTileMaterial() 
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile"); //ICI on génère un tableau de game
        Material material = Resources.Load<Material>("Tile");

        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = material;
        }
    }

    [MenuItem("Tools/Assign Tile Script")]
    public static void AssignTileScript()
    {

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {
            t.AddComponent<Tile>();
        }


    }
}
