using UnityEngine;

public class PlayerAmbientSounds : MonoBehaviour
{
    private AudioSource ambientLow;
    private AudioSource ambientHigh;
    private void Update()
    {
        if (ambientLow != null)
        {
            var flight = FlightVariables.instance;
            ambientHigh.volume = Mathf.Clamp01(Mathf.InverseLerp(0f, flight.topLayerHeight, transform.position.y));
            ambientLow.volume = 1 - ambientHigh.volume;
        } else
        {
            var sounds = SoundManager.instance;
            ambientLow = sounds.AmbientLowSource;
            ambientHigh = sounds.AmbientHighSource;
            ambientHigh.loop = ambientLow.loop = true;
        }
    }
}
