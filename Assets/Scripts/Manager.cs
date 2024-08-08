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
    public DayManager dayManager;
    public MajorEventManager majorManager;
    public ElectionManager electionManager;

    public bool CanInteract;

    // Unity UI Pieces//
    [Header("Unity UI Pieces")]
    [SerializeField] TextMeshProUGUI eventText;
    [SerializeField] Image eventImage;
    [SerializeField] TextMeshProUGUI choice1Text;
    [SerializeField] TextMeshProUGUI choice2Text;

    [SerializeField] TextMeshProUGUI happinessText;
    [SerializeField] TextMeshProUGUI collabText;
    [SerializeField] TextMeshProUGUI fundsText;

    GameObject currentCharacter;

    [SerializeField] UI_PartySymbol[] leftSymbols;
    [SerializeField] UI_PartySymbol[] rightSymbols;


    [SerializeField]
    GameObject[] choiceUI;

    ///

    //List Of Current Stats///

    [Header("Statistics")]
    [SerializeField]int _collaboration = 50;
    [SerializeField] int _happiness = 50;
    [SerializeField]int _funds = 50;

    [SerializeField] int collabReductionForCoalitionDisagreement = 10;


   public int Collaboration
    {
        get { return _collaboration; }
        set {
            if (value <= 0) value = 0;
            if (value >= 100) value = 100;
            _collaboration = value;
            collabText.text = _collaboration.ToString();
            }
    }
     public int Happiness
    {
        get { return _happiness; }
        set
        {
            if (value <= 0) value = 0;
            if (value >= 100) value = 100;
            _happiness = value;
            happinessText.text = _happiness.ToString();
        }
    }

    public int Funds
    {
        get { return _funds; }
        set
        {
            if (value <= 0) value = 0;
            if (value >= 100) value = 100;
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



    private void Awake()
    {
        if(instance != null)
        { Destroy(instance); }
        instance = this;

        DontDestroyOnLoad(this);
        fundsText.text = _funds.ToString();
        happinessText.text = _happiness.ToString();
        collabText.text = _collaboration.ToString();

        dayManager = GetComponent<DayManager>(); 
        majorManager = GetComponent<MajorEventManager>();

        

    }

    public void InitializeMinorEventUI(MinorEvent minorEvent)
    {
        
        eventText.text = minorEvent.text;
        eventText.transform.parent.gameObject.SetActive(false);
        
        choice1Text.text = minorEvent.choices[0].Text;
        choice2Text.text = minorEvent.choices[1].Text;
        


    }


    public MinorEvent currentMinorEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(DrawEvent());
        }

    }

    public IEnumerator DrawEvent()
    {

        Destroy(currentCharacter);


        ShowLeftText(false);
        ShowRightText(false);
        if (minorEventDeck.Count <= 1)
        {
            minorEventDeck.Add(genericEvents[Random.Range(0, genericEvents.Count)]);
            minorEventDeck.Add(genericEvents[Random.Range(0, genericEvents.Count)]);
        }

        minorEventDeck.RemoveAt(0);
        currentMinorEvent = minorEventDeck[0];
        dayManager.DecreaseDays();
        yield return new WaitForSeconds(2f);
    

    }

    public void GetNextCharacter()
    {
        InitializeMinorEventUI(currentMinorEvent);
        currentCharacter = Instantiate(currentMinorEvent.character);
    }

    public void ExecuteChoice(Choice choice)
    {
        
        foreach(Party.Tags tag in choice.tags)
        {
            foreach (Party party in Manager.instance.electionManager.selectedParties)
            {
                if (party.partyDislikes.Contains(tag))
                {
                    Collaboration -= collabReductionForCoalitionDisagreement;
                }
               
            }
        }
        UpdateStats(choice.positiveStats, 1);
        UpdateStats(choice.negativeStats, -1);
        foreach(MinorEvent minorEvent in choice.eventsToAdd)
        {
            if(minorEvent != null)
            minorEventDeck.Add(minorEvent);
        }

        ActivateText(false);

       
       
      


        
    }

    public void ExecuteLeftChoice()
    {
        ExecuteChoice(currentMinorEvent.choices[0]);
        StartCoroutine(DrawEvent());
        
    }
    public void ExecuteRightChoice()
    {
        ExecuteChoice(currentMinorEvent.choices[1]);
        StartCoroutine(DrawEvent());
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


    

    private void CheckPartyAgreement(Choice choice, UI_PartySymbol[] symbols)
    {
        foreach(UI_PartySymbol sym in symbols)
        {
            sym.gameObject.SetActive(false);
        }
        

        foreach (Party.Tags tag in choice.tags)
        {
            Debug.Log(tag);
            foreach(Party party in Manager.instance.electionManager.selectedParties)
            {
                if(party.partyLikes.Contains(tag))
                {
                    
                    foreach(UI_PartySymbol symbol in symbols)
                    {
                       
                        if (symbol.party == party && symbol.positive) symbol.gameObject.SetActive(true);
                       
                    }
                }

                if (party.partyDislikes.Contains(tag))
                {
                    foreach (UI_PartySymbol symbol in symbols)
                    {
                        Debug.Log("Hit Neg!");
                        if (symbol.party == party && !symbol.positive) symbol.gameObject.SetActive(true);
                  
                    }
                }
            }
        }
    }

    public void ShowLeftText(bool value)
    {
        //activates parent with vertical spacing group, instead of just the text - so the coalition (dis-)agreement appears

        if (!choiceUI[0].activeSelf) CheckPartyAgreement(currentMinorEvent.choices[0], leftSymbols);
        choiceUI[0].SetActive(value);

    }

    public void ShowRightText(bool value)
    {
        if (!choiceUI[1].activeSelf) CheckPartyAgreement(currentMinorEvent.choices[1], rightSymbols);
        choiceUI[1].SetActive(value);
    }

    public void SetInteraction(bool value)
    {
        CanInteract = value;
    }

    public void ActivateText(bool value)
    {
        eventText.transform.parent.gameObject.SetActive(value);
    }
}
