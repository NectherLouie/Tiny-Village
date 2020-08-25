using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OneRoom
{
    public class BuildingClicker : MonoBehaviour
    {
        public GameObject clickerGameObject;
        public GameObject prefabGameObject;

        public bool inputEnabled = false;

        public string buildingName = "Building Name";
        public int woodCost = 1;
        public int stoneCost = 1;

        private GameObject followObject;

        public void Load(DataResourceCost pDataResourceCost)
        {
            woodCost = pDataResourceCost.woodCost;
            stoneCost = pDataResourceCost.stoneCost;
        }

        public void UpdateEnableInput(GameData pGameData)
        {
            if (pGameData.woodCount >= woodCost && pGameData.stoneCount >= stoneCost)
            {
                inputEnabled = true;

                MaterialChanger[] materialChangers = clickerGameObject.GetComponents<MaterialChanger>();
                foreach (MaterialChanger m in materialChangers)
                {
                    m.ChangeMaterial(0);
                }
            }
            else
            {
                inputEnabled = false;

                MaterialChanger[] materialChangers = clickerGameObject.GetComponents<MaterialChanger>();
                foreach (MaterialChanger m in materialChangers)
                {
                    m.ChangeMaterial(3);
                }
            }
        }

        private void Update()
        {
            if (inputEnabled)
            {
                if (followObject != null)
                {
                    followObject.transform.position = FollowMousePosition();
                }

                UpdateMouseButtonInput();
            }
        }

        private void UpdateMouseButtonInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(followObject);

                followObject = null;

                PlayController.main.SetBuildPrefabObject(null, ref followObject, null);
            }
        }

        private void OnMouseOver()
        {
            PlayController.main.SetResourceCostPosition(transform.position + (Vector3.up * 1.5f));
            PlayController.main.ShowResourceCostPanel(buildingName, woodCost, stoneCost);

            if (inputEnabled)
            {
                clickerGameObject.transform.DOLocalMoveY(0.25f, 0.15f);
            }
        }

        private void OnMouseExit()
        {
            PlayController.main.HideResourceCostPanel();

            if (inputEnabled)
            {
                clickerGameObject.transform.DOLocalMoveY(0f, 0.15f);
            }
        }

        private void OnMouseDown()
        {
            if (inputEnabled)
            {
                PlayController.main.DestroyFollowObject();

                followObject = Instantiate(prefabGameObject);
                followObject.transform.position = transform.position;

                DataResourceCost drc = new DataResourceCost();
                drc.woodCost = woodCost;
                drc.stoneCost = stoneCost;
                PlayController.main.SetBuildPrefabObject(prefabGameObject, ref followObject, drc);

                MaterialChanger[] materialChangers = followObject.GetComponents<MaterialChanger>();
                foreach (MaterialChanger m in materialChangers)
                {
                    m.ChangeMaterial(1);
                }
            }
        }

        Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;

            mousePoint.z = Camera.main.WorldToScreenPoint(followObject.transform.position).z;

            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        private Vector3 FollowMousePosition()
        {
            Vector3 pos = GetMouseWorldPos();

            pos.y = 0f;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                Debug.Log(objectHit.name + ": " + hit.point.ToString());

                pos.y = hit.point.y;
            }

            return pos;
        }
    }
}
