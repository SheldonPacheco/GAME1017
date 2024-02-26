using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;
    [SerializeField] float bulletLifespan;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float rotSpeed;
    [SerializeField] float moveForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float magnitude;
    private Animator an;
    private Rigidbody2D rb;
    void Start()
    {
        an = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Rotate the ship.
        // TODO: Fill in for Week 5 lab.


        // Add forward thrust.
        // TODO: Fill in behaviour for Week 5 lab.
        //
        //
        //

        // Spawn a bullet.
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Teleport();
        }
        // Wrap player to other side of screen.
        // TODO: Fill in one wrapping code for Week 5 lab, just to explain it to students.
        //
        //
        //
    }

    void FixedUpdate()
    {
        // Clamp the magnitude of the ship.
        // TODO: Fill in for Week 5.

        // Print the magnitude of the ship.
        magnitude = rb.velocity.magnitude;
    }

    private void SpawnBullet()
    {
        // Play the Fire sound.
        if (Game.Instance != null)
            Game.Instance.SOMA.PlaySound("Fire");

        // Instantiate a bullet at the spawn point.
        // TODO: Fill in for Week 5.
        //

        // Set the bullet's velocity based on the ship's rotation.
        // TODO: Fill in for Week 5.
        //

    }

    private void Teleport()
    {
        // Add code to check for a safe position, i.e. a gap in asteroids. For LE3.
        //
        //
        //
        transform.position = new Vector2(Random.Range(-7f,7f), Random.Range(5f,-5f));
        if (Game.Instance != null) 
            Game.Instance.SOMA.PlaySound("Teleport");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
            if (Game.Instance != null)
                Game.Instance.SOMA.PlaySound("Explode");
        }
    }
}
