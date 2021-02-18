using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerController2D
{
    [SerializeField, Range(0.1f, 10f)]
    float moveSpeed = 1f;

    [SerializeField, Range(0.1f, 15f)]
    float jumpForce;

    new void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        playerControls.Gameplay.Jump.performed += ctx => Jump();
    }

    void Update()
    {
        //TranslateMovement(moveSpeed);
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
        //VelocityMovement(moveSpeed);
        //DirectVelocityMovement(moveSpeed);
        AddForceMovement(moveSpeed);
        anim.SetFloat("velocityY", rb2D.velocity.y);
        anim.SetBool("ground", IsGrounding);
    }

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
