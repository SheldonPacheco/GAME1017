using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public Sprite playHoverSprite;
    private SpriteRenderer titleScreenSpriteRenderer;
    private Sprite titleScreenBackground;

    void Start()
    {
        SoundManager.Instance.StopMusic(SoundManager.Instance.deathMusic);
        SoundManager.Instance.PlayMusic(SoundManager.Instance.gameMusic);
        
        titleScreenSpriteRenderer = GameObject.Find("TitleScreen").GetComponent<SpriteRenderer>(); // Replace "TitleScreen" with the actual name of your title screen object.
        titleScreenBackground = titleScreenSpriteRenderer.sprite;
    }

    void Update()
    {
        if (SoundManager.settingsPanel.activeSelf == true || InstructionsButton.currentInstructionsPanel != null)
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
        
    }

    void OnMouseEnter()
    {
        titleScreenSpriteRenderer.sprite = playHoverSprite;
    }

    void OnMouseExit()
    {
        titleScreenSpriteRenderer.sprite = titleScreenBackground;
    }
    void MouseClick()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonPress);
            if (SoundManager.settingsPanel.activeSelf == false)
            {
                
                SceneManager.LoadScene("PlayScene");
            }
        }
    }
}