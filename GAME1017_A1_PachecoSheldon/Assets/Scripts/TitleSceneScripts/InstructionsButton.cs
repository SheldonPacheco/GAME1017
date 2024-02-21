using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsButton : MonoBehaviour
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
        if (MainManagerLoad.currentInstructionsPanel != null)
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
        mainManagerLoad.InstructionsButtonHoverEnter();
    }
    void OnMouseExit()
    {
        mainManagerLoad.OnHoverExit();
    }
    void OnMouseDown()
    {
        if (SoundManager.currentSettingsPanel == null)
        {
            mainManagerLoad.InstructionsButtonClick();
        }
    }
}
