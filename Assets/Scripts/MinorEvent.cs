using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Minor Event")]
public class MinorEvent : ScriptableObject
{
    public Sprite image;

    [Header("Event Text")]
    [TextArea]
    public string text;


    [Space(20)]
    [Header("Choices")]
    public Choice[] choices = new Choice[2];

    
    
}
