using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Game Game;
    public Map Map;
    public GameObject TilePrefab;
    public GameObject UnitPrefab;
    public UnitManager UnitManager;
    public MapManager MapManager;
    public SelectorManager SelectorManager;
    void Start()
    {
        Game = new Game();
        Map = Game.Map;
        Map.BuildTiles();

        // Set up references
        MapManager = gameObject.GetComponent<MapManager>();
        MapManager.GameManager = this;
        MapManager.Game = Game;
        MapManager.Map = Map;

        UnitManager = gameObject.GetComponent<UnitManager>();
        UnitManager.GameManager = this;
        UnitManager.Game = Game;

        SelectorManager = gameObject.GetComponent<SelectorManager>();
        SelectorManager.GameManager = this;
        SelectorManager.Game = Game;
        SelectorManager.Selector = Game.Selector;


        // Set up additional references to Managers
        MapManager.SelectorManager = SelectorManager;

        // Run Setups
        MapManager.RenderTiles();

        Unit Unit1 = new Unit();
        Map.WarpUnitToTile(Unit1, Map.GetRandTile());
        UnitManager.RenderUnit(Unit1);
    }
}