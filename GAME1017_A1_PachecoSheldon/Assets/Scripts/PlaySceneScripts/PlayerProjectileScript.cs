using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerProjectileScript : MonoBehaviour
{
    public static SnowmanSpawner snowmanSpawner;

    public GameObject snowballExplode;
    public GameObject snowmanExplode;

    public GameObject powerfulSnowball;
    public Sprite PowerfulSnowballSprite;

    public SpriteRenderer spriteRenderer;
    public Sprite snowballSprite;

    public static int chanceToDropPowerfulSnowball;
    public static int dropPowerfulSnowball = 5;
    public GameObject PowerfulSnowballPickup;

    public static int chanceToDropLife;
    public static int dropLife = 5;
    public GameObject LifePickup;

    public void Start()
    {
        snowmanSpawner = FindObjectOfType<SnowmanSpawner>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }
    public void Update()
    {
        if (PlayerMovement.powerfulSnowballTimer > 0) 
        {

            spriteRenderer.sprite = PowerfulSnowballSprite;

        } else if (PlayerMovement.powerfulSnowballTimer <= 0)
        {

            spriteRenderer.sprite = snowballSprite;

        }  
        
    }
    void OnTriggerEnter2D(Collider2D snowballHit)
    {
        
        if (snowballHit.CompareTag("EnemySnowball"))// destroy the player projectile on collision with enemy projectile
        {
            if (PlayerMovement.powerfulSnowballTimer <= 0)
            {
                //explodes snowball
                GameObject snowballExplosion = Instantiate(snowballExplode, transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                Destroy(snowballExplosion, 0.5f);

                //20pts for enemy snowball
                EventManager.playerScore += 20;
                Destroy(gameObject);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                Destroy(snowballHit.gameObject);
            } 

        }
        if (snowballHit.CompareTag("Enemy"))
        {
            if (PlayerMovement.powerfulSnowballTimer <= 0)
            {
                //50pts for enemy
                EventManager.playerScore += 50;

                //explodes snowball
                GameObject snowballExplosion = Instantiate(snowballExplode, transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                Destroy(snowballExplosion, 0.5f);

                //explodes snowman
                GameObject snowmanExplosion = Instantiate(snowmanExplode, snowballHit.transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                Destroy(snowmanExplosion, 0.5f);
            }         


            chanceToDropPowerfulSnowball = Random.Range(1, 10);
            if (dropPowerfulSnowball == chanceToDropPowerfulSnowball)
            {
                Instantiate(PowerfulSnowballPickup, snowballHit.transform.position, Quaternion.identity);
            }

            if (PlayerMovement.powerfulSnowballTimer > 0)
            {
                SpawnSnowballStar(snowballHit.transform.position);
            }


            chanceToDropLife = Random.Range(1, 20);
            if (dropLife == chanceToDropLife)
            {
                Instantiate(LifePickup, snowballHit.transform.position, Quaternion.identity);
            }

            snowmanSpawner.MoveToPool(snowballHit.gameObject);

            Destroy(gameObject);

        }

    }
    void SpawnSnowballStar(Vector3 centerPosition)
    {
        int numberOfSnowballs = 8;
        float angleStep = 360f / numberOfSnowballs;

        for (int i = 0; i < numberOfSnowballs; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            Vector3 spawnPosition = centerPosition + new Vector3(direction.x, direction.y, 0f);

            GameObject snowball = Instantiate(powerfulSnowball, spawnPosition, Quaternion.identity);
            Rigidbody2D snowballSpeed = snowball.GetComponent<Rigidbody2D>();

            snowballSpeed.velocity = direction * 10.0f; 

            Destroy(snowball, 3.0f);
        }
    }
}


