using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{

    public static Manager instance;

    // Unity UI Pieces//
    [Header("Unity UI Pieces")]
    [SerializeField] TextMeshProUGUI eventText;
    [SerializeField] Image eventImage;
    [SerializeField] TextMeshProUGUI choice1Text;
    [SerializeField] TextMeshProUGUI choice2Text;

    [SerializeField] TextMeshProUGUI happinessText;
    [SerializeField] TextMeshProUGUI collabText;
    [SerializeField] TextMeshProUGUI fundsText;



    ///

    //List Of Current Stats///

    [Header("Statistics")]
    [SerializeField]int _collaboration;
    [SerializeField] int _happiness;
    [SerializeField]int _funds;
   int Collaboration
    {
        get { return _collaboration; }
        set { 
            _collaboration = value;
            collabText.text = _collaboration.ToString();
            }
    }
     int Happiness
    {
        get { return _happiness; }
        set
        {
            _happiness = value;
            happinessText.text = _happiness.ToString();
        }
    }

    int Funds
    {
        get { return _funds; }
        set
        {
            _funds = value;
            fundsText.text = _funds.ToString();
        }
    }

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

    [Header("Event Cards and Decks")]
    [Tooltip("Add Generics that will be added to the deck if the deck is empty!")]
    public List<MinorEvent> genericEvents = new List<MinorEvent>();

    public List<MinorEvent> minorEventDeck = new List<MinorEvent>();



    private void Start()
    {
        if(instance != null)
        { Destroy(instance); }
        instance = this;

        DontDestroyOnLoad(this);
        fundsText.text = _funds.ToString();
        happinessText.text = _happiness.ToString();
        collabText.text = _collaboration.ToString();

        

    }

    public void InitializeMinorEventUI(MinorEvent minorEvent)
    {
        eventText.text = minorEvent.text;
        
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

    public void DrawEvent()
    {
       
        ShowLeftText(false);
        ShowRightText(false);
        if (minorEventDeck.Count <= 1)
        {
            minorEventDeck.Add(genericEvents[Random.Range(0, genericEvents.Count)]);
            minorEventDeck.Add(genericEvents[Random.Range(0, genericEvents.Count)]);
        }
       
        minorEventDeck.RemoveAt(0);
        currentMinorEvent = minorEventDeck[0];
        InitializeMinorEventUI(currentMinorEvent);
        Instantiate(currentMinorEvent.character, new Vector2(Screen.width / 2, Screen.height / 2), Quaternion.identity, null);

        
  

    }
    
    
    private void ExecuteChoice(Choice choice)
    {
        UpdateStats(choice.positiveStats, 1);
        UpdateStats(choice.negativeStats, -1);
        foreach(MinorEvent minorEvent in choice.eventsToAdd)
        {
            if(minorEvent != null)
            minorEventDeck.Add(minorEvent);
        }

        DrawEvent();

        
    }

    public void ExecuteLeftChoice()
    {
        ExecuteChoice(currentMinorEvent.choices[0]);
    }
    public void ExecuteRightChoice()
    {
        ExecuteChoice(currentMinorEvent.choices[1]);
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


    public void ShowLeftText(bool value)
    {

        choice1Text.gameObject.SetActive(value);
    }
    public void ShowRightText(bool value)
    {
        choice2Text.gameObject.SetActive(value);
    }
}
