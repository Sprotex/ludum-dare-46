using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public int simultaneousDynamicSounds = 64;
    public AudioClip mainMusic;
    public AudioClip hit;
    public AudioClip ambientHigh;
    public AudioClip ambientLow;
    public AudioClip birdChirping;
    private AudioSource[] sources;
    private int sourceIndex = 0;
    private float maxSoundVolume;
    private float maxMusicVolume;
    public AudioSource Music { get; private set; }
    public AudioSource AmbientLowSource { get; private set; }
    public AudioSource AmbientHighSource { get; private set; }

    public void SetSoundVolume(AudioSource source, float volume)
    {
        source.volume = maxSoundVolume * volume;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private AudioSource CreateSource(string name)
    {
        var audioObject = new GameObject(name);
        audioObject.transform.SetParent(transform);
        var result = audioObject.AddComponent<AudioSource>();
        result.playOnAwake = false;
        return result;
    }

    private void Start()
    {
        SetupVolume();
        SetupDynamicAudio();
        StartPermanent2DAudio();
    }

    public void OnMusicVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat(CConstants.PPrefs.Strings.MusicVolume, volume);
        SetupVolume();
    }

    public void OnSoundVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat(CConstants.PPrefs.Strings.SoundVolume, volume);
        SetupVolume();
    }

    private void SetupVolume()
    {
        maxSoundVolume = PlayerPrefs.GetFloat(CConstants.PPrefs.Strings.SoundVolume, CConstants.PPrefs.DefaultValues.SoundVolume);
        maxMusicVolume = PlayerPrefs.GetFloat(CConstants.PPrefs.Strings.MusicVolume, CConstants.PPrefs.DefaultValues.MusicVolume);
        if (Music != null)
        {
            Music.volume = maxMusicVolume;
        }
    }

    private void SetupDynamicAudio()
    {
        sources = new AudioSource[simultaneousDynamicSounds];
        for (var i = 0; i < 64; ++i)
        {
            sources[i] = CreateSource("Audio " + i.ToString());
            sources[i].spatialBlend = 1f;
        }
    }

    private void StartPermanent2DAudio()
    {
        Music = CreateSource("Music");
        Music.spatialBlend = 0f;
        Music.clip = mainMusic;
        Music.volume = maxMusicVolume;
        Music.Play();
        Music.loop = true;
        AmbientHighSource = CreateSource("Ambient High Source");
        SetSoundVolume(AmbientHighSource, 0f);
        AmbientHighSource.clip = ambientHigh;
        AmbientHighSource.Play();
        AmbientLowSource = CreateSource("Ambient Low Source");
        SetSoundVolume(AmbientLowSource, 0f);
        AmbientLowSource.clip = ambientLow;
        AmbientLowSource.Play();
    }

    public AudioSource Play(Vector3 position, AudioClip clip)
    {
        var currentSourceIndex = sourceIndex++;
        if (sourceIndex >= sources.Length)
        {
            sourceIndex = 0;
        }
        var source = sources[currentSourceIndex];
        source.clip = clip;
        source.transform.position = position;
        source.Play();
        return source;
    }
}
