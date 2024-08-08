using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ElectionManager : MonoBehaviour
{
    [SerializeField] Canvas electionCanvas;
    [SerializeField] Party playerParty;
    [SerializeField] PartyCard[] allOppositionParties; 
    [SerializeField] GameObject[] allSeats;

    public List<Party> selectedParties = new List<Party> ();

    [SerializeField] GameObject startGameButton;

    [SerializeField] TextMeshProUGUI totalSeatsUI;
    [SerializeField] TextMeshProUGUI coalitionStrength;

    [SerializeField] GameObject TagPrefab;
    [SerializeField] GameObject TagPanel;
    List<GameObject> ui_Tags = new List<GameObject>();


    public int amountOfOpposingTags = 0; //coalition strength
    int totalSeatsInCoalition;

    private void Start()
    {
        RunElection();
        totalSeatsUI.text = totalSeatsInCoalition.ToString();

        string[] tags = Enum.GetNames(typeof(Party.Tags));

        foreach(string tag in tags)
        {
           GameObject tagText =  Instantiate(TagPrefab, TagPanel.transform.position,Quaternion.identity,TagPanel.transform);
           tagText.GetComponentInChildren<TextMeshProUGUI>().text = tag;
            ui_Tags.Add(tagText);
            tagText.SetActive(false);
           
        }
    }

    public void RunElection()
    {

        if (!electionCanvas.isActiveAndEnabled) electionCanvas.gameObject.SetActive(true);
        playerParty.totalSeats = Mathf.Max(40, Mathf.Min(100,Manager.instance.Happiness));
        //playerParty.totalSeats = 75;
        totalSeatsInCoalition += playerParty.totalSeats;

        int totalRemainingSeats = 150-playerParty.totalSeats;
        
        
        for(int i = 0; i<allOppositionParties.Length; i++)
        {
            allOppositionParties[i].seats.Clear();


            int seatsToGive;
            
            if (i == allOppositionParties.Length - 1) seatsToGive = totalRemainingSeats;
            else seatsToGive = Random.Range(1, totalRemainingSeats - allOppositionParties.Length - i);
            
            allOppositionParties[i].party.totalSeats = seatsToGive;
            totalRemainingSeats -= seatsToGive;
        }

        fillUISeats();
        Manager.instance.dayManager.ResetDays();
    }

    private void fillUISeats()
    {
        int currentIndex = 0;

       
        for(int i = 0; i<playerParty.totalSeats; i++)
        {
            allSeats[currentIndex].GetComponent<Image>().color = playerParty.partyColor;
            currentIndex++;
        }

        foreach (PartyCard p in allOppositionParties)
        {
            for(int i=0; i<p.party.totalSeats; i++)
            {
                p.seats.Add(allSeats[currentIndex]);
                currentIndex++;
           }

            foreach(GameObject seat in p.seats)
            {
                seat.GetComponent<Image>().color = p.party.partyColor;
            }
        }

        
    }

    public void AddParty(PartyCard card)
    {
        if (card.isSelected) selectedParties.Add(card.party);
        else selectedParties.Remove(card.party);

        totalSeatsInCoalition = playerParty.totalSeats;
        foreach(Party party in selectedParties)
        {
            totalSeatsInCoalition += party.totalSeats;
        }

        if (totalSeatsInCoalition >= 150 * 0.5f) startGameButton.SetActive(true);
        else startGameButton.SetActive(false);

        totalSeatsUI.text = totalSeatsInCoalition.ToString();

        CheckForInconsistentIdeals();



        
    }

    private void CheckForInconsistentIdeals()
    {
        string[] tags = Enum.GetNames(typeof(Party.Tags));

        amountOfOpposingTags = 0;

        foreach (GameObject uiTag in ui_Tags)
        {
            uiTag.SetActive(false);
        }


            foreach (string tag in tags)
        {
            bool hasPositiveInGovernment = false;
            bool hasNegativeInGovernment = false;

            foreach (Party party in selectedParties)
            {
                foreach(Party.Tags likeTag in party.partyLikes)
                {
                    if(likeTag.ToString() == tag)
                    {
                        hasPositiveInGovernment = true;
                    }
                }

                foreach (Party.Tags dislikeTag in party.partyDislikes)
                {
                    if (dislikeTag.ToString() == tag)
                    {
                        hasNegativeInGovernment = true;
                    }
                }
            }
            
            foreach(GameObject uiTag in ui_Tags)
            {
                if (hasPositiveInGovernment && hasNegativeInGovernment && uiTag.GetComponentInChildren<TextMeshProUGUI>().text == tag)
                {
                    Debug.Log(tag);
                    uiTag.SetActive(true);
                    amountOfOpposingTags++;
                }
                
            }

            if (totalSeatsInCoalition > 75)
            {
                switch (amountOfOpposingTags)
                {
                    case 0: coalitionStrength.text = "Sterk"; break;
                    case 1: coalitionStrength.text = "Stabiel"; break;
                    case 2: coalitionStrength.text = "Fragiel"; break;
                    case > 2: coalitionStrength.text = "Zwak"; break;

                }
            }
            else coalitionStrength.text = "";



           
        }
    }

    public void CloseElectionScreen()
    {
        electionCanvas.gameObject.SetActive(false);
        Manager.instance.DrawEvent();
        Manager.instance.GetNextCharacter();

    }
   
}
