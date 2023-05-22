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

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask JumpableGround;

    int addjump= 0;


    private enum MovementState { idle, running, jumping, falling, doublejump }
    

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
        }

        if (Input.GetButtonDown("Jump"))
        {

            if (IsGrounded() || doubleJump)
            {
                Debug.Log(addjump);

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                if (IsGrounded() || doubleJump)
                {
                    addjump += 1;

                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                    doubleJump = !doubleJump;
                }
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }


        UpdateAnimationState();
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, JumpableGround);    
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

        if (addjump == 1)
        {
            state = MovementState.jumping;
        }
        else if (addjump == 2)
        {
            state = MovementState.doublejump;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }


        anim.SetInteger("State", (int)state);
    }
}
