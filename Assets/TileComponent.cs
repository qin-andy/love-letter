using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager GameManager;
    public MapManager MapManager;
    public SelectorManager SelectorManager;
    public Tile Tile;
    public UnitComponent CurrentUnit;
    public bool Hovered;

    void OnMouseEnter()
    {
        SelectorManager.HoverTile(this);
    }

    void OnMouseExit()
    {
        SelectorManager.UnhoverTile(this);
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
