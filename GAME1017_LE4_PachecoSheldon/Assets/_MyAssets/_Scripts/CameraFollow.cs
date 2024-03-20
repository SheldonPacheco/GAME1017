using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SoundManager;

public class CameraFollow : MonoBehaviour
{
    public float moveSpeed = 5f; // The speed at which the camera moves horizontally.

    public Vector3 restartCamera;
    public Vector3 playerStartPosition;

    public Vector3 restartCameraCheckpointOne;
    public Vector3 playerStartPositionCheckpointOne;


    public Vector3 restartCameraCheckpointTwo;
    public Vector3 playerStartPositionCheckpointTwo;


    public Vector3 restartCameraCheckpointThree;
    public Vector3 playerStartPositionCheckpointThree;

    public GameObject playerCurrent;
    public GameObject playerPrefab;
    public float playerDistance;
    public int checkpoints;
    public bool finishLine;
    public GameObject checkPointObject;
    public TMP_Text checkpointText;
    public int maxCheckpoints = 3;
    float timerText = 3;

    void Start()
    {
        checkpoints = 0;
        maxCheckpoints = 3;
        restartCamera = transform.position;

        playerCurrent = Instantiate(playerPrefab);
        if (playerCurrent != null)
        {
            playerCurrent = GameObject.FindWithTag("Player");
            playerStartPosition = playerCurrent.transform.position;
        }
            
    }

    void LateUpdate()
    {
        if (playerDistance >= 307.0f && finishLine == true)
        {
            transform.position = transform.position;

        }
        else if (finishLine==false)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }


    }
    private void Update()
    {
        
        if (playerCurrent != null)
        {
            playerCurrent = GameObject.FindWithTag("Player");

            //tracks distance for checkpoints
            playerDistance = Vector3.Distance(playerStartPosition, playerCurrent.transform.position);
           
        }

        


        if (checkpoints == 0)
        {
            //Respawns player and resets camera before checkpoints
            if (playerCurrent == null)
            {
                transform.position = restartCamera;
                playerCurrent = Instantiate(playerPrefab);
            }
        }
        

        if (playerDistance >= 60.0f && checkpoints == 0)
        {
            playerStartPositionCheckpointOne = playerCurrent.transform.position;
            restartCameraCheckpointOne = transform.position;


            checkPointObject.SetActive(true);
            checkpointText.text = "Checkpoint " + (++checkpoints) + "/" + (maxCheckpoints) + " reached!";

            

        }
        if (playerDistance >= 150.0f && checkpoints == 1)
        {
            playerStartPositionCheckpointTwo = playerCurrent.transform.position;
            restartCameraCheckpointTwo = transform.position;


            checkPointObject.SetActive(true);
            checkpointText.text = "Checkpoint " + (++checkpoints) + "/" + (maxCheckpoints) + " reached!";

           

        }
        if (playerDistance >= 240.0f && checkpoints == 2)
        {
            playerStartPositionCheckpointThree = playerCurrent.transform.position;
            restartCameraCheckpointThree = transform.position;


            checkPointObject.SetActive(true);
            checkpointText.text = "Checkpoint " + (++checkpoints) + "/" + (maxCheckpoints) + " reached!";

        }
        if (checkpoints == 1)
        {
            if (checkPointObject.activeSelf == (true)) 
            {
                timerText -= Time.deltaTime;
            }
            
            if (timerText <= 0.0f && checkPointObject.activeSelf==(true))
            {
                checkPointObject.SetActive(false);
                timerText = 3.0f;
            }

            if (playerCurrent == null)
            {
                transform.position = restartCameraCheckpointOne;
                playerCurrent = Instantiate(playerPrefab);
                playerCurrent.transform.position = playerStartPositionCheckpointOne;
            }
        }
        if (checkpoints == 2)
        {

            if (checkPointObject.activeSelf == (true))
            {
                timerText -= Time.deltaTime;
            }
            if (timerText <= 0.0f && checkPointObject.activeSelf == (true))
            {
                checkPointObject.SetActive(false);
                timerText = 3.0f;
            }

            if (playerCurrent == null)
            {
                transform.position = restartCameraCheckpointTwo;
                playerCurrent = Instantiate(playerPrefab);
                playerCurrent.transform.position = playerStartPositionCheckpointTwo;
            }
        }

        if (checkpoints == 3)
        {

            if (checkPointObject.activeSelf == (true))
            {
                timerText -= Time.deltaTime;
            }
            if (timerText <= 0.0f && checkPointObject.activeSelf == (true))
            {
                checkPointObject.SetActive(false);
                timerText = 3.0f;
            }

            if (playerCurrent == null)
            {
                transform.position = restartCameraCheckpointThree;
                playerCurrent = Instantiate(playerPrefab);
                playerCurrent.transform.position = playerStartPositionCheckpointThree;
            }
        }




        if (playerDistance >= 307.0f & checkpoints == 3)
        {
            finishLine = true;
            Game.Instance.SOMA.SetVolume(0.30f, SoundType.SOUND_SFX);
            Game.Instance.SOMA.PlaySound("Cheering");
            checkPointObject.SetActive(true);
            checkpointText.text = "Game completed! Congrats!";
            checkpoints++;
        }

        if (checkpoints == 4)
        {

            if (checkPointObject.activeSelf == (true))
            {
                timerText -= Time.deltaTime;
            }
            if (timerText <= 0.0f && checkPointObject.activeSelf == (true))
            {
                checkPointObject.SetActive(false);
                timerText = 3.0f;
            }
        }
    }
}