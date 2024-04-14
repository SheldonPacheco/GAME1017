using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Make this whole script. :)
public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab = new GameObject[3];
    public List<GameObject> obstacles = new List<GameObject>();
    public float moveSpeed = -4.0f;
    int ChosenObstacle= 0;
    private float timer = 0f; // Used to manage the gaps between obstacles.
    void Start()
    {
        InvokeRepeating("MoveObstacles", 0f, Time.fixedDeltaTime);
    }
    private void MoveObstacles()
    {
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.transform.Translate(moveSpeed * Time.fixedDeltaTime, 0f, 0f);
        }
        if (obstacles.Count != 0)
        {
            if (obstacles[0].transform.position.x <= -10f)
            {
                // Remove the first obstacle.
                Destroy(obstacles[0]);
                obstacles.RemoveAt(0);
                // Push a new obstacle at the end.

                GameObject obsInst = GameObject.Instantiate(obstaclePrefab[ChosenObstacle = Random.Range(0, obstaclePrefab.Length)], new Vector3(39.8f, obstaclePrefab[ChosenObstacle].transform.position.y, 0f), Quaternion.identity);

                obsInst.transform.parent = transform;
                obstacles.Add(obsInst);
            }
        }

    }
    public void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (timer <= 0f)
            {
                GameObject obsInst = GameObject.Instantiate(obstaclePrefab[ChosenObstacle=Random.Range(0, obstaclePrefab.Length)], new Vector3(39.8f, obstaclePrefab[ChosenObstacle].transform.position.y, 0f), Quaternion.identity);

                obsInst.transform.parent = transform;
                obstacles.Add(obsInst);
                timer = 3.0f;
            }
        }
        if (obstacles.Count < 3)
        {
            timer -= Time.deltaTime;
        }
    }
}
