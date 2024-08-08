using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scrollable : MonoBehaviour, IScrollHandler
{

    public float scrollDelay = 3;
    public float scrollSpeed = 40;

    public float startingPosition;
    public float destinationPosition;
    public void OnScroll(PointerEventData eventData)
    {
        destinationPosition -= eventData.scrollDelta.y*scrollSpeed;

        
        
       
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y != destinationPosition)
        transform.DOMoveY(destinationPosition, scrollDelay);
    }
}
