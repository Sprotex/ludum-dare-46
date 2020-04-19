using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Nest : MonoBehaviour
{
    private float hungerPercentage = 1f;
    private AudioSource birdsSource;
    public float hungerDecrementPerSecond = 0.01f;
    public float hungerDecrementAdd = 0.01f;
    public Image hungerIndicator;

    // NOTE(Andy): Property, because it will be integrated with the UI.
    private float HungerPercentage
    {
        get
        {
            return hungerPercentage;
        }
        set
        {
            hungerPercentage = value;
            hungerIndicator.fillAmount = value;
        }
    }

    private IEnumerator HungerIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            hungerDecrementPerSecond += hungerDecrementAdd; 
        }
    }

    private void Start()
    {
        StartCoroutine(HungerIncrease());
        var sound = SoundManager.instance;
        birdsSource = sound.Play(transform.position, sound.birdChirping);
        birdsSource.loop = true;
    }

    public void Update()
    {
        HungerPercentage -= hungerDecrementPerSecond * Time.deltaTime;
    }

    public void Feed(float percentageAmount)
    {
        HungerPercentage = Mathf.Clamp01(HungerPercentage + percentageAmount);
        if (percentageAmount > 0f) Score.instance.Points += 1;
    }
}
