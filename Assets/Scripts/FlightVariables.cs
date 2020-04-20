using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightVariables : MonoBehaviour
{
    public static FlightVariables instance;
    public float topLayerHeight = 20f;
    public List<CrowHealth> crows;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
}
