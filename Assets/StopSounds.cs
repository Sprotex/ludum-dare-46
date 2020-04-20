using UnityEngine;

public class StopSounds : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.StopSounds();
    }
}
