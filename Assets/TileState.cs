using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileState : MonoBehaviour
{
    public GameController Game;
    public Vector2Int Coord;
    public Color PrevColor;

    public UnitState Unit;
    // Start is called before the first frame update
    void Start()
    {
        // Defaulting color
        PrevColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnMouseDown()
    {
        
    }
    
    void OnMouseEnter()
    {
        PrevColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        Debug.Log("Tile at " + Coord + " highlighted");

        Game.MainSelector.HoverSelectTile(this);
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.SetColor("_Color", PrevColor);
    }

}
