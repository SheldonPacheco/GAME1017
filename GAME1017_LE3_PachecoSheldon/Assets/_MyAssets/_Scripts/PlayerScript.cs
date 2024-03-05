using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    int index = 0;

    void Start()
    {
        an = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveShip();
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Teleport();
        }
        WrapPlayer();
    }

    void MoveShip()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            float angle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 360f, Vector3.forward);
        }

        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        rb.AddForce(movement * moveForce * Time.deltaTime);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        if (horizontalInput > 0 || verticalInput > 0 || (horizontalInput > 0 && verticalInput > 0))
        {
            an.SetBool("hasThrust", true);
            if (Game.Instance != null)
                Game.Instance.SOMA.PlaySound("Engines");
        }
        else if (horizontalInput == 0 && verticalInput == 0)
        {
            an.SetBool("hasThrust", false);
        }
    }

    private void WrapPlayer()
    {
        float screenWidth = 25f;

        if (transform.position.x < -screenWidth / 2f)
        {
            transform.position = new Vector2(screenWidth / 2f, transform.position.y);
        }
        else if (transform.position.x > screenWidth / 2f)
        {
            transform.position = new Vector2(-screenWidth / 2f, transform.position.y);
        }

        float screenHeight = 15f;

        if (transform.position.y < -screenHeight / 2f)
        {
            transform.position = new Vector2(transform.position.x, screenHeight / 2f);
        }
        else if (transform.position.y > screenHeight / 2f)
        {
            transform.position = new Vector2(transform.position.x, -screenHeight / 2f);
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = rb.velocity.magnitude;
        float clampedSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        rb.velocity = rb.velocity.normalized * clampedSpeed;

        magnitude = rb.velocity.magnitude;
    }

    private void SpawnBullet()
    {
        if (Game.Instance != null)
            Game.Instance.SOMA.PlaySound("Fire");

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        bullet.transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z - 90f);

        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = bullet.transform.up * bulletForce;

        Destroy(bullet, bulletLifespan);
    }

    private void Teleport()
    {
        Vector2 newPosition = FindSafeTeleportSpot();
        transform.position = newPosition;

        if (Game.Instance != null)
            Game.Instance.SOMA.PlaySound("Teleport");
    }

    private Vector2 FindSafeTeleportSpot()
    {
        Vector2 newPosition = Vector2.zero;
        bool foundSafeSpot = false;

        while (!foundSafeSpot)
        {
            newPosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-5f, 5f));

            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 1f);

            if (colliders.Length == 0)
            {
                foundSafeSpot = true;
            }
        }

        return newPosition;
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