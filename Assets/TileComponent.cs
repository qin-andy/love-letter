using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public Tile Tile;
    public bool Hovered;

    void Start()
    {
        
    }

    void OnMouseEnter()
    {
        Tile.Overlay.Hover();
    }

    void OnMouseExit()
    {
        Tile.Overlay.Unhover();
    }

    public void EnableHoverEffect()
    {
        if (!Hovered)
        {
            Hovered = true;
            transform.localScale += new Vector3(0, 0.5f, 0);
        }
    }

    public void DisableHoverEffect()
    {
        if (Hovered)
        {
            Hovered = false;
            transform.localScale -= new Vector3(0, 0.5f, 0);
        }
    }
}
