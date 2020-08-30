using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using Blocks;
using UnityEngine.XR.WSA.Input;

namespace OneRoom
{
    public class GroundHandler : MonoBehaviour
    {
        public GameObject groundQuad;
        public List<Material> groundQuadMaterials = new List<Material>();

        public GameObject buildingObject;

        private float localY = 0;

        public void Load(Transform pParent)
        {
            gameObject.SetActive(true);
            transform.SetParent(pParent);

            groundQuad.SetActive(false);
            
        }

        public void PlayEnter(float pPosY, int pIndex, float pDuration, float pDelay)
        {
            groundQuad.SetActive(true);
            groundQuad.GetComponent<Renderer>().material = groundQuadMaterials[pIndex];

            Vector3 pos = transform.position;
            pos.y = -5f;
            transform.position = pos;

            transform.DOMoveY(pPosY, pDuration)
                .SetDelay(pDelay)
                .SetEase(Ease.OutBack);

            Vector3 scl = transform.localScale;
            float targetScaleY = scl.y;
            scl.y = 0;
            transform.localScale = scl;

            transform.DOScaleY(targetScaleY, pDuration + 0.25f)
                .SetDelay(pDelay)
                .SetEase(Ease.OutBack);
        }

        public void PlayExit(float duration, float delay)
        {
            groundQuad.SetActive(false);
        }

        private void OnMouseDown()
        {
            GameObject buildPrefabObject = PlayController.main.GetBuildPrefabObject();

            if (buildPrefabObject != null)
            {
                // TODO: Try a building pool
                // buildingObject = PlayController.main.FetchBuilding();
                buildingObject = Instantiate(buildPrefabObject);
                buildingObject.transform.SetParent(transform);
                buildingObject.transform.localPosition = Vector3.zero;

                PlayController.main.HandleBuildingSpawned(buildingObject);

                PlayController.main.GetGridController().SliceGridPosition(transform.localPosition);

                PlayController.main.UpdateResourceCosts(buildingObject);

                PlayController.main.DestroyFollowObject();

                List<GameObject> grounds = PlayController.main.GetTerrainHandler().GetGrounds();

                // play anims
                float duration = 0.05f;
                float delay = 0.15f;
                PlayBuildAnimation(duration, 0f);

                foreach (GameObject g in grounds)
                {
                    float _x = transform.localPosition.x;
                    float _z = transform.localPosition.z;
                    float space = 2f;
                    
                    if (g.transform.localPosition.x == _x && g.transform.localPosition.z == _z + space)
                    {
                        GroundHandler gh = g.GetComponent<GroundHandler>();
                        gh.PlayBuildAnimation(duration, delay);
                    }

                    if (g.transform.localPosition.x == _x && g.transform.localPosition.z == _z - space)
                    {
                        GroundHandler gh = g.GetComponent<GroundHandler>();
                        gh.PlayBuildAnimation(duration, delay);
                    }

                    if (g.transform.localPosition.x == _x + space && g.transform.localPosition.z == _z)
                    {
                        GroundHandler gh = g.GetComponent<GroundHandler>();
                        gh.PlayBuildAnimation(duration, delay);
                    }

                    if (g.transform.localPosition.x == _x - space && g.transform.localPosition.z == _z)
                    {
                        GroundHandler gh = g.GetComponent<GroundHandler>();
                        gh.PlayBuildAnimation(duration, delay);
                    }
                }
            }
        }

        private void PlayBuildAnimation(float pDuration, float pDelay)
        {
            localY = transform.localPosition.y;
            transform.DOLocalMoveY(localY - 0.15f, pDuration)
                .SetDelay(pDelay)
                .OnComplete(OnMoveComplete);
        }

        private void OnMoveComplete()
        {
            transform.DOLocalMoveY(localY, 0.5f)
                .SetEase(Ease.OutElastic);
        }
    }
}
