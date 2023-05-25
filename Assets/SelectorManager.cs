using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject SelectorPrefab;
    public SelectorComponent SelectorComponent;
    public TileComponent SelectedTile;
    public UnitComponent SelectedUnit;

    public Game Game;
    public MapSelector Selector;

    void Start()
    {
       GameObject NewSelector = Instantiate(SelectorPrefab, Vector3.zero, Quaternion.identity);
       SelectorComponent = NewSelector.GetComponent<SelectorComponent>();
    }

    public void HoverTile(TileComponent tileComponent)
    {
        Tile tile = tileComponent.Tile;
        Selector.HoverTile(tile);
        tileComponent.EnableHoverEffect();
        MoveSelector(tileComponent);
    }

    public void UnhoverTile(TileComponent tileComponent)
    {
        Tile tile = tileComponent.Tile;
        Selector.UnhoverTile();
        tileComponent.DisableHoverEffect();
    }

    public void MoveSelector(TileComponent targetTileComponent)
    {
        float HoverHeight = 1f;
        SelectorComponent.transform.position = targetTileComponent.transform.position
            + new Vector3(0, HoverHeight, 0);
    }


    public void SelectTile(TileComponent tileComponent)
    {
        if (Selector.State == SelectorState.SelectingUnit && tileComponent.CurrentUnit != null)
        {
            SelectedTile = tileComponent;
            SelectedUnit = tileComponent.CurrentUnit;
            Selector.SelectTile(tileComponent.Tile);
            Selector.State = SelectorState.SelectingUnitMovement;
        }
        else if (Selector.State == SelectorState.SelectingUnitMovement)
        {
            // Empty Tile clicked state, Move there
            if (tileComponent.Tile.CurrentUnit == null)
            {
                GameManager.UnitManager.MoveUnit(SelectedUnit, tileComponent);
                Selector.State = SelectorState.SelectingUnit;
            }
        }
    }

}
