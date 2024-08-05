using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EventSystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class EventCharacter : MonoBehaviour
{
    [SerializeField]Sprite sprite_Carried;
    [SerializeField] Sprite sprite_Dropped;
    SpriteRenderer image;

    Vector2 startingPosition;

    float leftChoiceCoordinate = Screen.width / 3;
    float rightChoiceCoordinate = Screen.width - (Screen.width / 3);

    Vector3 convertedChoiceCoordinatesLeft;
    Vector3 convertedChoiceCoordinatesRight;

  

    bool isLeft;
    protected bool IsLeft
    {
        get{ return isLeft; }
        set 
        { 
            isLeft = value;
            Manager.instance.ShowLeftText(value);
        }
    }

    bool isRight;
    protected bool IsRight
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

        convertedChoiceCoordinatesLeft = Camera.main.ScreenToWorldPoint(new Vector3(leftChoiceCoordinate, 0, 0));
        convertedChoiceCoordinatesRight = Camera.main.ScreenToWorldPoint(new Vector3(rightChoiceCoordinate, 0, 0));


    }
    
    void OnMouseDown()
    {
        image.sprite = sprite_Carried;
  
    }

    void OnMouseDrag()
    {
 
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + 2*Vector3.forward;
        if(transform.position.x <= convertedChoiceCoordinatesLeft.x)
        {
            if(!IsLeft)
            IsLeft=true;
        }
        else IsLeft=false;
        
        if (transform.position.x >= convertedChoiceCoordinatesRight.x)
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

    

    protected virtual void CheckAreaDropped()
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


            Destroy(this.gameObject);
        }
        else transform.position = Vector3.zero;

    }


}
