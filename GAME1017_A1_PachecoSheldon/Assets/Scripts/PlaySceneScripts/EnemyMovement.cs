using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float maxSpeed = 2f; //max speed for snowman movement
    public SnowmanSpawner snowmanSpawner;
    public float snowballCooldown = 2f; // Adjust the cooldown time as needed
    private float nextSnowballTime;
    void Start()
    {
        nextSnowballTime = Time.time + snowballCooldown; //initiates snowball cooldown
    }
    void Update()
    {
        MoveLeft();
        if (Time.time >= nextSnowballTime)//throws snowsballs at a fixed speed
        {
            ThrowSnowball();
            
            nextSnowballTime = Time.time + snowballCooldown;// rests the cooldown timer
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * maxSpeed);

        
        float minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x;// checks if the enemy is off-screen
        if (transform.position.x < minX) //if it is sends it back to pool and has it respawn into active
        {
            snowmanSpawner.MoveToPool(gameObject);
            transform.position = snowmanSpawner.GetRandomSpawnPosition();
        }
    }
    void ThrowSnowball() //initiates the snowball throwing to go with the snowmen moving
    {
        snowmanSpawner.SpawnSnowball(transform.position);
    }
}