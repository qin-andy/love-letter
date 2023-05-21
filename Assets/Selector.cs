using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController Game;

    public float HoverHeight;
    void Start()
    {
        HoverHeight = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToTile(Vector2Int target)
    {
        this.transform.position = Game.GetTileAt(target).transform.position + new Vector3(0f, HoverHeight, 0f);
    }

    public void HoverSelectTile(TileState targetTile)
    {
        this.transform.position = targetTile.transform.position + new Vector3(0f, HoverHeight, 0f);
    }
}
