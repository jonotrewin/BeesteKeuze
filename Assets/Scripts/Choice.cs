using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Choice
{
    [Header("Choice Text")]
    [Tooltip("Add the text to be displayed on choice here")]
    [TextArea]
    public string Text;

    [System.Serializable]
    public struct statChange
    {
        public Manager.Stats stat;
        public Manager.Impact impact;
    }

    [Space(40)]
    [Header("Positive Stats")]
    [Tooltip("Choose which stats are increased on choice, and by what degree")]
    public statChange[] positiveStats;

    [Space(40)]
    [Header("Negative Stats")]
    [Tooltip("Choose which stats are decreased on choice, and by what degree")]
    public statChange[] negativeStats;


    [Space(40)]
    [Header("Cards To Add")]
    [Tooltip("List of Event Cards that are added to the deck")]
    public MinorEvent[] eventsToAdd;


}
