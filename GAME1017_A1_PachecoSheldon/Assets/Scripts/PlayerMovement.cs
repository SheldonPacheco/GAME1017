using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject playerSprite;
    public GameObject snowballProjectile;
    private Animator animator;
    private bool isIdle = true;
    public TreeSpawner ChristmasTreeSpawner;
    public GameObject treeExplode;
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

            if (horizontalInput < 0)
                playerSprite.GetComponent<SpriteRenderer>().flipX = true;
            else if (horizontalInput > 0)
                playerSprite.GetComponent<SpriteRenderer>().flipX = false;


        }
        else if (!isIdle)
        {
            isIdle = true;
            animator.SetBool("IsMoving", false);
        }


        if (Input.GetKeyDown(KeyCode.Space))//spawns snowball on sapce
        {


            GameObject snowball = Instantiate(snowballProjectile, transform.position, Quaternion.identity);
            snowball.SetActive(true);


            Rigidbody2D snowballThrow = snowball.GetComponent<Rigidbody2D>();
            snowballThrow.velocity = new Vector2((playerSprite.GetComponent<SpriteRenderer>().flipX ? -1 : 1) * 10f, 0f);

            Destroy(snowball, 3.0f);
        }
        //stops tree from sending player to moon
        Rigidbody2D player = GetComponent<Rigidbody2D>();
        player.velocity = Vector2.zero;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ChristmasTree"))
        {

            EventManager.playerScore += 100;
            EventManager.playerHealth--;

            //explodes tree
            GameObject treeExplosion = Instantiate(treeExplode, other.transform.position, Quaternion.identity);
            treeExplosion.SetActive(true);
            Destroy(treeExplosion, 0.5f);

            ChristmasTreeSpawner.MoveToPool(other.gameObject);
        }
    }
}
