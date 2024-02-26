using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject playerSprite;
    public GameObject snowballProjectile;
    private Animator animator;
    public TreeSpawner ChristmasTreeSpawner;
    public GameObject treeExplode;
    bool powerfulSnowball = false;
    static public float powerfulSnowballTimer = 0f;
    void Start()
    {
        animator = playerSprite.GetComponent<Animator>();  
    }


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //player movement work in progress
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;


        if (movement != Vector3.zero)
        {
            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);
            animator.SetBool("IsMoving", true);
            animator.Play("Walking");
            if (horizontalInput < 0)
                playerSprite.GetComponent<SpriteRenderer>().flipX = true;
            else if (horizontalInput > 0)
                playerSprite.GetComponent<SpriteRenderer>().flipX = false;


        }
        else if (movement.sqrMagnitude < 0.01f)
        {
            animator.Play("Idle");
            animator.SetBool("IsMoving", false);
        }


        if (Input.GetKeyDown(KeyCode.Space))//spawns snowball on sapce
        {
            if(powerfulSnowball==true)
            {
                
                ThrowSeekingSnowball();
            } else if (powerfulSnowball == false)
            {
                
                ThrowSnowball();
            }
            
        }
        //stops tree from sending player to moon
        Rigidbody2D player = GetComponent<Rigidbody2D>();
        player.velocity = Vector2.zero;

        if(powerfulSnowball==true)
        {
            powerfulSnowballTimer -= Time.deltaTime;
            Debug.Log("Powerful Snowball Timer: " + powerfulSnowballTimer);
            if (powerfulSnowballTimer <= 0f)
            {
                powerfulSnowball = false;
            }
        }
    }
    void ThrowSnowball()
    {
        GameObject snowball = Instantiate(snowballProjectile, transform.position, Quaternion.identity);
        Rigidbody2D snowballThrow = snowball.GetComponent<Rigidbody2D>();

       
        snowballThrow.velocity = new Vector2((playerSprite.GetComponent<SpriteRenderer>().flipX ? -1 : 1) * 10f, 0f);

        Destroy(snowball, 3.0f);
    }
    void ThrowSeekingSnowball()
    {
        GameObject snowball = Instantiate(snowballProjectile, transform.position, Quaternion.identity);
        Rigidbody2D snowballThrow = snowball.GetComponent<Rigidbody2D>();

        
        GameObject nearestSnowman = NearestSnowman();

        if (nearestSnowman != null)
        {
            
            Vector2 direction = (nearestSnowman.transform.position - transform.position).normalized;

            
            snowballThrow.velocity = direction * 10.0f;
        }
        else
        {
            
            snowballThrow.velocity = new Vector2((playerSprite.GetComponent<SpriteRenderer>().flipX ? -1 : 1) * 10.0f, 0f);
        }

        Destroy(snowball, 3.0f);
    }

    GameObject NearestSnowman()
    {
        GameObject nearestSnowman = null;
        float minDistance = float.MaxValue;

        foreach (var snowman in SnowmanSpawner.distancesToPlayer.Keys)
        {
            float distance = SnowmanSpawner.distancesToPlayer[snowman];

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestSnowman = snowman;
            }
        }

        return nearestSnowman;
    }

    void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("ChristmasTree"))
        {

            EventManager.playerScore += 100;
            EventManager.playerHealth--;

            //explodes tree
            GameObject treeExplosion = Instantiate(treeExplode, collison.transform.position, Quaternion.identity);
            treeExplosion.SetActive(true);
            Destroy(treeExplosion, 0.5f);

            ChristmasTreeSpawner.MoveToPool(collison.gameObject);
        }

        if (collison.CompareTag("PowerfulSnowball"))
        {
            powerfulSnowballTimer+=15f;
            powerfulSnowball = true;
            Destroy(collison.gameObject);

        }
        if (collison.CompareTag("LifePickup"))
        {

            Destroy(collison.gameObject);

        }
    }
}
