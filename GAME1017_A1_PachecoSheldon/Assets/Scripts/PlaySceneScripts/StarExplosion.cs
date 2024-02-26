using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarExplosion : MonoBehaviour
{
    public static SnowmanSpawner snowmanSpawner;
    public static TreeSpawner treeSpawner;
    public PlayerProjectileScript playerProjectileScript;

    public GameObject powerfulSnowballExplode;
    public GameObject powerfulSnowmanExplode;
    public GameObject powerfulTreeExplode;
    

    private void Start()
    {
        treeSpawner = FindObjectOfType<TreeSpawner>();
          
        snowmanSpawner = FindObjectOfType<SnowmanSpawner>();
    }
    void OnTriggerEnter2D(Collider2D snowballHit)
    {

        if (snowballHit.CompareTag("EnemySnowball"))// destroy the player projectile on collision with enemy projectile
        {
            if (PlayerMovement.powerfulSnowballTimer > 0)
            {
                //explodes snowball
                GameObject powerfulSnowballExplosion = Instantiate(powerfulSnowballExplode, transform.position, Quaternion.identity);

                Destroy(powerfulSnowballExplosion, 0.5f);

                //20pts for enemy snowball
                EventManager.playerScore += 20;
                Destroy(gameObject);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                Destroy(snowballHit.gameObject);
            }

        }
        if (snowballHit.CompareTag("Enemy"))
        {
            if (PlayerMovement.powerfulSnowballTimer > 0)
            {
                //50pts for enemy
                EventManager.playerScore += 50;

                //explodes snowball
                GameObject powerfulSnowballExplosion = Instantiate(powerfulSnowballExplode, transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                Destroy(powerfulSnowballExplosion, 0.5f);

                //explodes snowman
                GameObject powerfulSnowmanExplosion = Instantiate(powerfulSnowmanExplode, snowballHit.transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                Destroy(powerfulSnowmanExplosion, 0.5f);
            }

            PlayerProjectileScript.chanceToDropPowerfulSnowball = Random.Range(1, 10);
            if (PlayerProjectileScript.dropPowerfulSnowball == PlayerProjectileScript.chanceToDropPowerfulSnowball)
            {
                Instantiate(playerProjectileScript.PowerfulSnowballPickup, snowballHit.transform.position, Quaternion.identity);
            }


            PlayerProjectileScript.chanceToDropLife = Random.Range(1, 20);
            if (PlayerProjectileScript.dropLife == PlayerProjectileScript.chanceToDropLife)
            {
                Instantiate(playerProjectileScript.LifePickup, snowballHit.transform.position, Quaternion.identity);
            }

            snowmanSpawner.MoveToPool(snowballHit.gameObject);

            Destroy(gameObject);

        }
        if (snowballHit.CompareTag("ChristmasTree"))
        {
            if (PlayerMovement.powerfulSnowballTimer > 0)
            {
                //100pts for tree
                EventManager.playerScore += 100;

                //explodes snowball
                GameObject powerfulSnowballExplosion = Instantiate(powerfulSnowballExplode, transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                Destroy(powerfulSnowballExplosion, 0.5f);

                //explodes snowman
                GameObject powerfulTreeExplosion = Instantiate(powerfulTreeExplode, snowballHit.transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.snowballHitSound);
                Destroy(powerfulTreeExplosion, 0.5f);
            }

            PlayerProjectileScript.chanceToDropPowerfulSnowball = Random.Range(1, 10);
            if (PlayerProjectileScript.dropPowerfulSnowball == PlayerProjectileScript.chanceToDropPowerfulSnowball)
            {
                Instantiate(playerProjectileScript.PowerfulSnowballPickup, snowballHit.transform.position, Quaternion.identity);
            }


            PlayerProjectileScript.chanceToDropLife = Random.Range(1, 20);
            if (PlayerProjectileScript.dropLife == PlayerProjectileScript.chanceToDropLife)
            {
                Instantiate(playerProjectileScript.LifePickup, snowballHit.transform.position, Quaternion.identity);
            }

            treeSpawner.MoveToPool(snowballHit.gameObject);
            
            Destroy(gameObject);

        }
    }
}
