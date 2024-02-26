using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeSnowmen = new List<GameObject>();
    [SerializeField] private List<GameObject> poolOfSnowmen = new List<GameObject>();
    public GameObject enemySnowman;
    public GameObject enemySnowball;
    private float spawnInterval = 1f;
    public float maxSpeed = 5f;
    public float enemyProjectileSpeed = 5f;
    public int maxSnowmen = 3;
    public Transform playerTransform;
    public static Dictionary<GameObject, float> distancesToPlayer = new Dictionary<GameObject, float>();

    void Start()
    {
        InvokeRepeating("SpawnSnowman", 0f, spawnInterval);
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            List<GameObject> snowmenToRemove = new List<GameObject>();

            foreach (var snowman in activeSnowmen)
            {
                if (snowman == null)
                {
                    // The snowman has been destroyed, remove it from the distancesToPlayer dictionary
                    snowmenToRemove.Add(snowman);
                    continue;
                }

                float distanceToPlayer = Vector2.Distance(playerTransform.position, snowman.transform.position);

                if (distancesToPlayer.ContainsKey(snowman))
                {
                    // If the snowman is already in the dictionary, update its distance
                    distancesToPlayer[snowman] = distanceToPlayer;
                }
                else
                {
                    // If the snowman is not in the dictionary, add it
                    distancesToPlayer.Add(snowman, distanceToPlayer);
                }
            }

            // Remove the destroyed snowmen from the distancesToPlayer dictionary
            foreach (var snowmanToRemove in snowmenToRemove)
            {
                distancesToPlayer.Remove(snowmanToRemove);
            }
        }
    }

    void SpawnSnowman()
    {
        if (activeSnowmen.Count < maxSnowmen)
        {
            GameObject snowman = GetSnowmanFromPool();

            if (snowman == null)
            {
                snowman = Instantiate(enemySnowman, GetRandomSpawnPosition(), Quaternion.identity);
                snowman.SetActive(true);
            }

            snowman.SetActive(true);

            Rigidbody2D snowmanRb = snowman.GetComponent<Rigidbody2D>();
            if (snowmanRb != null)
            {
            }

            activeSnowmen.Add(snowman);          
            distancesToPlayer.Add(snowman, 0f);
            
        }
    }

    GameObject GetSnowmanFromPool()
    {
        if (poolOfSnowmen.Count > 0)
        {
            GameObject snowman = poolOfSnowmen[0];
            poolOfSnowmen.RemoveAt(0);
            return snowman;
        }

        return null;
    }

    public void MoveToPool(GameObject snowman)
    {
        if (activeSnowmen.Contains(snowman))
        {
            activeSnowmen.Remove(snowman);

            if (activeSnowmen.Count + poolOfSnowmen.Count < maxSnowmen)
            {
                snowman.transform.position = GetRandomSpawnPosition();
                Rigidbody2D snowmanRb = snowman.GetComponent<Rigidbody2D>();
                if (snowmanRb != null)
                {
                    snowmanRb.velocity = Vector2.left * maxSpeed;
                }

                snowman.SetActive(false);
                poolOfSnowmen.Add(snowman);

                // Remove the corresponding distance from the dictionary
                distancesToPlayer.Remove(snowman);
            }
        }
        else
        {
            if (poolOfSnowmen.Count < maxSnowmen)
            {
                snowman.SetActive(false);
                poolOfSnowmen.Add(snowman);
            }
        }
    }

    public void RespawnSnowman(GameObject snowmanToRespawn)
    {
        if (snowmanToRespawn != null)
        {
           
            snowmanToRespawn.transform.position = GetRandomSpawnPosition(); //resets the snowman position


            Rigidbody2D snowmanRb = snowmanToRespawn.GetComponent<Rigidbody2D>();//reset snowman velocity so it doesnt respawn at crazy speeds
            if (snowmanRb != null)
            {
                snowmanRb.velocity = Vector2.left * maxSpeed;
            }

            
            snowmanToRespawn.SetActive(true);

           
            activeSnowmen.Add(snowmanToRespawn);//respawns snowman and takes from pool if there

            
            if (poolOfSnowmen.Contains(snowmanToRespawn))
            {
                poolOfSnowmen.Remove(snowmanToRespawn);
            }
        }
    }

    public Vector3 GetRandomSpawnPosition() //randomize spawn on y set off screen for x
    {
        float randomY = Random.Range(-3.83f, 4.15f);
        float spawnX = 7.68f;
        return new Vector3(spawnX, randomY, 0f);
    }
    public void SpawnSnowball(Vector3 spawnPosition)
    {
        
        GameObject snowball = Instantiate(enemySnowball, spawnPosition, Quaternion.identity);//spawns enemy snowball

        
        snowball.SetActive(true);

        
        EnemyProjectileScript snowballScript = snowball.GetComponent<EnemyProjectileScript>();//gets speed variable from enemy projectile script
        if (snowballScript != null)
        {
            snowballScript.projectileSpeed = enemyProjectileSpeed;
        }

    }
}