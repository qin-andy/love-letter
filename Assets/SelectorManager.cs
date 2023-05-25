using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject SelectorPrefab;

    public Game Game;
    public MapSelector Selector;

    public void HoverTile(TileComponent tileComponent)
    {
        Tile tile = tileComponent.Tile;
        tile.Overlay.Hover();
        tileComponent.EnableHoverEffect();
    }

    public void UnhoverTile(TileComponent tileComponent)
    {
        Tile tile = tileComponent.Tile;
        tile.Overlay.Unhover();
        tileComponent.DisableHoverEffect();
    }
}
