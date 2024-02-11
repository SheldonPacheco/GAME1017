using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider PanningSlider;
    [SerializeField] private TMP_Text sfxDisplay;
    [SerializeField] private TMP_Text musicDisplay;
    [SerializeField] private TMP_Text masterDisplay;
    [SerializeField] private TMP_Text panningDisplay;
    [SerializeField] private TMP_Text maxSFXDisplay;
    [SerializeField] private TMP_Text maxMusicDisplay;
    private float masterVolume = 0.0f;
    public enum SoundType
    {
        SOUND_SFX,
        SOUND_MUSIC
    }
    public static SoundManager Instance { get; private set; }
    private Dictionary<string, AudioClip> soundfxDictionary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> musicDictionary = new Dictionary<string, AudioClip>();
    private AudioSource soundfxSource;
    private AudioSource musicSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        } else
        {
            Destroy(gameObject);
        }

        PanningSlider.value = musicSource.panStereo; panningDisplay.text = musicSource.panStereo.ToString("F2"); panningDisplay.text = soundfxSource.panStereo.ToString("F2");
        PanningSlider.value = soundfxSource.panStereo;

        masterSlider.value = soundfxSource.volume + soundfxSource.volume; masterDisplay.text = musicSource.volume.ToString("F2"); masterDisplay.text = soundfxSource.volume.ToString("F2");

        musicSlider.value = soundfxSource.volume; musicDisplay.text = musicSource.volume.ToString("F2");

        sfxSlider.value = soundfxSource.volume; sfxDisplay.text = musicSource.volume.ToString("F2");

        masterSlider.value = masterVolume;
    }
  
    private void Initialize()
    {
        soundfxSource = gameObject.AddComponent<AudioSource>();
        soundfxSource.volume = 1.0f;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.volume = 1.0f;
        musicSource.loop = true;
    }
    public void AdjustSFXVolume(float value)
    {
        soundfxSource.volume = value * masterVolume;
        sfxDisplay.text = soundfxSource.volume.ToString("F2");
    }
    public void AdjustMusicVolume(float value)
    {
        musicSource.volume = value * masterVolume;
        musicDisplay.text = musicSource.volume.ToString("F2");
    }
    public void AdjustMasterVolume(float value)
    {
        masterVolume = soundfxSource.volume = musicSource.volume = value;
        maxMusicDisplay.text = masterVolume.ToString("F1");
        maxSFXDisplay.text = masterVolume.ToString("F1");
        masterDisplay.text = masterVolume.ToString("F2");

    }
    public void AdjustSteroPanning(float value) {
        musicSource.panStereo = value;
        soundfxSource.panStereo = value;
        panningDisplay.text = value.ToString();
    }
   
    public void AddSound(string soundKey, AudioClip audioClip, SoundType soundType)
    {
        Dictionary<string, AudioClip> targetDictionary = GetDictionaryByType(soundType);    
        if(!targetDictionary.ContainsKey(soundKey))
        {
            targetDictionary.Add(soundKey, audioClip);

        }
        else
        {
            Debug.LogWarning("Sound key" + soundKey + "already exists in the " + soundType + " dictionary.");
        }
    }

    
    public void PlaySound(string soundKey)
    {
        Play(soundKey, SoundType.SOUND_SFX);
    }

    
    public void PlayMusic(string soundKey)
    {
        musicSource.Stop();
        Play(soundKey, SoundType.SOUND_MUSIC);
    }

   
    private void Play(string soundKey, SoundType soundType)
    {
        Dictionary<string, AudioClip> targetDictionary;
        AudioSource targetSource;

        SetTargetsByType(soundType, out targetDictionary, out targetSource);

        if (targetDictionary.ContainsKey(soundKey))
        {
            targetSource.PlayOneShot(targetDictionary[soundKey]);
        }
        else
        {
            Debug.LogWarning("Sound key " + soundKey + " not found in the " + soundType + " dictionary.");
        }
    }

    private void SetTargetsByType(SoundType soundType, out Dictionary<string, AudioClip> targetDictionary, out AudioSource targetSource)
    {
        switch (soundType) 
        { 
         case SoundType.SOUND_SFX:
                targetDictionary = soundfxDictionary;
                targetSource = soundfxSource;
                break;
         case SoundType.SOUND_MUSIC:
                targetDictionary = musicDictionary;
                targetSource = musicSource;
                break;
        default:
                Debug.LogError("Unknown sound type: " + soundType);
                targetDictionary = null;
                targetSource = null;
                break;
        }
    }
    private Dictionary<string, AudioClip> GetDictionaryByType(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                return soundfxDictionary;
            case SoundType.SOUND_MUSIC:
                return musicDictionary;
            default:
                Debug.LogError("Unknown sound type: " + soundType);
                return null;
        }
    }
}