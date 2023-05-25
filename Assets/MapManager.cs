using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameManager GameManager;
    public SelectorManager SelectorManager;
    public GameObject TilePrefab;


    public Game Game;
    
    public Map Map;

    public List<TileComponent> TileComponents;

    public void RenderTiles()
    {
        foreach (Tile Tile in Game.Map.Tiles)
        {
            float YLevel = 0f;
            Vector2Int Coord = Tile.Coord;
            Vector3 Position = new Vector3(Coord.x, YLevel, Coord.y);
            GameObject NewTile = Instantiate(TilePrefab, Position, Quaternion.identity);
            NewTile.transform.parent = gameObject.transform;

            TileComponent NewTileComponent = NewTile.GetComponent<TileComponent>();
            NewTileComponent.GameManager = GameManager;
            NewTileComponent.MapManager = this;
            NewTileComponent.SelectorManager = SelectorManager;

            TileComponents.Add(NewTileComponent);
            NewTileComponent.Tile = Tile;
        }
    }


    public TileComponent GetTileComponent(Tile tile)
    {
        return this.GetTileComponent(tile.Coord);
    }

    public TileComponent GetTileComponent(Vector2Int coord)
    {
        int X = coord.x;
        int Y = coord.y;
        return TileComponents[X + Y*Map.Size];
    }
}
