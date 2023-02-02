using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpFore;

    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float horizontal;

    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        body.velocity = new Vector2(horizontal * speed, body.velocity.y);

        if (horizontal > 0 )
        {
            transform.localScale = Vector2.one;
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Jump();
        }

        SetAnim();
    }
    private void Jump()
    {
        SoundManager.instance.PlaySound(jumpSound);
        body.velocity = new Vector2(body.velocity.x, jumpFore);
    }
    private void SetAnim()
    {
        anim.SetBool("walk", horizontal != 0);
        anim.SetBool("grounded", isGrounded());
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return horizontal == 0 && isGrounded();
    }
}
