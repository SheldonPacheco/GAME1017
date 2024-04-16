using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundFXSource;

    public AudioClip gameMusic;
    public AudioClip deathMusic;
    public AudioClip playerHit;
    public AudioClip buttonPress;
    public AudioClip playerRolling;

    public Slider masterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;

    public TMP_Text masterVolumeText;
    public TMP_Text MusicVolumeText;
    public TMP_Text SFXVolumeText;

    public static GameObject settingsPanel;
    private float masterVolume = 0.0f;
    public static SoundManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        settingsPanel = GameObject.Find("SettingsPanel");

        
        masterVolume = 1.0f;
        soundFXSource.volume = 0.3f;
        musicSource.volume = 0.4f;
        InitializeSettingsPanel();

    }

    void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.gameMusic);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

                Time.timeScale = 0f;
                ToggleSettingsPanelVisibility();
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel != null && settingsPanel.activeSelf == true)
            {
                Time.timeScale = 1f;
                ToggleSettingsPanelVisibility();
            }
        }
    }

    void ToggleSettingsPanelVisibility()
    {
          settingsPanel.SetActive(!settingsPanel.activeSelf);
        if (settingsPanel.activeSelf)
        {
            
            SpriteRenderer panelSpriteRenderer = settingsPanel.transform.Find("Background").GetComponent<SpriteRenderer>();
            Color panelColor = panelSpriteRenderer.color;
            panelColor.a = 0.8f;
            panelSpriteRenderer.color = panelColor;
        }
    }

    void InitializeSettingsPanel()
    {
       masterVolumeSlider.value = masterVolume;
       SFXVolumeSlider.value = 0.5f;
       MusicVolumeSlider.value = 0.5f;
       SFXVolumeText.text = soundFXSource.volume.ToString("F2");
       MusicVolumeText.text = musicSource.volume.ToString("F2");
       masterVolumeText.text = masterVolume.ToString("F2");

    }

    public void AdjustSFXVolume(float value)
    {
        soundFXSource.volume = value * masterVolume;
        SFXVolumeText.text = soundFXSource.volume.ToString("F2");
    }

    public void AdjustMusicVolume(float value)
    {
        musicSource.volume = value * masterVolume;
        MusicVolumeText.text = musicSource.volume.ToString("F2");
    }

    public void AdjustMasterVolume(float value)
    {
        masterVolume = soundFXSource.volume = musicSource.volume = value;
        SFXVolumeText.text = masterVolume.ToString("F2");
        MusicVolumeText.text = masterVolume.ToString("F2");
        masterVolumeText.text = masterVolume.ToString("F2");
    }
    public void PlaySFX(AudioClip audioClip)
    {
        soundFXSource.PlayOneShot(audioClip);
    }
    public void PlayMusic(AudioClip audioClip)
    {
        musicSource.PlayOneShot(audioClip);
        musicSource.loop = true;
    }
    public void StopMusic(AudioClip audioClip)
    {
        musicSource.Stop();
    }

}