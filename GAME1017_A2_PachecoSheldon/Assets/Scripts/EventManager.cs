using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventManager : MonoBehaviour
{
    private static GameObject uiContainer; 
    private static TMP_Text livesUI;
    private static TMP_Text invulnerableTimerText;
    public static SpriteRenderer playerLive1;
    public static SpriteRenderer playerLive2;
    public static SpriteRenderer playerLive3;
    public static GameObject player;
    public static int playerScore = 0;
    public static int playerHighScore = 0;
    public static int playerHealth = 4;
    private static int sceneLoadCount = 0;
    float timer = 3f;
    public static float invulnerableTimer = 0f;
    public static EventManager Instance { get; private set; }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayScene" && sceneLoadCount <= 1)
        {
            player = GameObject.Find("Player");
            uiContainer = GameObject.Find("UI");
            livesUI = uiContainer.transform.Find("LivesText").GetComponent<TMP_Text>();
            playerLive1 = uiContainer.transform.Find("Life1").GetComponent<SpriteRenderer>();
            playerLive2 = uiContainer.transform.Find("Life2").GetComponent<SpriteRenderer>();
            playerLive3 = uiContainer.transform.Find("Life3").GetComponent<SpriteRenderer>();
            invulnerableTimerText = uiContainer.transform.Find("InvulnerableTimer").GetComponent<TMP_Text>();

            sceneLoadCount++;

        }
    }

    void Update()
    {
        if ( invulnerableTimer > 0f)
        {
            invulnerableTimer -= Time.deltaTime;
            invulnerableTimerText.enabled = true;
        }
        if (invulnerableTimer <= 0f)
        {
            invulnerableTimerText.enabled = false;
        }
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            
            if (invulnerableTimer <= 0)
            {
                
                if (playerHealth == 3)
                {
                    playerLive1.enabled = true;
                    playerLive2.enabled = true;
                    playerLive3.enabled = true;
                }
                if (playerHealth == 2)
                {
                    playerLive1.enabled = true;
                    playerLive2.enabled = true;
                    playerLive3.enabled = false;
                    invulnerableTimer = 10f;
                    invulnerableTimerText.enabled = true;
                }
                if (playerHealth == 1)
                {
                    playerLive1.enabled = true;
                    playerLive2.enabled = false;
                    playerLive3.enabled = false;
                    invulnerableTimer = 10f;
                    invulnerableTimerText.enabled = true;
                }
                if (playerHealth == 0)
                {
                    playerLive1.enabled = false;
                    playerLive2.enabled = false;
                    playerLive3.enabled = false;
                    sceneLoadCount = 0;
                    player.GetComponent<Animator>().SetBool("Death", true);
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        SceneManager.LoadScene("GameoverScene");
                    }

                }
            }
            }

        }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}