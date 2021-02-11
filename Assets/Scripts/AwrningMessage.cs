using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Message", order = 1, menuName = "Advices/Awarning")]
public class AwrningMessage : ScriptableObject
{
    [SerializeField, TextArea(5, 10), Tooltip("Message for advice object"), Header("Configures your message")]
    string message;
    [SerializeField]
    Color borderColor = Color.white;

    public string Message => message;
    public Color BorderColor => borderColor;
}
