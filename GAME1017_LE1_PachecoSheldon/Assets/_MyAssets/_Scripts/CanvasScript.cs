using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    void Start()
    {
       
        SoundManager.Instance.AddSound("boom", Resources.Load<AudioClip>("boom"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("death", Resources.Load<AudioClip>("death"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("jump", Resources.Load<AudioClip>("jump"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("laser", Resources.Load<AudioClip>("laser"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("mask", Resources.Load<AudioClip>("MASK"), SoundManager.SoundType.SOUND_MUSIC);
        SoundManager.Instance.AddSound("tcats", Resources.Load<AudioClip>("Thundercats"), SoundManager.SoundType.SOUND_MUSIC);
        SoundManager.Instance.AddSound("turtles", Resources.Load<AudioClip>("Turtles"), SoundManager.SoundType.SOUND_MUSIC);
    }
    public void PlaySFX(string soundKey)
    {
        SoundManager.Instance.PlaySound(soundKey);
    }

    public void PlayMusic(string soundKey)
    {
        SoundManager.Instance.PlayMusic(soundKey);
    }
}
