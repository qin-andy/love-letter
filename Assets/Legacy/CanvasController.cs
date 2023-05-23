using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject UnitDisplayPrefab;
    public GameObject Canvas;
    public GameController Game;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddUnitDisplay(UnitState unit, Vector3 pos)
    {
        // new Vector3(-400f, 250f, 0f)
        GameObject NewUnitDisplay = Instantiate(UnitDisplayPrefab);
        NewUnitDisplay.transform.SetParent(Canvas.transform);
        NewUnitDisplay.GetComponent<RectTransform>().localPosition = pos;

        UnitDisplayState NewUnitDisplayState = NewUnitDisplay.GetComponent<UnitDisplayState>();
        NewUnitDisplayState.Unit = unit;
    }
}
