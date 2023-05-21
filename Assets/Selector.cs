using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectorState {
    UnitSelection,
    UnitMovement,
    CombatTargetSelection
}

public class Selector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController Game;
    public SelectorState State;
    public UnitState CurrentUnit;
    public TileState CurrentTile;

    public float HoverHeight;
    void Start()
    {
        HoverHeight = 0.5f;
        State = SelectorState.UnitSelection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverSelectTile(TileState targetTile)
    {
        this.transform.position = targetTile.transform.position + new Vector3(0f, HoverHeight, 0f);
        CurrentTile = targetTile;
    }

    public void SelectUnit(UnitState unit)
    {
        if (CurrentUnit != null)
        {
            DeselectUnit();
        }
        CurrentUnit = unit;
        unit.transform.localScale *= 2f;
    }

    public void DeselectUnit()
    {
        CurrentUnit.transform.localScale *= 0.5f;
    }
}
