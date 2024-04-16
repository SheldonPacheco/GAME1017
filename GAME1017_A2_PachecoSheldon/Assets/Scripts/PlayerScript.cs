using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private bool isGrounded; 
    [SerializeField] private float moveForce;
    [SerializeField] private float jumpForce;
    public LayerMask groundLayer;
    private Animator an;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;

    void Start()
    {
        an = GetComponentInChildren<Animator>();
        isGrounded = false; 
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        GroundedCheck();

        // Horizontal movement.
        float moveInput = Input.GetAxis("Horizontal");
        float moveInputRaw = Input.GetAxisRaw("Horizontal"); // Clamped to -1, 0, or 1.
        // bool isMoving = Mathf.Abs(moveInputRaw) > 0f;
        an.SetBool("Walking", Mathf.Abs(moveInputRaw) > 0f);
        // Set horizontal force in player. Use current vertical velocity.
        rb.velocity = new Vector2(moveInput * moveForce * Time.fixedDeltaTime, rb.velocity.y);

        // Trigger jump. Use current horizontal velocity. Cannot jump in a roll.
        if (isGrounded && !an.GetBool("Rolling") && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.playerJump);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.S))
        {
            an.SetBool("Rolling", true);
            cc.offset = new Vector2(-0.06393222f, 0.03418268f);
            cc.size = new Vector2(0.4365608f, 0.4818729f);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.playerRolling);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            an.SetBool("Rolling", false);
            cc.offset = new Vector2(-0.06393222f, -0.06967986f);
            cc.size = new Vector2(0.4365608f, 1.066089f);
        }
    }

        private void GroundedCheck()
        {
        isGrounded = GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"));

        an.SetBool("Jumping", !isGrounded);
        }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (EventManager.invulnerableTimer <= 0)
        {
            if (collision.CompareTag("Obstacle"))
            {
                EventManager.playerHealth--;
                SoundManager.Instance.PlaySFX(SoundManager.Instance.playerHit);
            }
        }
    }
}
