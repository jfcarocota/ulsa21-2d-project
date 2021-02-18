using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    float timer;
    [SerializeField, Range(0.1f, 5f)]
    float waitTime = 3f;
    [SerializeField]
    Vector2 direction = Vector2.right;
    [SerializeField, Range(0.1f, 5f)]
    float moveSpeed = 3f;
    Rigidbody2D rb2D;

    void Awake() 
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= waitTime)
        {
            timer = 0;
            if(direction == Vector2.right)
            {
                direction = Vector2.left;
                return;
            }
            if(direction == Vector2.left)
            {
                direction = Vector2.right;
                return;
            }
        }

        //transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void FixedUpdate() 
    {
        rb2D.position += new Vector2(direction.x * moveSpeed * Time.fixedDeltaTime, 0);
    }
}
