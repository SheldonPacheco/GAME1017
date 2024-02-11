using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._MyAssets._Scripts
{
    public class BulletScript : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);

                
                int index = EnemySpawner.Instance.GetEnemies().IndexOf(collision.gameObject);

                
                if (index != -1)
                {
                    
                    EnemySpawner.Instance.DeleteEnemy(index);
                }
            }
        }
    }
}