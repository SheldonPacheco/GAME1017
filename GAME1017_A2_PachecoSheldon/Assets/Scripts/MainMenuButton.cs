using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.StopMusic(SoundManager.Instance.gameMusic);
        SoundManager.Instance.PlayMusic(SoundManager.Instance.deathMusic);
    }
    void Update()
    {
        if (SoundManager.settingsPanel.activeSelf == true)
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

    void MouseClick()
    {
        // Check if the mouse is over the button
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            EventManager.playerHealth = 3;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonPress);
            SceneManager.LoadScene("TitleScene");
        }
    }
}