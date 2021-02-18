using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController2D : MonoBehaviour
{

    protected SpriteRenderer spr;
    protected Animator anim;
    protected Rigidbody2D rb2D;
    protected PlayerControls playerControls;
    [SerializeField]
    protected ContactFilter2D groundFilter;
    [SerializeField, Range(0.1f, 5f)]
    protected float maxVelX = 3f;
    Vector2 clampVelocity;

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable() 
    {
        playerControls.Disable();
    }

    protected void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
    }

    //Transform
    protected void TranslateMovement(float speed)
    {
        transform.Translate(Vector2.right * axis.x * speed * Time.deltaTime);
    }

    //Rigidbody velocity
    protected void VelocityMovement(float speed)
    {
        rb2D.velocity += new Vector2(axis.x * speed * Time.fixedDeltaTime, 0f);
        clampVelocity = Vector2.ClampMagnitude(rb2D.velocity, maxVelX);
        rb2D.velocity = new Vector2(clampVelocity.x, rb2D.velocity.y);
    }

    protected void DirectVelocityMovement(float speed)
    {
        rb2D.velocity = new Vector2(axis.x * speed * 100f * Time.fixedDeltaTime, rb2D.velocity.y);
        MaxVelImplement();
    }

    protected void AddForceMovement(float speed)
    {
        rb2D.AddForce(Vector2.right * axis.x * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        MaxVelImplement();
    }

    void MaxVelImplement()
    {
        clampVelocity = Vector2.ClampMagnitude(rb2D.velocity, maxVelX);
        rb2D.velocity = new Vector2(clampVelocity.x, rb2D.velocity.y);
    }

    protected Vector2 axis => playerControls.Gameplay.Movement.ReadValue<Vector2>();

    protected bool flipSprite => axis.x > 0 ? false : axis.x < 0 ? true : spr.flipX;

    protected bool IsGrounding => rb2D.IsTouching(groundFilter);
}
