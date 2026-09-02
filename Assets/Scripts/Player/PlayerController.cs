using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerView view;
    public PlayerModel model;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PlayerView>();
        model = new PlayerModel();
        view.SetModel(model);
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
        CheckGrounded();
        Jump();
        Attack();
        SwitchAnim();
    }

    private void Run()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        if (view != null)
        {
            Rigidbody2D rb = view.rb2d;
            rb.velocity = new Vector2(moveX * model.moveSpeed, rb.velocity.y);

            bool isRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
            view.SetAnimBool("Run", isRunning);
        }
    }

    private void Flip()
    {
        if (view != null)
        {
            Rigidbody2D rb = view.rb2d;
            bool isRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
            if (!isRunning)
            {
                return;
            }
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    private void Jump()
    {
        Rigidbody2D rigidBody2d = view.rb2d;
        if (rigidBody2d == null)
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (model.isGround)
            {
                view.SetAnimBool("Jump", true);
                rigidBody2d.velocity = Vector2.up * new Vector2(0f, model.jumpSpeed);
                model.maxJumpCount = 1; // Reset jump count when grounded
            }
            else
            {
                if (model.maxJumpCount > 0)
                {
                    view.SetAnimBool("DoubleJump", true);
                    rigidBody2d.velocity = Vector2.up * new Vector2(0f, model.doubleJumpSpeed);
                    model.maxJumpCount--;
                }
            }
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            view.SetAnimTrigger("Attack");
        }
    }

    private void CheckGrounded()
    {

        BoxCollider2D feet = view.feet;
        if (feet == null) { return; }

        model.isGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (model.isGround)
        {
            view.SetAnimBool("DoubleJump", false);
        }
    }

    private void SwitchAnim()
    {
        Rigidbody2D rigidBody2d = view.rb2d;
        if (rigidBody2d == null)
        {
            return;
        }
        view.SetAnimBool("Idle", false);
        bool isJumping = view.GetAnimBool("Jump");
        if (isJumping)
        {
            if (rigidBody2d.velocity.y < 0.0f)
            {
                view.SetAnimBool("Jump", false);
                view.SetAnimBool("Fall", true);
            }
        }
        else
        {
            if (model.isGround)
            {
                view.SetAnimBool("Fall", false);
                view.SetAnimBool("Idle", true);
            }
        }

        if (view.GetAnimBool("DoubleJump"))
        {
            if (rigidBody2d.velocity.y < 0.0f)
            {
                view.SetAnimBool("DoubleJump", false);
                view.SetAnimBool("DoubleFall", true);
            }
        }
        else
        {
            if (model.isGround)
            {
                view.SetAnimBool("DoubleFall", false);
                view.SetAnimBool("Idle", true);
            }
        }
    }
}
