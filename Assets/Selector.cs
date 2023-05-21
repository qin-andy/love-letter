using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectorState {
    UnitSelection,
    UnitMovementSelection,
    Moving,
    PostMoving,
}

public class Selector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController Game;
    public SelectorState State;
    public UnitState SelectedUnit;
    public TileState SelectedTile;

    public int UsedMoves;

    public float HoverHeight;

    public List<TileState> MovePath;
    void Start()
    {
        HoverHeight = 0.5f;
        State = SelectorState.UnitSelection;
        MovePath = new List<TileState>();
        UsedMoves = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverSelectTile(TileState targetTile)
    {
        this.transform.position = targetTile.transform.position + new Vector3(0f, HoverHeight, 0f);
        SelectedTile = targetTile;
    }

    public void SelectUnit(UnitState unit)
    {
        if (State == SelectorState.UnitSelection)
        {
            if (SelectedUnit != null)
            {
                DeselectAll();
                State = SelectorState.UnitSelection;
            }
            unit.transform.localScale *= 1.3f;
            SelectedUnit = unit;
            UsedMoves = 0;
            State = SelectorState.UnitMovementSelection;
            unit.Tile.AddToPath();
        }
    }

    public void DeselectAll()
    {
        SelectedUnit.transform.localScale /= 1.3f;
        SelectedUnit = null;
        UsedMoves = 0;
    }

    public void ClearMovement()
    {
        foreach (TileState tile in Game.MainSelector.MovePath)
        {
            tile.PathUnhighlight();
        }
        MovePath.Clear();
        UsedMoves = 0;
        DeselectAll();
    }
}
