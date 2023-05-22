using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public GameController Game;
    public GameObject UnitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Game = gameObject.GetComponent<GameController>();
    }

    public UnitState CreateUnit()
    {
        GameObject UnitObj = Instantiate(UnitPrefab, Vector3.zero, Quaternion.identity);
        UnitState NewUnitState = UnitObj.GetComponent<UnitState>();
        NewUnitState.Game = Game;

        return NewUnitState;
    }

}
