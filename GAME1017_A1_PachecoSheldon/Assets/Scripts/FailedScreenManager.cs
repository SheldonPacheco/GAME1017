using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class FailedScreenManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic.volume = 0.15f;
        backgroundMusic.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
  
        
    }
    public void RestartGame()
    {
        backgroundMusic.Stop();
        backgroundMusic.time = 0;
        EventManager.playerHealth = 4;
        EventManager.playerScore = 0;
        SceneManager.LoadScene("SampleScene");
    }
}
