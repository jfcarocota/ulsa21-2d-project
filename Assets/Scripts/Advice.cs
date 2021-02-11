using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advice : MonoBehaviour
{
    [SerializeField]
    AwrningMessage message;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.GetUIAdvice.InitAdvice(message.BorderColor, message.Message);
        }    
    }
}
