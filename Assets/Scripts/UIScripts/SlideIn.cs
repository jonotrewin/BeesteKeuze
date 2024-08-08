using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideIn : MonoBehaviour
{
    public enum direction { left, right }

    Vector3 startingPos;

    [SerializeField] SlideIn.direction dir;

    [SerializeField] float amountOffscreen = 20;

    [SerializeField] float speed = 0.1f;

    // Start is called before the first frame update
    void Awake()
    {
        startingPos = transform.position;

    }

    private void OnEnable()
    {

        switch (dir)
        {
            case direction.left:
                transform.position += amountOffscreen * Vector3.left;
                break;
            case direction.right:
                transform.position += amountOffscreen * Vector3.right;
                break;

        }

        transform.DOMove(startingPos, speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
