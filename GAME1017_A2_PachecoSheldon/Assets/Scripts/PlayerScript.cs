using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (isGrounded && !an.GetBool("sliding") && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            //SoundManager.Instance.SOMA.PlaySound("Jump");
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.S))
        {
            an.SetBool("sliding", true);
            cc.offset = new Vector2(0.33f, -1f);
            cc.size = new Vector2(2f, 2f);
            //SoundManager.Instance.SOMA.PlayLoopedSound("Roll");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            an.SetBool("sliding", false);
            cc.offset = new Vector2(0.33f, -0.25f);
            cc.size = new Vector2(2f, 3.5f);
            //SoundManager.Instance.SOMA.StopLoopedSound();
        }
    }

        private void GroundedCheck()
        {
        isGrounded = GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"));

        an.SetBool("Jumping", !isGrounded);
        }
}
