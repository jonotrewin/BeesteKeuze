using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInCanvas : MonoBehaviour
{
    CanvasGroup cv;
    public float speed;
    private void Awake()
    {
        cv = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        cv.alpha = 0f;
        DOTween.To(() => cv.alpha, (x) => cv.alpha = x, 1, speed);
    }
}
