using System.Collections;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    public float projectileSpeed = 5f;
    public float maxDistance = 4.0f; //max distance of snowball
    private float initialPositionX;

    public GameObject snowballExplode;
    public GameObject playerExplode;
    void Start()
    {
        
        initialPositionX = transform.position.x;//records snowball positions
    }
    void FixedUpdate()
    {

        //moves snowballs to the left from snowman position 
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-projectileSpeed, rb.velocity.y);

        
        if (Mathf.Abs(transform.position.x - initialPositionX) >= maxDistance)//checks if snowball reached the max distance then destroys
        {
           
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D snowballHit)
    {
        if (snowballHit.CompareTag("PlayerSnowball"))// destroy the enemy projectile on collision with player projectile
        {
            GameObject snowballExplosion = Instantiate(snowballExplode, transform.position, Quaternion.identity);
            snowballExplosion.SetActive(true);
            Destroy(snowballExplosion, 0.5f);

            Destroy(gameObject);
            
            Destroy(snowballHit.gameObject);
        }
        if (snowballHit.CompareTag("Player")) //check if player gets a collision with a snowball
        {
            EventManager.playerHealth--;
            if (EventManager.playerHealth ==0) //player dies in three hits, work in progress
            {

                Debug.Log("game over");
                
                Destroy(snowballHit.gameObject);//player dies
            }

            // destroys the snowball on player hit
            Destroy(gameObject);
            GameObject snowballExplosion = Instantiate(snowballExplode, transform.position, Quaternion.identity);
            snowballExplosion.SetActive(true);
            Destroy(snowballExplosion, 0.5f);

            //explodes player
            GameObject playerExplosion = Instantiate(playerExplode, snowballHit.transform.position, Quaternion.identity);
            playerExplosion.SetActive(true);
            Destroy(playerExplosion, 0.5f);

        }

    }
}
