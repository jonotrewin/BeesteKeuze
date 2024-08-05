using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorEventCharacter : EventCharacter
{
    [SerializeField]MajorEvent majorEvent;


    protected override void CheckAreaDropped()
    {
        if(IsLeft || IsRight)
        {
            Manager.instance.majorManager.InitializeMajorEvent(majorEvent);
            transform.position = Vector3.zero;
        }
    }
}
