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

        public void Load(Transform pParent)
        {
            gameObject.SetActive(true);
            transform.SetParent(pParent);

            groundQuad.SetActive(false);
            
        }

        public void PlayEnter(float pPosY, int pIndex)
        {
            groundQuad.SetActive(true);
            groundQuad.GetComponent<Renderer>().material = groundQuadMaterials[pIndex];

            Vector3 pos = transform.position;
            pos.y = pPosY;
            transform.position = pos;
        }

        public void PlayExit(float duration, float delay)
        {
            groundQuad.SetActive(false);
        }

        private void OnMouseEnter()
        {
            Vector3 pos = groundQuad.transform.position;

        }


        private void OnMouseExit()
        {

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
            }
        }
    }
}
