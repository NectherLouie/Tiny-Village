using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OneRoom
{
    public class TreeHandler : MonoBehaviour
    {
        public GameObject tree;

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
            tree.SetActive(false);

            // Rewind to setup
            Vector3 localScale = tree.transform.localScale;
            localScale.y = 0f;
            tree.transform.localScale = localScale;

            // Animate
            tree.transform.DOScaleY(2f, duration)
                .SetDelay(delay)
                .SetEase(Ease.OutElastic)
                .OnStart(() =>
                {
                    tree.SetActive(true);
                });
        }

        public void PlayShake()
        {
            tree.transform.DOShakeRotation(0.5f, 5f);
        }

        public void PlayDead()
        {
            // Rewind to setup
            Quaternion rotation = tree.transform.rotation;
            Vector3 eulerAngles = rotation.eulerAngles;
            eulerAngles.z = 0f;
            rotation.eulerAngles = eulerAngles;
            tree.transform.rotation = rotation;

            // animate
            Vector3 rot = tree.transform.rotation.eulerAngles;
            tree.transform.DORotate(new Vector3(rot.x, rot.y, 120f), 0.5f)
                .SetEase(Ease.InBack)
                .OnStart(() => {
                    tree.SetActive(true);
                })
                .OnComplete(OnPlayDeadComplete);
        }

        private void OnPlayDeadComplete()
        {
            tree.SetActive(false);
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
                    PlayController.main.GetGridController().AddTreeObjectCount(-1);

                    PlayController.main.GetResourceController().UpdateWoodCount();
                    
                    GameObjectFactory.main.ReturnTreeObject(gameObject);
                }
                else
                {
                    PlayShake();
                }
            }
        }
    }
}
