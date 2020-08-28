using Blocks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneRoom
{
    public class PlayController : MonoBehaviour
    {
        public static PlayController main;

        public event Action<GameData> OnDayStartedEvent;
        public event Action<GameData> OnGameOverEvent;

        public GameData gameData;

        [Header("Houses")]
        public GameObject houseSmallClickerObject;
        public GameObject houseMediumClickerObject;
        public GameObject houseLargeClickerObject;

        [Header("Resources")]
        public GameObject foresterClickerObject;
        public GameObject quarryClickerObject;
        public GameObject cemeteryClickerObject;

        [Header("Satisfaction")]
        public GameObject farmClickerObject;
        public GameObject clinicClickerObject;
        public GameObject tavernClickerObject;

        [Header("Canvas Panels")]
        public GameObject villageDayPanelObject;
        public GameObject mainMenuPanelObject;
        public GameObject resourceCostPanelObject;
        public GameObject unitActionPanelObject;

        [Header("Private")]
        private GameObject buildFollowObject;

        private DataResourceCost buildResourceCost = new DataResourceCost();

        private ResourceController resourceController;
        private GridController gridController;
        private DayNightController dayNightController;

        public void Load()
        {
            main = this;

            GameObjectFactory gameObjectFactory = GameObjectFactory.main;

            houseSmallClickerObject = gameObjectFactory.houseSmallClickerObject;
            houseMediumClickerObject = gameObjectFactory.houseMediumClickerObject;
            houseLargeClickerObject = gameObjectFactory.houseLargeClickerObject;
            foresterClickerObject = gameObjectFactory.foresterClickerObject;
            quarryClickerObject = gameObjectFactory.quarryClickerObject;
            cemeteryClickerObject = gameObjectFactory.cemeteryClickerObject;
            farmClickerObject = gameObjectFactory.farmClickerObject;
            clinicClickerObject = gameObjectFactory.clinicClickerObject;
            tavernClickerObject = gameObjectFactory.tavernClickerObject;
            villageDayPanelObject = gameObjectFactory.villageDayPanelObject;
            mainMenuPanelObject = gameObjectFactory.mainMenuPanelObject;
            resourceCostPanelObject = gameObjectFactory.resourceCostPanelObject;
            unitActionPanelObject = gameObjectFactory.unitActionPanelObject;

            resourceController = gameObject.GetComponent<ResourceController>();
            resourceController.Load(ref gameData);

            gridController = gameObject.GetComponent<GridController>();
            gridController.Load(ref gameData);

            dayNightController = gameObject.GetComponent<DayNightController>();
            dayNightController.Load(ref gameData);
        }

        private void Update()
        {
            EnableClickers();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //ResetScene();
            }

            if (Input.GetMouseButtonDown(0))
            {
                //RaycastHit hit;
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //if (Physics.Raycast(ray, out hit))
                //{
                //    Transform objectHit = hit.transform;

                //    Debug.Log(objectHit.name + ": " + hit.point.ToString());

                //    // Do something with the object that was hit by the raycast.
                //}
            }
        }

        public void ResetScene()
        {
            DOTween.Clear();

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void EnableClickers()
        {
            // houses
            BuildingClicker houseSmallClicker = houseSmallClickerObject.GetComponent<BuildingClicker>();
            houseSmallClicker.UpdateEnableInput(gameData);

            BuildingClicker houseMediumClicker = houseMediumClickerObject.GetComponent<BuildingClicker>();
            houseMediumClicker.UpdateEnableInput(gameData);

            BuildingClicker houseLargeClicker = houseLargeClickerObject.GetComponent<BuildingClicker>();
            houseLargeClicker.UpdateEnableInput(gameData);

            // resources
            BuildingClicker farmClicker = farmClickerObject.GetComponent<BuildingClicker>();
            farmClicker.UpdateEnableInput(gameData);

            BuildingClicker foresterClicker = foresterClickerObject.GetComponent<BuildingClicker>();
            foresterClicker.UpdateEnableInput(gameData);

            BuildingClicker quarryClicker = quarryClickerObject.GetComponent<BuildingClicker>();
            quarryClicker.UpdateEnableInput(gameData);

            // satisfaction
            BuildingClicker cemeteryClicker = cemeteryClickerObject.GetComponent<BuildingClicker>();
            cemeteryClicker.UpdateEnableInput(gameData);

            BuildingClicker clinicClicker = clinicClickerObject.GetComponent<BuildingClicker>();
            clinicClicker.UpdateEnableInput(gameData);

            BuildingClicker tavernClicker = tavernClickerObject.GetComponent<BuildingClicker>();
            tavernClicker.UpdateEnableInput(gameData);
        }

        public void ResetGameData()
        {
            gameData.Reset();
        }

        public void ShowMainMenu()
        {
            MainMenuPanel mainMenuPanel = mainMenuPanelObject.GetComponent<MainMenuPanel>();
            mainMenuPanel.Show(0.25f, 1.75f);
        }

        public void WaitForMainMenuPlayClick(Action pOnMainMenuPlayClicked)
        {
            MainMenuPanel mainMenuPanel = mainMenuPanelObject.GetComponent<MainMenuPanel>();
            mainMenuPanel.OnPlayButtonClicked -= pOnMainMenuPlayClicked;
            mainMenuPanel.OnPlayButtonClicked += pOnMainMenuPlayClicked;
        }

        public void ShowResourceCostPanel(string pBuildingName, int pWoodCost, int pStoneCost)
        {
            ResourceCostPanel resourceCostPanel = resourceCostPanelObject.GetComponent<ResourceCostPanel>();
            resourceCostPanel.Show(pBuildingName, pWoodCost, pStoneCost);
        }

        public void HideResourceCostPanel()
        {
            ResourceCostPanel resourceCostPanel = resourceCostPanelObject.GetComponent<ResourceCostPanel>();
            resourceCostPanel.Hide();
        }

        public void SetResourceCostPosition(Vector3 pPos)
        {
            ResourceCostPanel resourceCostPanel = resourceCostPanelObject.GetComponent<ResourceCostPanel>();
            resourceCostPanel.SetPosition(pPos);
        }

        public void ShowUnitActionPanel<T>(T pHandler)
        {
            UnitActionPanel unitActionPanel = unitActionPanelObject.GetComponent<UnitActionPanel>();
            unitActionPanel.Show<T>(pHandler);
        }

        public void HideUnitActionPanel()
        {
            UnitActionPanel unitActionPanel = unitActionPanelObject.GetComponent<UnitActionPanel>();
            unitActionPanel.Hide();
        }

        public GridController GetGridController()
        {
            return gridController;
        }

        public bool CanGrowTree()
        {
            return gameData.dayCount % gameData.moduloTreeObjectCount == 0;
        }

        public bool CanGrowStone()
        {
            return gameData.dayCount % gameData.moduloStoneObjectCount == 0;
        }

        public bool CanGrowFood()
        {
            return gameData.dayCount % gameData.moduloFoodObjectCount == 0;
        }

        public void StartDay()
        {
            GameEventsPanel gameEventsPanel = villageDayPanelObject.GetComponent<GameEventsPanel>();
            gameEventsPanel.StartDay();

            dayNightController.OnDayStarted += OnDayStarted;
            dayNightController.OnDayComplete += OnDayComplete;
            dayNightController.StartDay();
        }

        public DayNightController GetDayNightController()
        {
            return dayNightController;
        }

        private void OnDayStarted()
        {
            gameData.dayCount++;

            // food
            resourceController.FeedPopulation();

            OnDayStartedEvent?.Invoke(gameData);
        }

        private void OnDayComplete()
        {
            if (!gameData.hasTriggeredGameOver)
            {
                // food
                resourceController.CollectFoodProduce();

                // wood
                gameData.woodIncrement = gameData.woodProduce;
                resourceController.AddWood();

                // stone
                gameData.stoneIncrement = gameData.stoneProduce;
                resourceController.AddStone();

                // potion
                gameData.potionIncrement = gameData.potionProduce;
                resourceController.AddPotion();

                // booze
                gameData.boozeIncrement = gameData.boozeProduce;
                resourceController.AddBooze();
            }
            else
            {
                OnGameOverEvent?.Invoke(gameData);
            }
        }

        public void SetBuildPrefabObject(GameObject pBuildPrefabObject, ref GameObject pFollowObject, DataResourceCost pDataResourceCost)
        {
            gameData.buildPrefabObject = pBuildPrefabObject;

            buildFollowObject = pFollowObject;

            buildResourceCost = pDataResourceCost;
        }

        public GameObject GetBuildPrefabObject()
        {
            return gameData.buildPrefabObject;
        }

        public void DestroyFollowObject()
        {
            Destroy(buildFollowObject);

            buildFollowObject = null;

            PlayController.main.SetBuildPrefabObject(null, ref buildFollowObject, null);
        }

        public void HandleBuildingSpawned(GameObject pBuildingObject)
        {
            // houses
            HouseHandler houseHandler = pBuildingObject.GetComponent<HouseHandler>();
            if (houseHandler != null)
            {
                DataHouseHandler dataHouseHandler = houseHandler.dataHouseHandler;
                switch (dataHouseHandler.houseType)
                {
                    case DataHouseHandler.eHouseType.SMALL:
                        dataHouseHandler.additionalPopulation = gameData.houseSmallData.additionalPopulation;
                        break;
                    case DataHouseHandler.eHouseType.MEDIUM:
                        dataHouseHandler.additionalPopulation = gameData.houseMediumData.additionalPopulation;
                        break;
                    case DataHouseHandler.eHouseType.LARGE:
                        dataHouseHandler.additionalPopulation = gameData.houseLargeData.additionalPopulation;
                        break;
                }

                houseHandler.EnableBoxCollider();

                resourceController.UpdateMaxPopulationIncrease(dataHouseHandler.houseType);
            }

            FarmHandler farmHandler = pBuildingObject.GetComponent<FarmHandler>();
            if (farmHandler != null)
            {
                farmHandler.EnableBoxCollider();
                resourceController.UpdateMaxFarmingPopulation();
            }

            WoodcutterHandler woodcutterHandler = pBuildingObject.GetComponent<WoodcutterHandler>();
            if (woodcutterHandler != null)
            {
                woodcutterHandler.EnableBoxCollider();
                resourceController.UpdateMaxWoodcutterPopulation();
            }

            QuarryHandler quarryHandler = pBuildingObject.GetComponent<QuarryHandler>();
            if (quarryHandler != null)
            {
                quarryHandler.EnableBoxCollider();
                resourceController.UpdateMaxMinerPopulation();
            }

            ApothecaryHandler apothecaryHandler = pBuildingObject.GetComponent<ApothecaryHandler>();
            if (apothecaryHandler != null)
            {
                apothecaryHandler.EnableBoxCollider();
                resourceController.UpdateMaxApothecaryPopulation();
            }

            TavernHandler tavernHandler = pBuildingObject.GetComponent<TavernHandler>();
            if (tavernHandler != null)
            {
                tavernHandler.EnableBoxCollider();
                resourceController.UpdateMaxBarkeepPopulation();
            }
        }

        public void RemoveBuilding(GameObject pGroundObject)
        {
            gridController.ReturnGridPosition(pGroundObject.transform.localPosition);

            GameObject buildingObject = pGroundObject.GetComponent<GroundHandler>().buildingObject;
            
            Destroy(buildingObject);
        }

        // resource
        public ResourceController GetResourceController()
        {
            return resourceController;
        }

        public void UpdateResourceCosts(GameObject pBuildingObject)
        {
            gameData.woodIncrement = -buildResourceCost.woodCost;
            resourceController.AddWood();

            gameData.stoneIncrement = -buildResourceCost.stoneCost;
            resourceController.AddStone();
        }
    }
}
