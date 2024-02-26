using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsButton : MonoBehaviour
{
    public Sprite instructionsHoverSprite;
    private SpriteRenderer titleScreenSpriteRenderer;
    private Sprite titleScreenBackground;
    public GameObject InstructionsPanel;
    public static GameObject currentInstructionsPanel;

    void Start()
    {
        titleScreenSpriteRenderer = GameObject.Find("TitleScreen").GetComponent<SpriteRenderer>();
        titleScreenBackground = titleScreenSpriteRenderer.sprite;
    }

    void Update()
    {
        // Check for input to close the instructions panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInstructionsPanel();
        }

        HandleButtonLayer();
    }

    void OnMouseEnter()
    {
        titleScreenSpriteRenderer.sprite = instructionsHoverSprite;
    }

    void OnMouseExit()
    {
        titleScreenSpriteRenderer.sprite = titleScreenBackground;
    }

    void OnMouseDown()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonPress);
        if (SoundManager.settingsPanel.activeSelf == false)
        {
            ToggleInstructionsPanel();
        }
    }

    private void ToggleInstructionsPanel()
    {
        if (currentInstructionsPanel == null)
        {
            currentInstructionsPanel = Instantiate(InstructionsPanel, Vector3.zero, Quaternion.identity);
            SetTransparency(currentInstructionsPanel, 1.0f);
        }
        else
        {
            Destroy(currentInstructionsPanel);
            currentInstructionsPanel = null;
        }
    }

    private void CloseInstructionsPanel()
    {
        if (currentInstructionsPanel != null)
        {
            Destroy(currentInstructionsPanel);
            currentInstructionsPanel = null;
        }
    }

    private void HandleButtonLayer()
    {
        if (SoundManager.settingsPanel.activeSelf == true || currentInstructionsPanel != null)
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
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