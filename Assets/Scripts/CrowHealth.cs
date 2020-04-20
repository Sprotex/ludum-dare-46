using UnityEngine;

public class CrowHealth : MonoBehaviour
{
    private float healthPercentage = 1;
    public GameObject crowDeathPrefab;

    private void Start()
    {
        FlightVariables.instance.crows.Add(this);
    }

    private void OnDestroy()
    {
        FlightVariables.instance.crows.Remove(this);
    }

    public float HealthPercentage
    {
        get
        {
            return healthPercentage;
        }
        set
        {
            if (healthPercentage > value && value > 0f)
            {
                SoundManager.instance.Play(transform.position, SoundManager.instance.fistHit);
            }
            healthPercentage = value;
            if (HealthPercentage <= 0f)
            {
                SoundManager.instance.Play(transform.position, SoundManager.instance.enemyHit);
                Instantiate(crowDeathPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
