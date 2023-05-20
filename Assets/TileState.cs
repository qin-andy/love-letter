using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileState : MonoBehaviour
{
    public Vector2Int coord;
    public Color previousColor;
    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to events
        GetComponent<EventManager>().OnPrimaryClicked += OnClicked;
        GetComponent<EventManager>().OnMouseHoverEnter += OnHoverEnter;
        GetComponent<EventManager>().OnMouseHoverLeave += OnHoverLeave;


        // Defaulting color
        previousColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnClicked(System.Object sender, EventArgs e)
    {

    }

    void OnHoverEnter(System.Object sender, EventArgs e)
    {
        previousColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        Debug.Log("Tile at " + coord + " highlighted");
    }
    void OnHoverLeave(System.Object sender, EventArgs e)
    {
        GetComponent<Renderer>().material.SetColor("_Color", previousColor);
    }
}
