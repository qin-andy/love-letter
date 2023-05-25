using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject UnitPrefab;
    public Game Game;

    void Start()
    {
        GameManager = gameObject.GetComponent<GameManager>();
        Game = GameManager.Game;
    }

    public UnitComponent RenderUnit(Unit unit)
    {
        GameObject NewUnitObj = Instantiate(UnitPrefab, Vector3.zero, Quaternion.identity);
        UnitComponent NewUnitComponent = NewUnitObj.GetComponent<UnitComponent>();

        NewUnitComponent.Unit = unit;

        if (unit.HomeTile != null)
        {
            TileComponent HomeTileComponent = GameManager.MapManager.GetTileComponent(unit.HomeTile);
            MoveUnit(NewUnitComponent, HomeTileComponent);
        }
        return NewUnitComponent;
    }

    public void MoveUnit(UnitComponent unitComponent, TileComponent dest)
    {
        float HoverHeight = 1f;
        TileComponent Origin = unitComponent.CurrentTile;
        if (Origin != null)
        {
            Origin.CurrentUnit = null;
        }
        dest.CurrentUnit = unitComponent;
        // unitComponent.transform.SetParent(dest.transform);
        unitComponent.transform.position = dest.transform.position + new Vector3(0f, HoverHeight, 0f);

        Game.Map.WarpUnitToTile(unitComponent.Unit, dest.Tile);
    }
}
