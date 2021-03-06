﻿using Blocks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class BuildingsHandler : MonoBehaviour
    {
        private List<GameObject> buildings = new List<GameObject>();

        public void Load()
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                GameObject g = transform.GetChild(i).gameObject;
                g.SetActive(false);

                buildings.Add(g);
            }
        }

        public void PlayEnter()
        {
            foreach (GameObject g in buildings)
            {
                //g.SetActive(true);

                Vector3 scl = g.transform.localScale;
                scl.y = 0f;
                g.transform.localScale = scl;

                g.transform.DOScale(Vector3.one, 1.0f)
                    .SetDelay(1.5f + Utils.RandomRange(0f, 0.5f))
                    .SetEase(Ease.OutElastic)
                    .OnStart(() =>
                    {
                        g.SetActive(true);
                    });
            }
        }
    }
}
