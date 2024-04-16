using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // Update is called once per frame
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
            EventManager.playerHealth = 3;
            EventManager.player.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.06393222f, -0.3617879f);
            EventManager.player.GetComponent<CapsuleCollider2D>().size = new Vector2(0.4365608f, 0.4818728f);
            SoundManager.Instance.StopMusic(SoundManager.Instance.deathMusic);
            SoundManager.Instance.PlayMusic(SoundManager.Instance.gameMusic);
            MouseClick();
        }
    }

    void MouseClick()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {          
            SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonPress);
            SceneManager.LoadScene("PlayScene");
            
        }
    }
}