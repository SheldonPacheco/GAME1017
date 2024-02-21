using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public MainManagerLoad mainManagerLoad;
    private void Update()

    {
        if (SoundManager.currentSettingsPanel != null)
        {

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {

            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        if (MainManagerLoad.currentInstructionsPanel!=null) 
        {

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        }
        else
        {

            gameObject.layer = LayerMask.NameToLayer("Default");

        }

    }
    void OnMouseEnter()
    {
        mainManagerLoad.PlayButtonHoverEnter();
    }
    void OnMouseExit()
    {
        mainManagerLoad.OnHoverExit();
    }
    void OnMouseDown()
    {
        if (SoundManager.currentSettingsPanel == null&& MainManagerLoad.currentInstructionsPanel == null)
        {
            SceneManager.LoadScene("PlayScene");
        }
        
    }
}
