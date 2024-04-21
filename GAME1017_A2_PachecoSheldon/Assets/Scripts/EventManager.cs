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
    float timer = 1f;
    public static float invulnerableTimer = 0f;
    Color invulnerableColor;
    public static float invulnerableTimerStart = 0f;
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
        if (scene.name == "PlayScene" && sceneLoadCount >= 1)
        {
            EventManager.playerHealth = 3;
            EventManager.player.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.06393222f, -0.3617879f);
            EventManager.player.GetComponent<CapsuleCollider2D>().size = new Vector2(0.4365608f, 0.4818728f);

        }
    }

    void Update()
    {
         
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            invulnerableTimerText.text = "Invulnerable Timer: " + invulnerableTimer;
            if (invulnerableTimer > 0f)
            {
                invulnerableColor = player.GetComponent<SpriteRenderer>().color;
                invulnerableColor.a = 0.5f;
                player.GetComponent<SpriteRenderer>().color = invulnerableColor;    

                invulnerableTimer -= Time.deltaTime;
                invulnerableTimerText.gameObject.SetActive(true);
            }
            if (invulnerableTimer <= 0f)
            {
                invulnerableColor = player.GetComponent<SpriteRenderer>().color;
                invulnerableColor.a = 1f;
                player.GetComponent<SpriteRenderer>().color = invulnerableColor;

                invulnerableTimer = 0f;
                invulnerableTimerText.gameObject.SetActive(false);
            }
            if (invulnerableTimer <= 0f)
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
                    if (invulnerableTimerStart == 0f)
                    {
                        invulnerableTimer = 10f;
                        invulnerableTimerStart = 3.0f;
                    }
                }
                if (playerHealth == 1)
                {
                    playerLive1.enabled = true;
                    playerLive2.enabled = false;
                    playerLive3.enabled = false;
                    if (invulnerableTimerStart == 3f)
                    {
                        invulnerableTimer = 10f;
                        invulnerableTimerStart = 0.0f;
                    }
                }
                if (playerHealth == 0)
                {
                    playerLive1.enabled = false;
                    playerLive2.enabled = false;
                    playerLive3.enabled = false;
                    player.GetComponent<Animator>().SetBool("Death", true);
                    player.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.06393222f, 0.03418268f);
                    player.GetComponent<CapsuleCollider2D>().size = new Vector2(0.4365608f, 0.4818729f);
                }
            }
            if (playerHealth <= 0)
            {
                invulnerableTimer = 0f;
                sceneLoadCount = 0;
                playerHealth = 0;
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    SceneManager.LoadScene("GameoverScene");
                    timer = 1f;
                }

            }
        }

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}