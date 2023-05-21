using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    public GameController Game;
    public TileState Tile;
    // Start is called before the first frame update
    void Start()
    {
        
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
            Tile.Unit = null;
        }
        float HoverHeight = 0.5f;
        this.transform.position = targetTile.transform.position + new Vector3(0f, HoverHeight, 0f);
        Tile = targetTile;
        Tile.Unit = this;

    }

}
