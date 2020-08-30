using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace OneRoom
{
    public class UnitActionPanel : MonoBehaviour
    {
        public RectTransform rootPanel;
        public Button openButton;
        public Button closeButton;

        public float targetClosePosition = 0;
        public float targetOpenPosition = 0;

        public void Show<T>(T pHandler)
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {

        }

        public void ClickOpenArrow()
        {
            Vector2 r = rootPanel.anchoredPosition;
            r.y = targetClosePosition;
            rootPanel.anchoredPosition = r;

            rootPanel.DOAnchorPosY(targetOpenPosition, 0.25f)
                .OnStart(OpenButtonComplete);
        }

        private void OpenButtonComplete()
        {
            CanvasGroup canvasGroup = openButton.gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;

            canvasGroup.DOFade(0f, 0.25f);

            closeButton.gameObject.SetActive(true);
        }

        public void ClickCloseArrow()
        {
            Vector2 r = rootPanel.anchoredPosition;
            r.y = targetOpenPosition;
            rootPanel.anchoredPosition = r;

            rootPanel.DOAnchorPosY(targetClosePosition, 0.25f)
                .OnComplete(CloseButtonComplete);
        }

        private void CloseButtonComplete()
        {
            CanvasGroup canvasGroup = openButton.gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;

            canvasGroup.DOFade(1f, 0.15f);

            closeButton.gameObject.SetActive(false);
        }
    }
}
