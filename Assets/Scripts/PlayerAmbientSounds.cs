using UnityEngine;

public class PlayerAmbientSounds : MonoBehaviour
{
    private AudioSource ambientLow;
    private AudioSource ambientHigh;
    private SoundManager sounds;

    private void Start()
    {
        sounds = SoundManager.instance;
    }
    private void Update()
    {
        if (ambientLow != null)
        {
            var flight = FlightVariables.instance;
            var ambientHighVolume = Mathf.Clamp01(Mathf.InverseLerp(0f, flight.topLayerHeight, transform.position.y));
            var ambientLowVolume = 1 - ambientHighVolume;
            ambientLow.volume = 1 - ambientHigh.volume;
            sounds.SetSoundVolume(ambientHigh, ambientHighVolume);
            sounds.SetSoundVolume(ambientLow, ambientLowVolume);
        } else
        {
            var sounds = SoundManager.instance;
            ambientLow = sounds.AmbientLowSource;
            ambientHigh = sounds.AmbientHighSource;
            ambientHigh.loop = ambientLow.loop = true;
        }
    }
}
