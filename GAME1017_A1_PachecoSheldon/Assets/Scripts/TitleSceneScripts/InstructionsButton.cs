using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsButton : MonoBehaviour
{
    public MainManagerLoad mainManagerLoad;

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
        mainManagerLoad.InstructionsButtonClick();
    }
}
