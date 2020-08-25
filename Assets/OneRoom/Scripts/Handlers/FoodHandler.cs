using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OneRoom
{
    public class FoodHandler : MonoBehaviour
    {
        public GameObject food;

        private int health = 1;
        private bool inputEnabled = false;

        public void Load(Transform pParent)
        {
            gameObject.SetActive(false);
            transform.SetParent(pParent);
        }

        public void PlayGrow(float duration, float delay)
        {
            inputEnabled = true;

            gameObject.SetActive(true);
            food.SetActive(false);

            // Rewind to setup
            Vector3 localScale = food.transform.localScale;
            localScale.y = 0f;
            food.transform.localScale = localScale;

            // Animate
            food.transform.DOScaleY(1f, duration)
                .SetDelay(delay)
                .SetEase(Ease.OutElastic)
                .OnStart(() =>
                {
                    food.SetActive(true);
                });
        }

        public void PlayShake()
        {
            food.transform.DOShakeScale(0.1f);
        }

        public void PlayDead()
        {
            food.transform.DOShakeScale(0.1f).
                OnComplete(OnDeadShakeComplete);
        }

        private void OnDeadShakeComplete()
        {
            // Rewind to setup
            Vector3 localScale = food.transform.localScale;
            localScale.y = 1.0f;
            food.transform.localScale = localScale;

            // animate
            Vector3 rot = food.transform.rotation.eulerAngles;
            food.transform.DOScale(0f, 0.1f)
                .SetEase(Ease.InBack)
                .OnComplete(OnPlayDeadComplete);
        }

        private void OnPlayDeadComplete()
        {
            food.SetActive(false);
            gameObject.SetActive(false);
        }

        private void OnMouseDown()
        {
            if (inputEnabled)
            {
                health--;
                if (health <= 0)
                {
                    inputEnabled = false;
                    PlayDead();

                    PlayController.main.GetGridController().ReturnGridPosition(transform.localPosition);
                    PlayController.main.GetGridController().AddFoodObjectCount(-1);

                    PlayController.main.GetResourceController().UpdateFoodCount();

                    GameObjectFactory.main.ReturnFoodObject(gameObject);
                }
                else
                {
                    PlayShake();
                    PlayController.main.GetResourceController().UpdateFoodCount();
                }
            }
        }
    }
}
