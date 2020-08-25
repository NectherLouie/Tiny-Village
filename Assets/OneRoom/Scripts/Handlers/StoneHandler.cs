using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OneRoom
{
    public class StoneHandler : MonoBehaviour
    {
        public GameObject stone;

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
            stone.SetActive(false);

            // Rewind to setup
            Vector3 localScale = stone.transform.localScale;
            localScale.y = 0f;
            stone.transform.localScale = localScale;

            // Animate
            stone.transform.DOScaleY(1f, duration)
                .SetDelay(delay)
                .SetEase(Ease.OutElastic)
                .OnStart(() =>
                {
                    stone.SetActive(true);
                });
        }

        public void PlayShake()
        {
            stone.transform.DOShakeScale(0.25f, 0.5f);
        }

        public void PlayDead()
        {
            stone.transform.DOShakeScale(0.25f, 0.5f).
                OnComplete(OnDeadShakeComplete);
        }

        private void OnDeadShakeComplete()
        {
            // Rewind to setup
            Vector3 localScale = stone.transform.localScale;
            localScale.y = 1.0f;
            stone.transform.localScale = localScale;

            // animate
            Vector3 rot = stone.transform.rotation.eulerAngles;
            stone.transform.DOScale(0f, 0.1f)
                .SetEase(Ease.InBack)
                .OnComplete(OnPlayDeadComplete);
        }

        private void OnPlayDeadComplete()
        {
            stone.SetActive(false);
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
                    PlayController.main.GetGridController().AddStoneObjectCount(-1);

                    PlayController.main.GetResourceController().UpdateStoneCount();

                    GameObjectFactory.main.ReturnStoneObject(gameObject);
                }
                else
                {
                    PlayShake();
                    PlayController.main.GetResourceController().UpdateStoneCount();
                }
            }
        }
    }
}
