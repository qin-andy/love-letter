using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Tile;
    public GameObject SelectorObj;
    public Selector MainSelector;
    public List<TileState> Tiles;

    void Start()
    {
        // Place all tiles
        List<Vector2Int> Coords = new List<Vector2Int>();
        for (int i = 0; i < 25; i++)
        {
            Coords.Add(new Vector2Int(i % 5, i / 5));
        }
        foreach (Vector2Int Coord in Coords)
        {
            Vector3 Position = new Vector3(Coord.x, 0, Coord.y);
            GameObject NewTile = Instantiate(Tile, Position, Quaternion.identity);
            NewTile.GetComponent<TileState>().Coord = Coord;
            NewTile.transform.parent = gameObject.transform;
            TileState NewTileState = NewTile.GetComponent<TileState>();
            NewTileState.Game = this;
            Tiles.Add(NewTile.GetComponent<TileState>());
        }

        GameObject SingleSelectorObj = Instantiate(SelectorObj, Vector3.zero, Quaternion.identity);
        MainSelector = SingleSelectorObj.GetComponent<Selector>();
        MainSelector.Game = this;
    }

    public TileState GetTileAt(Vector2Int coord)
    {
        return Tiles[coord[0]*5 + coord[1]];
    }

    // Update is called once per frame
    void Update()
    {

    }
}

