using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Vector2 moveInput;
    private bool isGround = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGround = false;
            anim.Play("Jump");
        }
    }

    public void OnTeleport(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.velocity = Vector2.zero;
            transform.position = new Vector2(-2.5f, 2.75f);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        UpdateAnim();
    }

    private void UpdateAnim()
    {
        if (!isGround) anim.Play("Jump");
        else if (Mathf.Abs(rb.velocity.x) > 0.01f) anim.Play("Run");
        else anim.Play("Idle");

        sprite.flipX = moveInput.x < 0 ? true : moveInput.x > 0 ? false : sprite.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGround = true;
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            transform.position = new Vector2(-2.5f, 2.75f);
        }
    }

    public void SetJumpPower(float newJumpPower)
    {
        jumpPower = newJumpPower;
    }

    public void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }
}