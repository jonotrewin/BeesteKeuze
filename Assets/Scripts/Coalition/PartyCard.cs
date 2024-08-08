using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartyCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Party party;

    public List<GameObject> seats;

    public bool isSelected;

    public UnityEvent onClick;



    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = !isSelected;
        onClick.Invoke();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover");
        foreach (GameObject seat in seats)
        {

            seat.transform.DORotate(transform.rotation.eulerAngles+new Vector3(0,0,20), 1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        foreach (GameObject seat in seats)
        {
            seat.transform.DORotate(transform.rotation.eulerAngles, 1f);
        }
    }
}
