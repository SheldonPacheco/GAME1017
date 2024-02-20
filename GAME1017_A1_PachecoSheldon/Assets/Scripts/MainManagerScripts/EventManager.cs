using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    [SerializeField] TMP_Text playerScoreUI;
    [SerializeField] TMP_Text LivesUI;
    [SerializeField] SpriteRenderer playerLive1;
    [SerializeField] SpriteRenderer playerLive2;
    [SerializeField] SpriteRenderer playerLive3;
    [SerializeField] SpriteRenderer playerLive4;
    public GameObject player;
    public static int playerScore = 0;
    public static int playerHealth = 4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 3)
            playerLive4.enabled = false;
        if (playerHealth == 2)
            playerLive3.enabled = false;
        if (playerHealth == 1)
            playerLive2.enabled = false;
        if (playerHealth == 0)
        {
            
            playerLive1.enabled = false;

            SceneManager.LoadScene("FailedScreen");
            
            
        }
        playerScoreUI.text = "Score: " + playerScore;
    }
}
