using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<TileState> Tiles;

    GameController Game;
    public GameObject TilePrefab;
    void Start()
    {
        Game = gameObject.GetComponent<GameController>();
    }

    public void BuildTiles()
    {
        List<Vector2Int> Coords = new List<Vector2Int>();
        for (int i = 0; i < 100; i++)
        {
            Coords.Add(new Vector2Int(i % 10, i / 10));
        }
        foreach (Vector2Int Coord in Coords)
        {
            TileState NewTile = CreateTile(Coord);
            Tiles.Add(NewTile);
        }
    }

    TileState CreateTile(Vector2Int coord)
    {
        float YLevel = 0f;
        Vector3 Position = new Vector3(coord.x, YLevel, coord.y);
        GameObject NewTile = Instantiate(TilePrefab, Position, Quaternion.identity);
        NewTile.transform.parent = gameObject.transform;
        TileState NewTileState = NewTile.GetComponent<TileState>();
        NewTileState.Game = Game;
        NewTileState.MapController = this;
        NewTileState.Coord = coord;
        return NewTileState;
    }

    public TileState GetTileAt(Vector2Int coord)
    {
        return Tiles[coord[0]*10 + coord[1]];
    }

    public TileState GetRandomTile()
    {
        TileState RandTile = Tiles[Random.Range(0, Tiles.Count)];
        return RandTile;
    }
}
