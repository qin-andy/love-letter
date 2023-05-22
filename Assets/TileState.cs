using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileState : MonoBehaviour
{
    public GameController Game;
    public MapController MapController;
    public Vector2Int Coord;
    public Color PrevColor;

    public UnitState CurrentUnit;
    public bool SelectHighlighted;
    public bool PathHighlighted;
    // Start is called before the first frame update
    public IEnumerator PathCoroutine;
    void Start()
    {
        // Defaulting color
        PrevColor = GetComponent<Renderer>().material.color;
        SelectHighlighted = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnMouseDown()
    {
        if (Game.Selector.State == SelectorPhase.UnitSelection && CurrentUnit != null)
        {
            /**
                Clicked on a tile functionality:
                1. Hover unhighlight
                2. Select the unit on current tile
            */
            HoverUnhighlight();
            Game.Selector.SelectUnit(CurrentUnit);
        }
        else if (Game.Selector.State == SelectorPhase.UnitMovementSelection && ValidMoveTarget())
        {
            List<TileState> pathClone = new List<TileState>(Game.Selector.MovePath);

            // MainSelector state is handled in hte Move Along Path coroutine
            Game.Selector.SelectedUnit.MoveAlongPath(pathClone, SelectorPhase.UnitSelection);

            Game.Selector.ClearMovement();
        }
        else if (Game.Selector.State == SelectorPhase.UnitMovementSelection && ValidAttackTarget())
        {
            Debug.Log("Arranging combat!");
            List<TileState> pathClone = new List<TileState>(Game.Selector.MovePath);
            Game.CombatController.DoCombat(Game.Selector.SelectedUnit, CurrentUnit);
            Game.Selector.SelectedUnit.MoveAlongPath(pathClone, SelectorPhase.UnitSelection);
            Game.Selector.ClearMovement();
        }
        // else if (Game.MainSelector.State == SelectorState.PostMoving)
        // {
        //     foreach (TileState tile in Game.MainSelector.MovePath)
        //     {
        //         tile.PathUnhighlight();
        //     }
        //     Game.MainSelector.MovePath.Clear();
        //     Game.MainSelector.UsedMoves = 0;
        //     Game.MainSelector.DeselectAll();
        //     Game.MainSelector.State = SelectorState.UnitSelection;

        //     // Otherwise, reverse the move
        //     else
        //     {
        //         List<TileState> pathClone = new List<TileState>(Game.MainSelector.MovePath);
        //         pathClone.Reverse();
        //         Game.MainSelector.SelectedUnit.MoveAlongPath(pathClone, SelectorState.UnitSelection);
        //     }
        // }
    }

    bool ValidMoveTarget()
    {
        return PathHighlighted && CurrentUnit == null;
    }

    void OnMouseEnter()
    {
        Game.Selector.HoverSelectTile(this);

        if (Game.Selector.State == SelectorPhase.UnitSelection)
        {
            HoverHighlight();
        }
        else if ((CurrentUnit == null || CurrentUnit == Game.Selector.SelectedUnit)
                    && Game.Selector.State == SelectorPhase.UnitMovementSelection)
        {
            TryAddingSelfToPath();
        }
        else if (CurrentUnit != null 
                    && CurrentUnit != Game.Selector.SelectedUnit
                    && Game.Selector.State == SelectorPhase.UnitMovementSelection)
        {
            // Enemy highlighting
            GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
        }


    }
    void OnMouseExit()
    {
        if (Game.Selector.State == SelectorPhase.UnitSelection)
        {
            HoverUnhighlight();
        }
        else if (CurrentUnit != null 
                    && CurrentUnit != Game.Selector.SelectedUnit
                    && Game.Selector.State == SelectorPhase.UnitMovementSelection)
        {
            // Enemy highlighting
            GetComponent<Renderer>().material.SetColor("_Color", PrevColor);
        }
    }

    void TryAddingSelfToPath()
    {
        // Attempts to draw a contiguous path frm the currently highlighted unit
        // The selector entering a tile attempts to add the current tile to the path
        // If it's already in the path, rewinds the path to current tile

        // If already path highlighted, "Rewind" path to current tile]
        Debug.Log("Attempting to add to path: " + Coord);
        if (PathHighlighted)
        {
            List<TileState> MovePath = Game.Selector.MovePath;
            TileState Top = MovePath[MovePath.Count - 1];
            while (Top != this && MovePath.Count > 0)
            {
                Top.PathUnhighlight();
                Game.Selector.UsedMoves -= 1;
                MovePath.RemoveAt(MovePath.Count - 1);
                Top = MovePath[MovePath.Count - 1];
            }
        }

        // If not part of path and there is movement left, attempt ot add to path
        else if (ValidPathTarget())
        {
            List<TileState> MovePath = Game.Selector.MovePath;

            if (MovePath.Count == 0)
            {
                AddToPath();
            }
            else
            {
                TileState Top = MovePath[MovePath.Count - 1];
                if ((Top.Coord - Coord).magnitude == 1)
                {
                    AddToPath();
                }
            }
        }
    }

    public void AddToPath()
    {
        PathHighlight();
        Game.Selector.MovePath.Add(this);
        Game.Selector.UsedMoves += 1;
        Debug.Log(Game.Selector.UsedMoves);
    }

    bool ValidPathTarget()
    {
        return !PathHighlighted
            && Game.Selector.UsedMoves <= Game.Selector.SelectedUnit.Movement
            && CurrentUnit == null;
    }

    bool ValidAttackTarget()
    {
        TileState Top = Game.Selector.MovePath[Game.Selector.MovePath.Count - 1];
        bool InRange = (Top.Coord - Coord).magnitude == 1;
        return CurrentUnit != null
            && InRange;
    }

    void HoverHighlight()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        SelectHighlighted = true;
    }

    void HoverUnhighlight()
    {
        GetComponent<Renderer>().material.SetColor("_Color", PrevColor);
        SelectHighlighted = false;
    }

    void PathHighlight()
    {
        // GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        StopAllCoroutines();
        PathCoroutine = PathHighlightCoroutine();
        StartCoroutine(PathCoroutine);
        transform.localScale += new Vector3(0f, 0.2f, 0f);
        PathHighlighted = true;
    }

    public void PathUnhighlight()
    {
        StopAllCoroutines();
        StartCoroutine(PathUnhighlightCoroutine());
        transform.localScale -= new Vector3(0f, 0.2f, 0f);
        PathHighlighted = false;
    }

    IEnumerator PathHighlightCoroutine()
    {
        Color target = Color.white;
        Color curr = GetComponent<Renderer>().material.color;
        for (float i = 0; i < 1f; i += 0.02f)
        {
            Color c = ((target * i) + (curr * (1 - i)));
            GetComponent<Renderer>().material.SetColor("_Color", c);
            yield return null;
        }
    }

    IEnumerator PathUnhighlightCoroutine()
    {
        Color target = PrevColor;
        Color curr = GetComponent<Renderer>().material.color;
        for (float i = 0; i < 1f; i += 0.02f)
        {
            Color c = ((target * i) + (curr * (1 - i)));
            GetComponent<Renderer>().material.SetColor("_Color", c);
            yield return null;
        }
    }

}
