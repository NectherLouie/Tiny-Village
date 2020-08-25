using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GameOverPanel : MonoBehaviour
{
    public event Action OnMainMenuButtonClicked;

    public List<GameObject> mainMenuPanels = new List<GameObject>();
    public List<GameObject> playPanels = new List<GameObject>();
    public List<GameObject> inputBlockers = new List<GameObject>();

    public void ClickMainMenuButton()
    {
        foreach(GameObject g in mainMenuPanels)
        {
            g.SetActive(true);

            CanvasGroup canvasGroup = g.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;

            canvasGroup.DOFade(1f, 0.25f);
        }

        foreach (GameObject g in inputBlockers)
        {
            g.SetActive(false);
        }

        Hide();

        OnMainMenuButtonClicked?.Invoke();
    }

    public void Show(float duration, float delay)
    {
        gameObject.SetActive(true);

        foreach (GameObject g in inputBlockers)
        {
            g.SetActive(true);
        }

        foreach (GameObject g in playPanels)
        {
            g.SetActive(true);

            CanvasGroup playCanvasGroup = g.GetComponent<CanvasGroup>();
            playCanvasGroup.alpha = 1;

            playCanvasGroup.DOFade(0f, duration);
        }

        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        canvasGroup.DOFade(1f, duration)
            .SetDelay(delay);
    }

    public void Hide()
    {
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;

        canvasGroup.DOFade(0f, 0.25f)
            .OnComplete(OnHideComplete);
    }

    private void OnHideComplete()
    {
        gameObject.SetActive(false);
    }
}
