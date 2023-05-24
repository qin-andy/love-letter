using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject UnitPrefab;
    public GameManager GameManager;
    public Game Game;

    public UnitComponent RenderUnit(Unit unit)
    {
        GameObject NewUnitObj = Instantiate(UnitPrefab, Vector3.zero, Quaternion.identity);
        UnitComponent NewUnitComponent = NewUnitObj.GetComponent<UnitComponent>();

        NewUnitComponent.Unit = unit;
        unit.UnitComponent = NewUnitComponent;

        if (unit.HomeTile != null)
        {
            MoveUnit(NewUnitComponent, unit.HomeTile.TileComponent);
        }
        return NewUnitComponent;
    }

    public void MoveUnit(UnitComponent unitComponent, TileComponent dest)
    {
        float HoverHeight = 0.5f;
        TileComponent Origin = unitComponent.CurrentTile;
        if (Origin != null)
        {
            Origin.CurrentUnit = null;
        }
        dest.CurrentUnit = unitComponent;
        unitComponent.transform.position = dest.transform.position + new Vector3(0f, HoverHeight, 0f);

        unitComponent.transform.SetParent(dest.transform);

    }
}
