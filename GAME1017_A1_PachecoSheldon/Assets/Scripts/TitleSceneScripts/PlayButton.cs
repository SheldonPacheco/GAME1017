using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public MainManagerLoad mainManagerLoad;

    void OnMouseEnter()
    {
        mainManagerLoad.PlayButtonHoverEnter();
    }
    void OnMouseExit()
    {
        mainManagerLoad.OnHoverExit();
    }
}
