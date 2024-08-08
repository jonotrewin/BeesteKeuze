using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    int daysUntilNextElection;
    [SerializeField]int minimumDaysBetweenEvents = 3;
    [SerializeField]int maximumDaysBetweenEvents = 10;

    int eventsPassedThisPeriod = 0;

    [SerializeField]TextMeshProUGUI uiDaysTillNext;


    [SerializeField]float uiTickdownTime = 0.3f;
    private void Start()
    {
        ResetDays();
    }
    public void DecreaseDays()
    {
        daysUntilNextElection -= Random.Range(minimumDaysBetweenEvents,maximumDaysBetweenEvents);
        if (daysUntilNextElection <= 0) daysUntilNextElection = 0;
        if (int.Parse(uiDaysTillNext.text) != daysUntilNextElection)
        {
            StartCoroutine(UpdateUI());
        }
    }

    public void ResetDays()
    {
        daysUntilNextElection = 365 * 5;
        eventsPassedThisPeriod = 0;
        uiDaysTillNext.text = daysUntilNextElection.ToString();
    }

    public IEnumerator UpdateUI()
    {
        yield return new WaitForSeconds(uiTickdownTime);
        int daysTillNext = int.Parse(uiDaysTillNext.text);
        daysTillNext -= 1;
        uiDaysTillNext.text = daysTillNext.ToString();
        if(daysTillNext != daysUntilNextElection)
        {
            StartCoroutine(UpdateUI());
        }
        else {
            yield return new WaitForSeconds(2f);
            Manager.instance.GetNextCharacter(); 
        }

        if (daysUntilNextElection <= 0)
        {
            yield return new WaitForSeconds(5f);
            Manager.instance.electionManager.RunElection();
        }
     
       
        

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            DecreaseDays();
        }
    }
}
