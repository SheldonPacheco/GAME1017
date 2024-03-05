using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class AsteroidManager : MonoBehaviour
{
    int index = 1;  
    public static AsteroidManager Instance { get; private set; } 

    [SerializeField] GameObject asteroidParent;
    [SerializeField] int asteroidCount;
    private List<GameObject> asteroids;
    public Vector3 position;
    public int phase = 0;    
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Initialize()
    {
        asteroids = new List<GameObject>();
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        float offSet = 12f / (asteroidCount / 2);
        for (int i = 0; i < asteroidCount / 2; i++)
        {
            GameObject roidInst = GameObject.Instantiate(asteroidParent, new Vector3(-6f + i * offSet + offSet / 2, 4f, 0f), Quaternion.identity);
            asteroids.Add(roidInst);
            roidInst = GameObject.Instantiate(asteroidParent, new Vector3(-6f + i * offSet + offSet / 2, -4f, 0f), Quaternion.identity);
            asteroids.Add(roidInst);
            roidInst.GetComponent<AsteroidScript>().InitializeAsteroid(roidInst.GetComponent<AsteroidScript>().AsteroidData);
        }
    }

    public List<GameObject> GetAsteroids()
    {
        return asteroids;
    }

    public void DeleteAsteroid(int index)
    {
        
        GameObject asteroid = asteroids[index];
        
        position = asteroid.transform.position;
        if(asteroid.GetComponent<AsteroidScript>().currentPhase == 0)
        {
            SplitAsteroid(asteroid, position);
        }
        else
        {

        }
        
        asteroids.RemoveAt(index);
        
        Destroy(asteroid);
        
        if (Game.Instance != null)
            Game.Instance.SOMA.PlaySound("Explode");

      
        
    }

    public void SplitAsteroid(GameObject asteroid, Vector3 position)
    {
        AsteroidScript asteroidScript = asteroid.GetComponent<AsteroidScript>();
        asteroid = asteroids[index];
        phase++;
        if (asteroidScript != null && asteroidScript.CurrentPhase == 0)
        {

            
            GameObject leftChunk = Instantiate(asteroidParent, position, Quaternion.identity);
            leftChunk.transform.localScale = asteroidParent.transform.localScale * 0.5f;
            leftChunk.GetComponent<AsteroidScript>().InitializeAsteroid(asteroidScript.AsteroidData);
            leftChunk.GetComponent<AsteroidScript>().Direction = Quaternion.Euler(0, 0, UnityEngine.Random.Range(30, 60)) * asteroidScript.Direction;
            leftChunk.GetComponent<AsteroidScript>().CurrentPhase = 3;
            asteroidScript.SetAsteroidTintColor(leftChunk, asteroidScript.Tint);

            GameObject rightChunk = Instantiate(asteroidParent, position, Quaternion.identity);
            rightChunk.transform.localScale = asteroidParent.transform.localScale * 0.5f;
            rightChunk.GetComponent<AsteroidScript>().InitializeAsteroid(asteroidScript.AsteroidData);
            rightChunk.GetComponent<AsteroidScript>().Direction = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-60, -30)) * asteroidScript.Direction;
            rightChunk.GetComponent<AsteroidScript>().CurrentPhase = 3;
            asteroidScript.SetAsteroidTintColor(rightChunk, asteroidScript.Tint);

            asteroids.Add(leftChunk);
            asteroids.Add(rightChunk);
        }
    }
}