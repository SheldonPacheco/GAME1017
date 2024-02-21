using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    
    public AudioSource musicSource;
    public AudioSource soundFXSource;

    public AudioClip gameMusic;
    public AudioClip deathMusic;
    public AudioClip snowballHitSound;
    public AudioClip powerfulSoundBallSound;
    public AudioClip deathSound;

    public GameObject settingsPanel;
    public static GameObject currentSettingsPanel;

    public Slider masterVolumeSlider;
    public Slider soundFXVolumeSlider;
    public Slider musicVolumeSlider;

    public TMP_Text masterVolumeText;
    public TMP_Text soundFXVolumeText;
    public TMP_Text musicVolumeText;

    public float masterVolume = 0.0f;
    void Start()
    {
        
        
    }

    void PlayMusic()
    {
            
    }
    void Update()
    {  
            if (Input.GetKeyDown(KeyCode.P))
            {
            if (MainManagerLoad.currentInstructionsPanel == null)
            {
                ToggleSettingsPanel();
            }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DestroySettingsPanel();
            }
        
    }

    void ToggleSettingsPanel()
    {
        if (currentSettingsPanel == null)
        {
            
            InitializeSettingsPanel();
            
        }
        else
        {
            DestroySettingsPanel();
        }
    }

    void DestroySettingsPanel()
    {
        if (currentSettingsPanel != null)
        {
            Destroy(currentSettingsPanel);
            currentSettingsPanel = null;
            
        }
    }

    void InitializeSettingsPanel()
    {
        currentSettingsPanel = Instantiate(settingsPanel, Vector3.zero, Quaternion.identity);

        
        masterVolumeSlider = GetMasterVolumeSlider();
        soundFXVolumeSlider = GetSFXVolumeSlider();
        musicVolumeSlider = GetMusicVolumeSlider();

        masterVolumeText = GetMasterVolumeText();
        soundFXVolumeText = GetSFXVolumeText();
        musicVolumeText = GetMusicVolumeText();

        GetMasterVolumeSlider().value = masterVolume = soundFXSource.volume = musicSource.volume;
        GetMusicVolumeSlider().value = musicSource.volume;
        GetSFXVolumeSlider().value = soundFXSource.volume;

        soundFXVolumeText.text = soundFXSource.volume.ToString("F2");
        musicVolumeText.text = musicSource.volume.ToString("F2");
        masterVolumeText.text = masterVolume.ToString("F2");


    }
    public void AdjustSFXVolume(float value)
    {
        soundFXSource.volume = value * masterVolume;
        soundFXVolumeText.text = soundFXSource.volume.ToString("F2");
    }
    public void AdjustMusicVolume(float value)
    {
        musicSource.volume = value * masterVolume;
        musicVolumeText.text = musicSource.volume.ToString("F2");
    }
    public void AdjustMasterVolume(float value)
    {
        masterVolume = soundFXSource.volume = musicSource.volume = value;
        masterVolumeText.text = masterVolume.ToString("F2");

    }
    public Canvas GetCanvasChildren()
    {
        return currentSettingsPanel.GetComponentInChildren<Canvas>();
    }
    public Slider GetMasterVolumeSlider()
    {
        return GetCanvasChildren().transform.Find("MasterVolumeSlider").GetComponent<Slider>();
    }
    public Slider GetSFXVolumeSlider()
    {
        return GetCanvasChildren().transform.Find("SFXVolumeSlider").GetComponent<Slider>();
    }
    public Slider GetMusicVolumeSlider()
    {
        return GetCanvasChildren().transform.Find("MusicVolumeSlider").GetComponent<Slider>();
    }
    public TMP_Text GetMasterVolumeText()
    {
        return GetMasterVolumeSlider().transform.Find("MasterVolumeText").GetComponent<TMP_Text>();
    }
    public TMP_Text GetSFXVolumeText()
    {
        return GetSFXVolumeSlider().transform.Find("SFXVolumeText").GetComponent<TMP_Text>();
    }

    public TMP_Text GetMusicVolumeText()
    {
        return GetMusicVolumeSlider().transform.Find("MusicVolumeText").GetComponent<TMP_Text>();
    }
}