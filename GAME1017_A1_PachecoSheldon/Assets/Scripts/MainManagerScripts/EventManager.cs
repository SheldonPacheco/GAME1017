using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
[Serializable]
public class PlayerData
{
    public int playerHighScore;
}

public class EventManager : MonoBehaviour
{
    private static GameObject uiContainer; 
    private static TMP_Text playerScoreUI;
    private static TMP_Text livesUI;
    private static TMP_Text highScoreUI;
    private static TMP_Text powerfulSnowballTimer;
    public static SpriteRenderer playerLive1;
    public static SpriteRenderer playerLive2;
    public static SpriteRenderer playerLive3;
    public static SpriteRenderer playerLive4;
    public static SpriteRenderer playerLive5;
    public static SpriteRenderer playerLive6;
    public static SpriteRenderer playerLive7;
    public static SpriteRenderer playerLive8;
    public static GameObject player;
    public static int playerScore = 0;
    public static int playerHighScore = 0;
    public static int playerHealth = 4;
    private static int sceneLoadCount = 0;
    private const string saveFileName = "playerData.xml";
    public static EventManager Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayScene" && sceneLoadCount <= 1)
        {
            player = GameObject.Find("Player");
            uiContainer = GameObject.Find("UI");
            playerScoreUI = uiContainer.transform.Find("ScoreText").GetComponent<TMP_Text>();
            livesUI = uiContainer.transform.Find("LivesText").GetComponent<TMP_Text>();
            highScoreUI = uiContainer.transform.Find("HighScoreText").GetComponent<TMP_Text>();
            powerfulSnowballTimer = uiContainer.transform.Find("PowerfulSnowballTimer").GetComponent<TMP_Text>();
            playerLive1 = uiContainer.transform.Find("Life1").GetComponent<SpriteRenderer>();
            playerLive2 = uiContainer.transform.Find("Life2").GetComponent<SpriteRenderer>();
            playerLive3 = uiContainer.transform.Find("Life3").GetComponent<SpriteRenderer>();
            playerLive4 = uiContainer.transform.Find("Life4").GetComponent<SpriteRenderer>();
            playerLive5 = uiContainer.transform.Find("Life5").GetComponent<SpriteRenderer>();
            playerLive6 = uiContainer.transform.Find("Life6").GetComponent<SpriteRenderer>();
            playerLive7 = uiContainer.transform.Find("Life7").GetComponent<SpriteRenderer>();
            playerLive8 = uiContainer.transform.Find("Life8").GetComponent<SpriteRenderer>();
            highScoreUI.text = "Highscore: " + playerHighScore;

            sceneLoadCount++;
            LoadPlayerData();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            if (playerHealth == 8)
            {
                playerLive1.enabled = true;
                playerLive2.enabled = true;
                playerLive3.enabled = true;
                playerLive4.enabled = true;
                playerLive5.enabled = true;
                playerLive6.enabled = true;
                playerLive7.enabled = true;
                playerLive8.enabled = true;
            }
            if (playerHealth == 7)
            {
                playerLive1.enabled = true;
                playerLive2.enabled = true;
                playerLive3.enabled = true;
                playerLive4.enabled = true;
                playerLive5.enabled = true;
                playerLive6.enabled = true;
                playerLive7.enabled = true;
                playerLive8.enabled = false;
            }
            if (playerHealth == 6)
            {
                playerLive1.enabled = true;
                playerLive2.enabled = true;
                playerLive3.enabled = true;
                playerLive4.enabled = true;
                playerLive5.enabled = true;
                playerLive6.enabled = true;
                playerLive7.enabled = false;
                playerLive8.enabled = false;
            }
            if (playerHealth == 5)
            {
                playerLive1.enabled = true;
                playerLive2.enabled = true;
                playerLive3.enabled = true;
                playerLive4.enabled = true;
                playerLive5.enabled = true;
                playerLive6.enabled = false;
                playerLive7.enabled = false;
                playerLive8.enabled = false;
            }
            if (playerHealth == 4)
            {
                playerLive1.enabled = true;
                playerLive2.enabled = true;
                playerLive3.enabled = true;
                playerLive4.enabled = true;
                playerLive5.enabled = false;
                playerLive6.enabled = false;
                playerLive7.enabled = false;
                playerLive8.enabled = false;
            }
            if (playerHealth == 3)
            {
                playerLive1.enabled = true;
                playerLive2.enabled = true;
                playerLive3.enabled = true;
                playerLive4.enabled = false;
                playerLive5.enabled = false;
                playerLive6.enabled = false;
                playerLive7.enabled = false;
                playerLive8.enabled = false;
            }
            if (playerHealth == 2)
            {
                playerLive1.enabled = true;
                playerLive2.enabled = true;
                playerLive3.enabled = false;
                playerLive4.enabled = false;
                playerLive5.enabled = false;
                playerLive6.enabled = false;
                playerLive7.enabled = false;
                playerLive8.enabled = false;
            }
            if (playerHealth == 1)
            {
                playerLive1.enabled = true;
                playerLive2.enabled = false;
                playerLive3.enabled = false;
                playerLive4.enabled = false;
                playerLive5.enabled = false;
                playerLive6.enabled = false;
                playerLive7.enabled = false;
                playerLive8.enabled = false;
            }
            if (playerHealth == 0)
            {
                playerLive1.enabled = false;
                playerLive2.enabled = false;
                playerLive3.enabled = false;
                playerLive4.enabled = false;
                playerLive5.enabled = false;
                playerLive6.enabled = false;
                playerLive7.enabled = false;
                playerLive8.enabled = false;
                sceneLoadCount = 0;
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                SceneManager.LoadScene("FailedScene");
            }

            playerScoreUI.text = "Score: " + playerScore;
            if (playerScore > playerHighScore)
            {
                playerHighScore = playerScore;
                highScoreUI.text = "Highscore: " + playerHighScore;
                SavePlayerData();
            }
            if(PlayerMovement.powerfulSnowballTimer > 0)
            {
                powerfulSnowballTimer.text = "Powerful Snowball Timer: " + PlayerMovement.powerfulSnowballTimer.ToString("F2");
                
            } else if (PlayerMovement.powerfulSnowballTimer <= 0)
            {
                powerfulSnowballTimer.text = "";
            }
        }

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void SavePlayerData()
    {
        PlayerData playerData = new PlayerData
        {
            playerHighScore = playerHighScore
        };

        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, saveFileName)))
        {
            serializer.Serialize(streamWriter, playerData);
        }
    }

    private void LoadPlayerData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                PlayerData playerData = (PlayerData)serializer.Deserialize(streamReader);
                playerHighScore = playerData.playerHighScore;
            }
        }
    }
}