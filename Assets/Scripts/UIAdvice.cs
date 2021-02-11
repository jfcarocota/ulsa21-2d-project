using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAdvice : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI messageContent;
    [SerializeField]
    Image borderImage;
    float timer = 0f;
    [SerializeField, Range(0.1f, 10f)]
    float timeLimit = 0.5f; 

    public void SetBorderColor(Color color)
    {
        borderImage.color = color;
    }

    public void SetMessageContent(string message)
    {
        messageContent.text = message;
    }

    public void InitAdvice(Color color, string message)
    {
        if(gameObject.activeSelf) return;
        borderImage.color = color;
        messageContent.text = message;
        gameObject.SetActive(true);
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeLimit)
        {
            timer = 0f;
            gameObject.SetActive(false);
        }
    }
}
