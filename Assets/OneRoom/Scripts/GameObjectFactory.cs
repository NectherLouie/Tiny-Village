using OneRoom;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class GameObjectFactory : MonoBehaviour
    {
        public static GameObjectFactory main;

        public event Action OnLoadComplete;

        [Header("Prefabs")]
        public GameObject prefabPlayController;
        public GameObject prefabResultController;
        public GameObject prefabTree;
        public GameObject prefabStone;
        public GameObject prefabFood;

        [Header("Terrains")]
        public GameObject prefabTerrain;

        [Header("Preloads")]
        public GameObject buildingsHandlerObject;

        [Header("Houses")]
        public GameObject houseSmallClickerObject;
        public GameObject houseMediumClickerObject;
        public GameObject houseLargeClickerObject;

        [Header("Resources")]
        public GameObject farmClickerObject;
        public GameObject foresterClickerObject;
        public GameObject quarryClickerObject;

        [Header("Satisfaction")]
        public GameObject cemeteryClickerObject;
        public GameObject clinicClickerObject;
        public GameObject tavernClickerObject;

        [Header("Canvas Panels")]
        public GameObject villageDayPanelObject;
        public GameObject mainMenuPanelObject;
        public GameObject gameOverPanelObject;
        public GameObject congratsPanelObject;
        public GameObject resourceCostPanelObject;
        public GameObject unitActionPanelObject;

        private Queue<GameObject> treeObjectPool = new Queue<GameObject>();
        private Queue<GameObject> stoneObjectPool = new Queue<GameObject>();
        private Queue<GameObject> foodObjectPool = new Queue<GameObject>();

        private void Awake()
        {
            main = this;
        }

        public void Load()
        {
            Transform pParent = transform;

            // Prefabs
            for (int i = 0; i < 100; ++i)
            {
                // tree
                GameObject goTree = Instantiate(prefabTree);
                TreeHandler treeHandler = goTree.GetComponent<TreeHandler>();
                treeHandler.Load(pParent);
                treeObjectPool.Enqueue(goTree);

                // stone
                GameObject goStone = Instantiate(prefabStone);
                StoneHandler stoneHandler = goStone.GetComponent<StoneHandler>();
                stoneHandler.Load(pParent);
                stoneObjectPool.Enqueue(goStone);

                // food
                GameObject goFood = Instantiate(prefabFood);
                FoodHandler foodHandler = goFood.GetComponent<FoodHandler>();
                foodHandler.Load(pParent);
                foodObjectPool.Enqueue(goFood);
            }

            // Preloads
            PlayController pc = PlayController.main;

            BuildingsHandler buildingsHandler = buildingsHandlerObject.GetComponent<BuildingsHandler>();
            buildingsHandler.Load();

            // houses
            BuildingClicker houseClicker = houseSmallClickerObject.GetComponent<BuildingClicker>();
            houseClicker.Load(pc.gameData.houseSmallResourceCost);

            houseClicker = houseMediumClickerObject.GetComponent<BuildingClicker>();
            houseClicker.Load(pc.gameData.houseMediumResourceCost);

            houseClicker = houseLargeClickerObject.GetComponent<BuildingClicker>();
            houseClicker.Load(pc.gameData.houseLargeResourceCost);

            // resources
            BuildingClicker satisfactionClicker = farmClickerObject.GetComponent<BuildingClicker>();
            satisfactionClicker.Load(pc.gameData.farmResourceCost);

            BuildingClicker resourceClicker = foresterClickerObject.GetComponent<BuildingClicker>();
            resourceClicker.Load(pc.gameData.foresterResourceCost);

            resourceClicker = quarryClickerObject.GetComponent<BuildingClicker>();
            resourceClicker.Load(pc.gameData.quarryResourceCost);

            // satisfaction
            resourceClicker = cemeteryClickerObject.GetComponent<BuildingClicker>();
            resourceClicker.Load(pc.gameData.cemeteryResourceCost);

            satisfactionClicker = clinicClickerObject.GetComponent<BuildingClicker>();
            satisfactionClicker.Load(pc.gameData.clinicResourceCost);

            satisfactionClicker = tavernClickerObject.GetComponent<BuildingClicker>();
            satisfactionClicker.Load(pc.gameData.tavernResourceCost);

            OnLoadComplete?.Invoke();
        }

        public GameObject FetchTreeObject()
        {
            GameObject g = treeObjectPool.Dequeue();

            return g;
        }

        public void ReturnTreeObject(GameObject pTreeObject)
        {
            treeObjectPool.Enqueue(pTreeObject);
        }

        public GameObject FetchStoneObject()
        {
            GameObject g = stoneObjectPool.Dequeue();

            return g;
        }

        public void ReturnStoneObject(GameObject pStoneObject)
        {
            stoneObjectPool.Enqueue(pStoneObject);
        }

        public GameObject FetchFoodObject()
        {
            GameObject g = foodObjectPool.Dequeue();

            return g;
        }

        public void ReturnFoodObject(GameObject pStoneObject)
        {
            foodObjectPool.Enqueue(pStoneObject);
        }
    }
}
