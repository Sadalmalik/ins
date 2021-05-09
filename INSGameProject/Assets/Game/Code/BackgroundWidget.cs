using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundWidget : MonoBehaviour
{
    public Image firstRenderer;
    public Image secondRenderer;
    public float duration = 0.6f;
    
    private Tween tween;
    private bool switchInProgress = false;
    
    public event Action OnBackgroundChanged;
    
    public void SetBackground(Sprite background)
    {
        if (switchInProgress)
            tween.Kill();
        
        switchInProgress = true;
        secondRenderer.sprite = background;
        var sequence = DOTween.Sequence();
        sequence
            .Append(firstRenderer.DOFade(0, duration))
            .Join(secondRenderer.DOFade(1, duration))
            .AppendCallback(()=>
            {
                OnBackgroundChanged?.Invoke();
                (firstRenderer, secondRenderer) = (secondRenderer, firstRenderer);
                switchInProgress = false;
            });
        tween = sequence;
    }
    
    public IEnumerator WaitForChange()
    {
        while (switchInProgress)
            yield return null;
    }
}
