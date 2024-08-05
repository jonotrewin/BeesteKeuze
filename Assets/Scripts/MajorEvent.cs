using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Major Event")]
public class MajorEvent : MinorEvent
{
    public Sprite newspaperImage;
    public string headline;
    [TextArea()]
    public string newspaperBody;
    public newspaperChoice[] newspaperChoices = new newspaperChoice[3];



    [Serializable]
    public struct newspaperChoice
    {
        public Choice choice;
        public string endingText;
    }

}
