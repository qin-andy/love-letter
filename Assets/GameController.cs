using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tile;
    public GameObject currHoveredObj;
    public List<TileState> tiles;

    void Start()
    {
        // Place all tiles
        List<Vector2Int> coords = new List<Vector2Int>();
        for (int i = 0; i < 25; i++)
        {
            coords.Add(new Vector2Int(i % 5, i / 5));
        }
        foreach (Vector2Int coord in coords)
        {
            Vector3 position = new Vector3(coord.x, 0, coord.y);
            GameObject newTile = Instantiate(tile, position, Quaternion.identity);
            newTile.GetComponent<TileState>().coord = coord;
            newTile.transform.parent = gameObject.transform;
            tiles.Add(newTile.GetComponent<TileState>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        PrimaryClick();
        SecondaryClick();
        MouseHover();
    }

    void PrimaryClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedObject = GetMouseGameObject();
            clickedObject?.GetComponent<EventManager>()?.FirePrimaryClickEvent();
        }
    }

    void SecondaryClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject clickedObject = GetMouseGameObject();
            clickedObject?.GetComponent<EventManager>()?.FireSecondaryClickEvent();
        }
    }

    void MouseHover()
    {
        GameObject hoveredObj = GetMouseGameObject();
        // TODO : casework here can be simplified
        if (currHoveredObj == null)
        {
            currHoveredObj = hoveredObj;
            hoveredObj?.GetComponent<EventManager>().FireMouseHoverEnter();
        }
        else if (hoveredObj != currHoveredObj)
        {
            currHoveredObj?.GetComponent<EventManager>().FireMouseHoverLeave();
            hoveredObj?.GetComponent<EventManager>().FireMouseHoverEnter();
            currHoveredObj = hoveredObj;
        }
    }

    GameObject GetMouseGameObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 30.0f))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
