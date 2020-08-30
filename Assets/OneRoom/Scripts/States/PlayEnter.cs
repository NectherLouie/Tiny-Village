using Blocks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class PlayEnter : MonoBehaviour
    {
        public event Action<GameData> OnComplete;
        public GameObjectFactory gameObjectFactory;

        public void Enter()
        {
            PlayController.main.OnDayStartedEvent -= OnDayStarted;
            PlayController.main.OnDayStartedEvent += OnDayStarted;
            PlayController.main.EnableClickers();

            PlayBuildingsEnter();

            PlayGridEnter();

            GridController gridController = PlayController.main.GetGridController();

            // Tree
            int treeCount = gridController.GetTreeObjectMaxCount();
            for (int i = 0; i < treeCount; ++i)
            {
                Vector3 treePosition = gridController.PickRandomGridPosition();
                gridController.SliceGridPosition(treePosition);

                GameObject goTree = gameObjectFactory.FetchTreeObject();
                goTree.transform.localPosition = treePosition;
                TreeHandler treeHandler = goTree.GetComponent<TreeHandler>();
                treeHandler.PlayGrow(1.0f, Utils.RandomRange(1.0f, 1.5f));

                gridController.AddTreeObjectCount(1);
            }

            // Stone
            int stoneCount = gridController.GetStoneObjectMaxCount();
            for (int i = 0; i < stoneCount; ++i)
            {
                Vector3 stonePosition = gridController.PickRandomGridPosition();
                gridController.SliceGridPosition(stonePosition);

                GameObject goStone = gameObjectFactory.FetchStoneObject();
                goStone.transform.localPosition = stonePosition;
                StoneHandler stoneHandler = goStone.GetComponent<StoneHandler>();
                stoneHandler.PlayGrow(1.0f, Utils.RandomRange(1.0f, 1.5f));

                gridController.AddStoneObjectCount(1);
            }

            // Food
            int foodCount = gridController.GetFoodObjectMaxCount();
            for (int i = 0; i < foodCount; ++i)
            {
                Vector3 foodPosition = gridController.PickRandomGridPosition();
                gridController.SliceGridPosition(foodPosition);

                GameObject goFood = gameObjectFactory.FetchFoodObject();
                goFood.transform.localPosition = foodPosition;
                FoodHandler foodHandler = goFood.GetComponent<FoodHandler>();
                foodHandler.PlayGrow(1.0f, Utils.RandomRange(1.0f, 1.5f));

                gridController.AddFoodObjectCount(1);
            }

            PlayController.main.ShowMainMenu();
            PlayController.main.WaitForMainMenuPlayClick(MainMenuPlayClicked);
        }

        private void PlayBuildingsEnter()
        {
            BuildingsHandler buildingsHandler = gameObjectFactory.buildingsHandlerObject.GetComponent<BuildingsHandler>();
            buildingsHandler.PlayEnter();
        }

        private void PlayGridEnter()
        {
            List<GameObject> grounds = PlayController.main.GetTerrainHandler().GetGrounds();

            float _randomScale = Utils.RandomRange(1.25f, 1.75f);
            float _randomHeight = Utils.RandomRange(7.0f, 10.0f);
            foreach (GameObject g in grounds)
            {
                Vector3 newPos = g.transform.localPosition;

                float _x = newPos.x;
                float _z = newPos.z;
                float xCoord = _x / _randomHeight * _randomScale;
                float yCoord = _z / _randomHeight * _randomScale;
                float height = Mathf.PerlinNoise(xCoord, yCoord);

                newPos.y = height;
                GroundHandler groundHandler = g.GetComponent<GroundHandler>();
                float duration = 0.25f;
                float _randomDelayX = Utils.RandomRange(0.02f, 0.04f);
                float _randomDelayY = Utils.RandomRange(0.02f, 0.04f);
                float delay = _x * _randomDelayX + _z * _randomDelayY;
                groundHandler.PlayEnter(newPos.y, Mathf.FloorToInt(height * 10f), duration, delay);

                PlayController.main.GetGridController().AddGridPosition(newPos);
            }
        }

        private void MainMenuPlayClicked()
        {
            PlayController.main.OnGameOverEvent -= OnGameOver;
            PlayController.main.OnGameOverEvent += OnGameOver;
            PlayController.main.StartDay();
        }

        private void OnDayStarted(GameData pGameData)
        {
            bool hasGridPositionAvailable = PlayController.main.GetGridController().HasGridPositionAvailable();
            bool canGrowTree = PlayController.main.CanGrowTree();
            bool canGrowStone = PlayController.main.CanGrowStone();
            bool canGrowFood = PlayController.main.CanGrowFood();

            if (canGrowTree && hasGridPositionAvailable)
            {
                GrowTrees(pGameData.treeObjectCount);
            }

            if (canGrowStone && hasGridPositionAvailable)
            {
                GrowStones(pGameData.stoneObjectCount);
            }

            if (canGrowFood && hasGridPositionAvailable)
            {
                GrowFoods(pGameData.foodObjectCount);
            }
        }

        private void GrowTrees(int pTreeCount)
        {
            GridController gridController = PlayController.main.GetGridController();

            int maxTreeCount = gridController.GetTreeObjectMaxCount();
            int randomExtra = Utils.RandomRange(0, 2);
            for (int i = pTreeCount - randomExtra; i < maxTreeCount; ++i)
            {
                Vector3 treePosition = gridController.PickRandomGridPosition();
                gridController.SliceGridPosition(treePosition);

                GameObject goTree = gameObjectFactory.FetchTreeObject();
                goTree.transform.localPosition = treePosition;
                TreeHandler treeHandler = goTree.GetComponent<TreeHandler>();
                treeHandler.PlayGrow(1.0f, Utils.RandomRange(0f, 0.5f));

                gridController.AddTreeObjectCount(1);
            }
        }

        private void GrowStones(int pStoneCount)
        {
            GridController gridController = PlayController.main.GetGridController();

            int maxStoneCount = gridController.GetStoneObjectMaxCount();
            int randomExtra = Utils.RandomRange(0, 2);
            for (int i = pStoneCount - randomExtra; i < maxStoneCount; ++i)
            {
                Vector3 stonePosition = gridController.PickRandomGridPosition();
                gridController.SliceGridPosition(stonePosition);

                GameObject goStone = gameObjectFactory.FetchStoneObject();
                goStone.transform.localPosition = stonePosition;
                StoneHandler stoneHandler = goStone.GetComponent<StoneHandler>();
                stoneHandler.PlayGrow(1.0f, Utils.RandomRange(0f, 0.5f));

                gridController.AddStoneObjectCount(1);
            }
        }

        private void GrowFoods(int pFoodCount)
        {
            GridController gridController = PlayController.main.GetGridController();

            int maxFoodCount = gridController.GetFoodObjectMaxCount();
            int randomExtra = Utils.RandomRange(0, 2);
            for (int i = pFoodCount - randomExtra; i < maxFoodCount; ++i)
            {
                Vector3 foodPosition = gridController.PickRandomGridPosition();
                gridController.SliceGridPosition(foodPosition);

                GameObject goFood = gameObjectFactory.FetchFoodObject();
                goFood.transform.localPosition = foodPosition;
                FoodHandler foodHandler = goFood.GetComponent<FoodHandler>();
                foodHandler.PlayGrow(1.0f, Utils.RandomRange(0f, 0.5f));

                gridController.AddFoodObjectCount(1);
            }
        }

        private void OnGameOver(GameData pGameData)
        {
            OnComplete?.Invoke(pGameData);
        }
    }
}
