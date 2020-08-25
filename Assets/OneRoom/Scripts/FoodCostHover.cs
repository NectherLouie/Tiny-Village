using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

namespace OneRoom
{
    public class FoodCostHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public CanvasGroup target;
        public TMP_Text foodCostText;
        private bool mouseOver = false;

        private void Update()
        {
            PlayController pc = PlayController.main;
            if (pc != null)
            {
                if (mouseOver)
                {
                    if (target != null)
                    {
                        foodCostText.text = pc.GetResourceController().GetVillagerFoodCost().ToString();
                        target.DOFade(1f, 0.25f)
                            .OnStart(() =>
                            {
                                target.gameObject.SetActive(true);
                            });
                    }
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
                target.DOFade(0f, 0.25f)
                    .OnComplete(() =>
                    {
                        target.gameObject.SetActive(false);
                    });
            }
        }
    }
}
