using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public float distance = 2f;
    public void Attack(float damage)
    {
        CrowHealth closestCrow = null;
        var closestDistanceSqr = float.PositiveInfinity;
        foreach (var crow in FlightVariables.instance.crows)
        {
            var diff = transform.position - crow.transform.position;
            if (closestDistanceSqr > diff.sqrMagnitude)
            {
                closestDistanceSqr = diff.sqrMagnitude;
                closestCrow = crow;
            }
        }
        if (closestCrow != null)
        {
            closestCrow.HealthPercentage -= damage;
        }
    }
}
