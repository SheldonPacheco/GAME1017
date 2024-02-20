using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerProjectileScript : MonoBehaviour
{
    public SnowmanSpawner snowmanSpawner;
    public GameObject snowballExplode;
    public GameObject snowmanExplode;

    public void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D snowballHit)
    {
        
        if (snowballHit.CompareTag("EnemySnowball"))// destroy the player projectile on collision with enemy projectile
        {
            //explodes snowball
            GameObject snowballExplosion = Instantiate(snowballExplode, transform.position, Quaternion.identity);
            snowballExplosion.SetActive(true);
            Destroy(snowballExplosion, 0.5f);

            //20pts for enemy snowball
            EventManager.playerScore += 20;
            Destroy(gameObject);

            Destroy(snowballHit.gameObject);
        }
        if (snowballHit.CompareTag("Enemy"))
        {
            //50pts for enemy
            EventManager.playerScore += 50;

            //explodes snowball
            GameObject snowballExplosion = Instantiate(snowballExplode, transform.position, Quaternion.identity);
            snowballExplosion.SetActive(true);
            Destroy(snowballExplosion, 0.5f);
            
            //explodes snowman
            GameObject snowmanExplosion = Instantiate(snowmanExplode, snowballHit.transform.position, Quaternion.identity);
            snowmanExplosion.SetActive(true);
            Destroy(snowmanExplosion, 0.5f);


            snowmanSpawner.MoveToPool(snowballHit.gameObject);
            Destroy(gameObject);

        }

    }
}


