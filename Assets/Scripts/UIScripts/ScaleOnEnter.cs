using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScaleOnEnter : MonoBehaviour
{
    Vector3 startingScale;
    [SerializeField]public float slowdown = 4f;
    public UnityEvent OnScaleComplete;

    private void Awake()
    {
        startingScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(startingScale, slowdown).OnComplete(() =>{
            Manager.instance.ActivateText(true);
        });
    }
}
