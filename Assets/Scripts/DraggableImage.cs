using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EventSystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class DraggableImage : MonoBehaviour
{
    [SerializeField]Sprite sprite_Carried;
    [SerializeField] Sprite sprite_Dropped;
    SpriteRenderer image;

    Vector2 startingPosition;

    float leftChoiceCoordinate = Screen.width / 3;
    float rightChoiceCoordinate = Screen.width - (Screen.width / 3);

   public Manager manager;

    bool isLeft;
    bool IsLeft
    {
        get{ return isLeft; }
        set 
        { 
            isLeft = value;
            Manager.instance.ShowLeftText(value);
        }
    }

    bool isRight;
    bool IsRight
    {
        get { return isRight; }
        set
        {
            isRight = value;
            Manager.instance.ShowRightText(value);
        }
    }



    private void Start()
    {
        image = GetComponent<SpriteRenderer>();
        startingPosition = transform.position;
        image.sprite = sprite_Dropped;



    }
    
    void OnMouseDown()
    {
        image.sprite = sprite_Carried;
  
    }

    void OnMouseDrag()
    {
 
        transform.position = Input.mousePosition;
        if(transform.position.x <= leftChoiceCoordinate)
        {
            if(!IsLeft)
            IsLeft=true;
        }
        else IsLeft=false;
        
        if (transform.position.x >= rightChoiceCoordinate)
        {
            if(!IsRight)
            IsRight=true;
        }
        else IsRight=false;
    }

    void OnMouseUp()
    {
        image.sprite = sprite_Dropped;
        CheckAreaDropped();
    }

    

    private void CheckAreaDropped()
    {
        
        if (IsLeft)
        {

            Manager.instance.ExecuteLeftChoice();
            
        }

        if(IsRight)
        {
            Manager.instance.ExecuteRightChoice();
        }
        if (isLeft || isRight)
        {

            transform.position = startingPosition;
            Destroy(this.gameObject);
        }

    }


}
