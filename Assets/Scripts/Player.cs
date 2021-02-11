using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)]
    float moveSpeed = 1f;

    SpriteRenderer spr;
    Animator anim;
    [SerializeField, Range(0.1f, 15f)]
    float jumpForce;
    Rigidbody2D rb2D;

    [SerializeField]
    ContactFilter2D groundFilter;

    PlayerControls playerControls;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
    }

    void Start()
    {
        playerControls.Gameplay.Jump.performed += ctx => Jump();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable() 
    {
        playerControls.Disable();
    }

    void Update()
    {
        transform.Translate(Vector2.right * axis.x * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if(IsGrounding)
        {
            anim.SetTrigger("jump");
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void LateUpdate() 
    {
        spr.flipX = flipSprite;
        anim.SetFloat("moveX", Mathf.Abs(axis.x));
    }

    void FixedUpdate() 
    {
        anim.SetFloat("velocityY", rb2D.velocity.y);
        anim.SetBool("ground", IsGrounding);
    }

    Vector2 axis => playerControls.Gameplay.Movement.ReadValue<Vector2>();

    bool flipSprite => axis.x > 0 ? false : axis.x < 0 ? true : spr.flipX;

    bool IsGrounding => rb2D.IsTouching(groundFilter);

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            GameManager.instance.GetScore.AddPoints(coin.Points);
            Destroy(other.gameObject);
        }    
    }
}
