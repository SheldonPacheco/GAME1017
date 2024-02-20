using UnityEngine;
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

    public GameObject settingsPanelPrefab; // Assign your settings panel prefab in the inspector
    private GameObject settingsPanelContainer;
    private GameObject settingsPanel;

    private Slider masterVolumeSlider;
    private Slider soundFXVolumeSlider;
    private Slider musicVolumeSlider;

    private Text masterVolumeText;
    private Text soundFXVolumeText;
    private Text musicVolumeText;

    private float masterVolume = 0.5f;
    private float soundFXVolume = 0.5f;
    private float musicVolume = 0.5f;

    private bool isSettingsPanelActive = false;

    void Start()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume * masterVolume;
        musicSource.Play();

        soundFXSource.clip = powerfulSoundBallSound;
        soundFXSource.loop = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleSettingsPanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DestroySettingsPanel();
        }
    }

    public void ToggleSettingsPanel()
    {
        InitializeSettingsPanel();
    }

    public void DestroySettingsPanel()
    {
        if (settingsPanelContainer != null)
        {
            Destroy(settingsPanelContainer);
            isSettingsPanelActive = false;
        }
    }

    void InitializeSettingsPanel()
    {
        settingsPanelContainer = new GameObject("SettingsPanelContainer");

        settingsPanelPrefab = Instantiate(settingsPanelPrefab, Vector3.zero, Quaternion.identity, settingsPanelContainer.transform);

        masterVolumeSlider = CreateSlider("MasterVolumeSlider", settingsPanel.transform, new Vector2(150, 50), masterVolume);
        soundFXVolumeSlider = CreateSlider("SoundFXVolumeSlider", settingsPanel.transform, new Vector2(150, 100), soundFXVolume);
        musicVolumeSlider = CreateSlider("MusicVolumeSlider", settingsPanel.transform, new Vector2(150, 150), musicVolume);

        masterVolumeText = CreateText("MasterVolumeText", settingsPanel.transform, new Vector2(300, 50));
        soundFXVolumeText = CreateText("SoundFXVolumeText", settingsPanel.transform, new Vector2(300, 100));
        musicVolumeText = CreateText("MusicVolumeText", settingsPanel.transform, new Vector2(300, 150));

        isSettingsPanelActive = true;
    }

    Slider CreateSlider(string name, Transform parent, Vector2 position, float initialValue)
    {
        GameObject sliderObject = new GameObject(name);
        sliderObject.transform.SetParent(parent);

        RectTransform rectTransform = sliderObject.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;

        Slider slider = sliderObject.AddComponent<Slider>();
        slider.value = initialValue;
        slider.onValueChanged.AddListener((value) => OnSliderValueChanged(name, value));

        return slider;
    }

    void OnSliderValueChanged(string sliderName, float value)
    {
        switch (sliderName)
        {
            case "MasterVolumeSlider":
                masterVolume = value;
                break;
            case "SoundFXVolumeSlider":
                soundFXVolume = value;
                break;
            case "MusicVolumeSlider":
                musicVolume = value;
                break;
        }

        ApplyVolumeSettings();
    }

    void ApplyVolumeSettings()
    {
        musicSource.volume = musicVolume * masterVolume;
        soundFXSource.volume = soundFXVolume * masterVolume;

        // Update text values
        masterVolumeText.text = "Master Volume: " + Mathf.RoundToInt(masterVolume * 100) + "%";
        soundFXVolumeText.text = "Sound FX Volume: " + Mathf.RoundToInt(soundFXVolume * 100) + "%";
        musicVolumeText.text = "Music Volume: " + Mathf.RoundToInt(musicVolume * 100) + "%";
    }

    Text CreateText(string name, Transform parent, Vector2 position)
    {
        GameObject textObject = new GameObject(name);
        textObject.transform.SetParent(parent);

        RectTransform rectTransform = textObject.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;

        Text textComponent = textObject.AddComponent<Text>();
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // You can replace this with your desired font
        textComponent.fontSize = 14;
        textComponent.color = Color.white;

        return textComponent;
    }
}