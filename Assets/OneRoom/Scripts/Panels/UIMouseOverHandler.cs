using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIMouseOverHandler : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler
{
    public CanvasGroup target;

    private bool mouseOver = false;

    void Update()
    {
        if (mouseOver)
        {
            if (target != null)
            {
                target.DOFade(1f, 0.25f);
            }
        }
    }

    public void OnPointerEnter(PointerEventData pData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData pData)
    {
        mouseOver = false;

        if (target != null)
        {
            target.DOFade(0f, 0.25f);
        }
    }
}
