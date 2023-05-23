using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Game Game;
    public GameObject TilePrefab;
    public GameObject UnitPrefab;
    public TileComponent TileComponent;
    void Start()
    {
        Game = new Game();
        Game.Map.BuildTiles();
        RenderTiles();
    }

    void RenderTiles()
    {
        foreach (Tile Tile in Game.Map.Tiles)
        {
            float YLevel = 0f;
            Vector2Int Coord = Tile.Coord;
            Vector3 Position = new Vector3(Coord.x, YLevel, Coord.y);
            GameObject NewTile = Instantiate(TilePrefab, Position, Quaternion.identity);
            NewTile.transform.parent = gameObject.transform;

            TileComponent NewTileComponent = NewTile.GetComponent<TileComponent>();
            NewTileComponent.Tile = Tile;
            Tile.TileComponent = NewTileComponent;
        }
    }
}

public class Game
{
    public Map Map;
    public MapSelector Selector;
    public List<Unit> Units;

    public Game()
    {
        Map = new Map();
        Map.Size = 6;

        Selector = new MapSelector();
        Selector.Map = Map;
    }
}
public class Unit
{
    // A single unit residing on a tile
    public float Health;
    public float AtkPower;
    public int Movement;
    public Tile HomeTile;
}

public class Tile
{
    // All logic related to a tile
    public Vector2Int Coord;
    public Unit CurrentUnit;

    public TileOverlay Overlay;
    public TileComponent TileComponent;

    public Tile(Vector2Int Coord)
    {
        Overlay = new TileOverlay(this);
        this.Coord = Coord;
    }
}

public class Map
{
    public GameObject TilePrefab;
    public List<Tile> Tiles;
    public int Size;

    public Map()
    {
        Tiles = new List<Tile>();
    }

    public void BuildTiles()
    {
        List<Vector2Int> Coords = new List<Vector2Int>();
        for (int i = 0; i < Mathf.Pow(Size, 2); i++)
        {
            Coords.Add(new Vector2Int(i % Size, i / Size));
        }

        foreach (Vector2Int Coord in Coords)
        {
            Tile NewTile = new Tile(Coord);
            NewTile.Coord = Coord;
            Tiles.Add(NewTile);
        }
    }
    public Tile GetTileAt(Vector2Int coord)
    {
        int X = coord.x;
        int Y = coord.y;
        return Tiles[X + Y*Size];
    }

    public Tile GetRandTile()
    {
        Tile RandTile = Tiles[Random.Range(0, Tiles.Count)];
        return RandTile;
    }

    // Raw warp to target tile. No checks: Overwrites existing unit.
    public void WarpUnitToTile(Unit unit, Tile targetTile)
    {
        unit.HomeTile.CurrentUnit = null;
        unit.HomeTile = targetTile;
        targetTile.CurrentUnit = unit;
    }

    public void RenderTiles()
    {

    }
}

public class TileOverlay
{
    // All logic related to Tile decorator visuals
    public Tile Tile;
    public bool HoverHighlighted; // When potentially being selected
    public bool SelectHighlighted; // When actively selected
    public bool PathHighlighted; // When potentially being pathed

    public TileOverlay(Tile tile)
    {
        Tile = tile;
    }
    public void Hover()
    {
        HoverHighlighted = true;
        Tile.TileComponent.EnableHoverEffect();
    }

    public void Unhover()
    {
        HoverHighlighted = false;
        Tile.TileComponent.DisableHoverEffect();
    }
}


public enum SelectorState {
    SelectingUnit,
    SelectingUnitMovement,
    Moving,
}
public class MapSelector
{
    // Object for interacitng with overlay
    public Map Map;
    
    public Tile Location; // Location on map, used for hovering (?)
    public Tile TileSelected; // Currently selected tile
    public Tile TileHovered; // For single hovers
    public Unit UnitSelected; // Currently selected unit

    public SelectorState State;

    public int SelectionPhase; // Phase of seleciotn, TODO: change type;

    public void SelectTile(Tile tile)
    {
        TileSelected = tile;
        UnitSelected = tile.CurrentUnit;
    }

    public void DeselectAll()
    {
        TileSelected = null;
        UnitSelected = null;
    }

    public void HoverTile(Tile tile)
    {
        tile.Overlay.Hover();
        TileHovered = tile;
    }

    public void UnhoverTile()
    {
        TileHovered.Overlay.Unhover();
        TileHovered = null;
    }
}

