using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MajorEventManager : MonoBehaviour
{
    [SerializeField]GameObject NewspaperCanvas;
    [SerializeField]TextMeshProUGUI headline;
    [SerializeField] TextMeshProUGUI mainBody;
    [SerializeField]TextMeshProUGUI endText;
    [SerializeField] Image headerImage;
    [SerializeField] GameObject choiceButtonsUIGroup;
    [SerializeField] Button[] choiceButtons;

    MajorEvent currentMajorEvent;

    [SerializeField]
    MajorEvent[] listOfAllMajorEvents;

    Choice chosenChoice;
    public void InitializeMajorEvent(MajorEvent mEvent)
    {
        currentMajorEvent = mEvent;
        NewspaperCanvas.SetActive(true);
        headline.text = currentMajorEvent.headline;
        mainBody.text = currentMajorEvent.newspaperBody;
        headerImage.sprite = currentMajorEvent.newspaperImage;

        for(int i = 0; i < choiceButtons.Length; i++)
        {
            Choice choice = currentMajorEvent.newspaperChoices[i].choice;
            string ending = currentMajorEvent.newspaperChoices[i].endingText;

            choiceButtons[i].onClick.AddListener(()=> 
            {

                chosenChoice = choice;
                endText.text = ending;
                endText.gameObject.SetActive(true);
                choiceButtonsUIGroup.SetActive(false);
                
                

            }
            );
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentMajorEvent.newspaperChoices[i].choice.Text;
        }



    }

    public void exitMajorEvent()
    {
        if (chosenChoice != null) Manager.instance.ExecuteChoice(chosenChoice);
        else return;
        endText.gameObject.SetActive(false);    
        NewspaperCanvas.SetActive(false);
        StartCoroutine(Manager.instance.DrawEvent());
    }

    public void AddMajorEvent(bool requestSpecificEvent = false, string eventName = "")
    {
        Manager.instance.minorEventDeck.Add(listOfAllMajorEvents[Random.Range(0, listOfAllMajorEvents.Length)]);
    }
}
