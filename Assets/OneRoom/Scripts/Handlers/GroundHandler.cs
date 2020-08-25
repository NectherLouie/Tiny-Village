using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

namespace OneRoom
{
    public class GroundHandler : MonoBehaviour
    {
        public GameObject groundQuad;
        public List<Material> groundQuadMaterials = new List<Material>();

        public GameObject buildingObject;

        public void Load(Transform pParent)
        {
            gameObject.SetActive(false);
            transform.SetParent(pParent);

            groundQuad.GetComponent<Renderer>().material = groundQuadMaterials[Random.Range(0, groundQuadMaterials.Count)];
        }

        public void PlayEnter(float duration, float delay)
        {
            gameObject.SetActive(true);
            groundQuad.transform.localScale = Vector3.one * 2f;
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
