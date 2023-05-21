using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Tile;
    public GameObject SelectorObj;
    public GameObject UnitObj;
    public GameObject UnitDisplayObj;
    public GameObject Canvas;

    public Selector MainSelector;
    public List<TileState> Tiles;
    public CombatController MainCombatController;

    void Start()
    {
        // Place all tiles
        List<Vector2Int> Coords = new List<Vector2Int>();
        for (int i = 0; i < 100; i++)
        {
            Coords.Add(new Vector2Int(i % 10, i / 10));
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


        GameObject EgUnitObj = Instantiate(UnitObj, Vector3.zero, Quaternion.identity);
        UnitState EgUnitState = EgUnitObj.GetComponent<UnitState>();
        EgUnitState.Game = this;

        GameObject EgUnitObj2 = Instantiate(UnitObj, Vector3.zero, Quaternion.identity);
        UnitState EgUnitState2 = EgUnitObj2.GetComponent<UnitState>();
        EgUnitState2.Game = this;

        TileState RandTile = Tiles[Random.Range(0, Tiles.Count)];
        EgUnitState.MoveToTile(RandTile);

        RandTile = Tiles[Random.Range(0, Tiles.Count)];
        EgUnitState2.MoveToTile(RandTile);

        GameObject UnitDisplay1Obj = Instantiate(UnitDisplayObj);
        UnitDisplay1Obj.transform.SetParent(Canvas.transform);
        UnitDisplay1Obj.GetComponent<RectTransform>().localPosition = new Vector3(-400f, 250f, 0f);

        UnitDisplayState UnitDisplay1 = UnitDisplay1Obj.GetComponent<UnitDisplayState>();
        UnitDisplay1.Unit = EgUnitState;

        GameObject UnitDisplay2Obj = Instantiate(UnitDisplayObj);
        UnitDisplay2Obj.transform.SetParent(Canvas.transform);
        UnitDisplay2Obj.GetComponent<RectTransform>().localPosition = new Vector3(400f, 250f, 0f);

        UnitDisplayState UnitDisplay2 = UnitDisplay2Obj.GetComponent<UnitDisplayState>();
        UnitDisplay2.Unit = EgUnitState2;

        MainCombatController = gameObject.AddComponent<CombatController>();
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

