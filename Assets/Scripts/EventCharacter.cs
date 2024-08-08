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
    [SerializeField] Sprite sprite_Carried;
    [SerializeField] Sprite sprite_Dropped;
    SpriteRenderer image;

    Vector2 startingPosition;

    float leftChoiceCoordinate = Screen.width / 3;
    float rightChoiceCoordinate = Screen.width - (Screen.width / 3);

    Vector3 convertedChoiceCoordinatesLeft;
    Vector3 convertedChoiceCoordinatesRight;

    [SerializeField] int distanceFromCamera = 15;



    bool isLeft;
    protected bool IsLeft
    {
        get { return isLeft; }
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
        gameObject.AddComponent<ScaleOnEnter>();

    }

    void OnMouseDown()
    {
        if (!Manager.instance.CanInteract) return;
        image.sprite = sprite_Carried;

    }

    void OnMouseDrag()
    {

        if (!Manager.instance.CanInteract) return;

        var mousePos = Input.mousePosition;
        mousePos.z = distanceFromCamera;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        if (Camera.main.WorldToScreenPoint(transform.position).x <= leftChoiceCoordinate)
        {
            if (!IsLeft)
                IsLeft = true;
        }
        else IsLeft = false;

        if (Camera.main.WorldToScreenPoint(transform.position).x >= rightChoiceCoordinate)
        {
            if (!IsRight)
                IsRight = true;
        }
        else IsRight = false;
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

        if (IsRight)
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
