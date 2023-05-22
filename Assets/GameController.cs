using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SelectorPrefab;
    public GameObject DebugMenuPrefab;

    public Selector Selector;
    public CombatController CombatController;
    public UnitController UnitController;
    public MapController Map;

    public CanvasController CanvasController;

    void Start()
    {
        // Place all tiles
        Map = GetComponent<MapController>();
        Map.BuildTiles();

        CombatController = GetComponent<CombatController>();

        GameObject SelectorPrefab = Instantiate(this.SelectorPrefab, Vector3.zero, Quaternion.identity);
        Selector = SelectorPrefab.GetComponent<Selector>();
        Selector.Game = this;

        UnitController = gameObject.GetComponent<UnitController>();

        UnitState Unit1 = UnitController.CreateUnit();
        UnitState Unit2 = UnitController.CreateUnit();

        Unit1.WarpToTile(Map.GetRandomTile());
        Unit2.WarpToTile(Map.GetRandomTile());

        CanvasController = GetComponent<CanvasController>();
        CanvasController.Game = this;
        Vector3 Pos1 = new Vector3(-400f, 250f, 0f);
        Vector3 Pos2 = new Vector3(400f, 250f, 0f);
        CanvasController.AddUnitDisplay(Unit1, Pos1);
        CanvasController.AddUnitDisplay(Unit2, Pos2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

