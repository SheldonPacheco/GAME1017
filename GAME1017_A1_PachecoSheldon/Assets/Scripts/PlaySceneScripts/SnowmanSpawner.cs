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
    void Start()
    {
        
        InvokeRepeating("SpawnSnowman", 0f, spawnInterval);//spawns snowman
       
    }

    void SpawnSnowman()
    {
        
        if (activeSnowmen.Count < maxSnowmen)//makes sure only the set amount gets spawned at a time
        {
            
            GameObject snowman = GetSnowmanFromPool();//spawns a new snowman from the pool if available

            //if the pool is empty, instantiate a new snowman
            if (snowman == null)
            {
                
                snowman = Instantiate(enemySnowman, GetRandomSpawnPosition(), Quaternion.identity);
                snowman.SetActive(true);
            }

            
            snowman.SetActive(true);

            
            Rigidbody2D snowmanRb = snowman.GetComponent<Rigidbody2D>();// reset the snowman's velocity, so they dont spawn at crazy speeds
            if (snowmanRb != null)
            {
                snowmanRb.velocity = Vector2.left * maxSpeed;
            }

            
            activeSnowmen.Add(snowman);//adds the snowman to the list of active snowmen

        }
    }

    GameObject GetSnowmanFromPool()
    {
        
        if (poolOfSnowmen.Count > 0)//checks if there are snowmen available in the pool and gets a snowman from pool
        {
            
            GameObject snowman = poolOfSnowmen[0];
            poolOfSnowmen.RemoveAt(0);
            return snowman;
        }

        return null;
    }

    public void MoveToPool(GameObject snowman)
    {
        
        if (activeSnowmen.Contains(snowman))//checks if the snowman is in the active list before moving to the pool and removes from list
        {
            
            activeSnowmen.Remove(snowman);

            
            if (activeSnowmen.Count + poolOfSnowmen.Count < maxSnowmen)//checks to make sure lists dont get overloaded
            {
                
                snowman.transform.position = GetRandomSpawnPosition();//reset snowman position before moving it to the pool

                
                Rigidbody2D snowmanRb = snowman.GetComponent<Rigidbody2D>();//reset snowman velocity so it doesnt respawn at crazy speeds
                if (snowmanRb != null)
                {
                    snowmanRb.velocity = Vector2.left * maxSpeed;
                }

                
                snowman.SetActive(false); //deactives the snowman and adds it to the pool for later respawn
                poolOfSnowmen.Add(snowman);
            }
            
        }
        else
        {
            if (poolOfSnowmen.Count < maxSnowmen) //handles snowman going off screen
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