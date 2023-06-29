using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;

    private float dirX = 0f;
    private bool doubleJump;
    private bool falling;

    private bool isWallSliding = false;
    private float wallSlidingSpeed = 2f;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask JumpableGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask JumpableWall;

    private int jumpCount;


    private enum MovementState { idle, running, jumping, falling, doublejump, wallslide, walljump }
    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;

            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {

            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                if (IsGrounded() || doubleJump || falling)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                    doubleJump = !doubleJump;

                    falling = !falling;

                    jumpCount++;
                }
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        WallSlide();

        UpdateAnimationState();

        Debug.Log(falling);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            falling = false;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            falling = true;
        }
    }


            private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .2f, JumpableGround);    
    }

    private bool IsWalled_R()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right, .1f, JumpableWall);
    }
    private bool IsWalled_L()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right, -.2f, JumpableWall);
    }

    private void WallSlide()
    {
        if (IsWalled_R() || IsWalled_L() && !IsGrounded() && dirX != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            if (jumpCount == 1)
            {
                state = MovementState.jumping;
            }
            if (jumpCount == 2)
            {
                state = MovementState.doublejump;
            }
        }

        else if (rb.velocity.y < -.1f)
        {
            if (isWallSliding == true)
            {
                state = MovementState.wallslide;
            }
            else
            {
                state = MovementState.falling;
            }
        }


        anim.SetInteger("State", (int)state);
    }
}
