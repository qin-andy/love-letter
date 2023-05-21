using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    public GameController Game;
    public TileState Tile;

    public int Movement;

    float HoverHeight = 0.5f;
    public float Health;
    public float AttackPower;

    // Start is called before the first frame update
    void Start()
    {
        Movement = 6;
        Health = 10f;
        AttackPower = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Game.MainSelector.SelectUnit(this);
    }

    public void MoveToTile(TileState targetTile)
    {
        if (Tile != null)
        {
            Tile.CurrentUnit = null;
        }
        this.transform.position = targetTile.transform.position + new Vector3(0f, HoverHeight, 0f);
        Tile = targetTile;
        Tile.CurrentUnit = this;
    }

    public void MoveAlongPath(List<TileState> path, SelectorState endState)
    {
        Game.MainSelector.State = SelectorState.Moving;

        Tile.CurrentUnit = null;
        Tile = path[path.Count - 1];
        Tile.CurrentUnit = this;
        StartCoroutine(MoveAlongPathCoroutine(path, endState));
    }

    IEnumerator MoveAlongPathCoroutine(List<TileState> path, SelectorState endState)
    {
        for (int node = 0; node < path.Count; node++)
        {
            Vector3 origin = transform.position;
            Vector3 dest = path[node].transform.position + new Vector3(0f, HoverHeight, 0f);
            for (float i = 0; i < 1f; i += 0.03f)
            {
                Vector3 currLoc = (i * dest) + ((1 - i) * origin);
                transform.position = currLoc;
                yield return null;
            }
        }
        Game.MainSelector.State = endState;
    }

}
