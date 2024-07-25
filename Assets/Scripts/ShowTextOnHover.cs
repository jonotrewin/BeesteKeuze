using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTextOnHover : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject textToShow;
    public void OnPointerEnter(PointerEventData eventData)
    {
        textToShow.SetActive(true);
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textToShow.SetActive(false);
        Debug.Log("Exit");
    }
}
