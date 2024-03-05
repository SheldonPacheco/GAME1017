using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    
    int index = 0;
  

    void Update()
    {
        
        if (transform.position.x < -8.5f)
        {
            transform.position = new Vector2(8.5f, transform.position.y);
        }
        else if (transform.position.x > 8.5f)
        {
            transform.position = new Vector2(-8.5f, transform.position.y);
        }
        if (transform.position.y < -6.5f)
        {
            transform.position = new Vector2(transform.position.x, 6.5f);
        }
        else if (transform.position.y > 6.5f)
        {
            transform.position = new Vector2(transform.position.x, -6.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            List<GameObject> asteroids = AsteroidManager.Instance.GetAsteroids();
            index = asteroids.IndexOf(collision.gameObject);
            
            AsteroidManager.Instance.DeleteAsteroid(index);
            
            Destroy(gameObject);
            
        }
    }
}