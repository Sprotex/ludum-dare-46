using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSliders : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;
    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(CConstants.PPrefs.Strings.MusicVolume, CConstants.PPrefs.DefaultValues.MusicVolume);
    }
}
