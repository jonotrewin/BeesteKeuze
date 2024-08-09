using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCharacter : EventCharacter
{

    [SerializeField] public string endText;

    protected override void CheckAreaDropped()
    {
        if (IsLeft||IsRight)
        {
            StartCoroutine(Manager.instance.EndGame());
        }
    }
    
    

}
