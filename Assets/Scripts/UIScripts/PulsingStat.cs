using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingStat : MonoBehaviour
{
    [SerializeField]public Manager.Stats stat;

    [SerializeField] float pulseSizeModifier = 1;
    [SerializeField] float pulseSpeedModifier = 1;
    [SerializeField] float ImpactSizeDifference = 1;

    Vector3 startingScale;

    private void Start()
    {
        startingScale = transform.localScale;
    }

    public void Pulse(Manager.Impact impact)
    {
       // transform.DOScale(pulseSizeModifier + (((int)impact + 1)*ImpactSizeDifference), pulseSpeedModifier).SetLoops(-1,LoopType.Yoyo);
        transform.DOShakePosition(100f, (int)impact + 1);
        
    }   

    public void StopPulse()
    {
        transform.DOKill();
        transform.localScale = startingScale;
    }
}
