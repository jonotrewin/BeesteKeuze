using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{

    // Unity UI Pieces//
    [Header("Unity UI Pieces")]
    [SerializeField] TextMeshProUGUI eventText;
    [SerializeField] Image eventImage;
    [SerializeField] Button yes;
    [SerializeField] Button no;
    [SerializeField] TextMeshProUGUI choice1Text;
    [SerializeField] TextMeshProUGUI choice2Text;


    ///

    //List Of Current Stats///

    [Header("Statistics")]
    [SerializeField] int Collaboration;
    [SerializeField] int Happiness;
    [SerializeField] int Funds;

    [Space(20)]
    [SerializeField] int[] statIncreaseValues = new int[3];




    public enum Stats
    {
        Collaboration,
        Happiness,
        Funds
    }

    public enum Impact
    {
        Small,
        Medium,
        Large
    }


    public List<MinorEvent> minorEventDeck = new List<MinorEvent>();

    
    public void InitializeMinorEventUI(MinorEvent minorEvent)
    {
        eventText.text = minorEvent.text;
        eventImage.sprite = minorEvent.image;
        choice1Text.text = minorEvent.choices[0].Text;
        choice2Text.text = minorEvent.choices[1].Text;


    }


    public MinorEvent currentMinorEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DrawEvent();
        }

    }

    private void DrawEvent()
    {
        yes.onClick.RemoveAllListeners();
        no.onClick.RemoveAllListeners();

        currentMinorEvent = minorEventDeck[Random.Range(0, minorEventDeck.Count)];
        InitializeMinorEventUI(currentMinorEvent);
        yes.onClick.AddListener(() => ExecuteChoice(currentMinorEvent.choices[0]));
        no.onClick.AddListener(() => ExecuteChoice(currentMinorEvent.choices[1]));

    }

    private void ExecuteChoice(Choice choice)
    {
        UpdateStats(choice.positiveStats, 1);
        UpdateStats(choice.negativeStats, -1);

        DrawEvent();

        
    }

    private void UpdateStats(Choice.statChange[] stats, int v)
    {
        foreach(Choice.statChange stat in stats)
        {
            int statIncrease = statIncreaseValues[(int)stat.impact] * v;
            Debug.Log(statIncrease);
            switch (stat.stat)
            {
                case Manager.Stats.Collaboration:
                    Collaboration += statIncrease;
                    break;
                case Manager.Stats.Funds:
                    Funds += statIncrease;
                    break;
                case Manager.Stats.Happiness:
                    Happiness += statIncrease;
                    break;
            }
        }

        
    }
}
