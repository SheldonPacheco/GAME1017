using UnityEngine;

public class MainManagerLoad : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject InstructionsButton;
    public GameObject titleScreen;

    public GameObject instructionsPanel;
    public static GameObject currentInstructionsPanel;

    private SpriteRenderer titleScreenSpriteRenderer;
    private Sprite titleScreenBackground;

    public Sprite playHoverSprite;
    public Sprite instructionsHoverSprite;

    float instructionsTransparency = 1.0f; //1.0 = 0% translucent | 0.1 = 90% translucent
    public static MainManagerLoad Instance { get; private set; }
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        titleScreenSpriteRenderer = titleScreen.GetComponent<SpriteRenderer>();
        titleScreenBackground = titleScreenSpriteRenderer.sprite;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInstructionsPopup();
        }

        if (currentInstructionsPanel != null && !currentInstructionsPanel.activeSelf)
        {
            Destroy(currentInstructionsPanel);
            currentInstructionsPanel = null;
        }
    }

    public void PlayButtonHoverEnter()
    {
        titleScreenSpriteRenderer.sprite = playHoverSprite;
    }

    public void InstructionsButtonHoverEnter()
    {
        titleScreenSpriteRenderer.sprite = instructionsHoverSprite;
    }

    public void OnHoverExit()
    {
        titleScreenSpriteRenderer.sprite = titleScreenBackground;
    }

    public void InstructionsButtonClick()
    {
        if (currentInstructionsPanel == null)
        {
            currentInstructionsPanel = Instantiate(instructionsPanel, Vector3.zero, Quaternion.identity);
            SetTransparency(currentInstructionsPanel, instructionsTransparency);
        }
        else
        {
            CloseInstructionsPopup();
        }
    }

    private void CloseInstructionsPopup()
    {
        if (currentInstructionsPanel != null)
        {
            Destroy(currentInstructionsPanel);
            currentInstructionsPanel = null;
        }
    }

    private void SetTransparency(GameObject panel, float alpha)
    {
        if (panel != null)
        {
            Color panelColor = panel.GetComponent<SpriteRenderer>().color;
            panelColor.a = alpha;
            panel.GetComponent<SpriteRenderer>().color = panelColor;
        }
    }
}