using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitDisplayState : MonoBehaviour
{
    // Start is called before the first frame update
    public string UnitName;
    public UnitState Unit;

    void Start()
    {
        string text = "Health: " + Unit.Health;
        gameObject.GetComponent<TMP_Text>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        string text = "Health: " + Unit.Health;
        gameObject.GetComponent<TMP_Text>().text = text;
    }
}
