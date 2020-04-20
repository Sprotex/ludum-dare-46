using UnityEngine;

public class CrowHealth : MonoBehaviour
{
    private float healthPercentage = 1;
    public GameObject crowDeathPrefab;
    public float HealthPercentage
    {
        get
        {
            return healthPercentage;
        }
        set
        {
            healthPercentage = value;
            print("Crow health: " + HealthPercentage.ToString("F2"));
            if (HealthPercentage <= 0f)
            {
                Instantiate(crowDeathPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
