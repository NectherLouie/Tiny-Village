using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MainMenuPanel : MonoBehaviour
{
    public event Action OnPlayButtonClicked;

    public List<GameObject> playPanels = new List<GameObject>();
    public List<GameObject> inputBlockers = new List<GameObject>();

    public void ClickPlayButton()
    {
        foreach(GameObject g in playPanels)
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

        OnPlayButtonClicked?.Invoke();
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void Show(float duration, float delay)
    {
        gameObject.SetActive(true);

        foreach (GameObject g in inputBlockers)
        {
            g.SetActive(true);
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

        foreach (GameObject g in inputBlockers)
        {
            g.SetActive(false);
        }

        canvasGroup.DOFade(0f, 0.25f)
            .OnComplete(OnHideComplete);
    }

    private void OnHideComplete()
    {
        gameObject.SetActive(false);
    }
}
