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
                treeHandler.PlayGrow(1.0f, Utils.RandomRange(0f, 0.5f));

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
                stoneHandler.PlayGrow(1.0f, Utils.RandomRange(0f, 0.5f));

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
                foodHandler.PlayGrow(1.0f, Utils.RandomRange(0f, 0.5f));

                gridController.AddFoodObjectCount(1);
            }

            PlayController.main.ShowMainMenu();
            PlayController.main.WaitForMainMenuPlayClick(MainMenuPlayClicked);
        }

        private void PlayGridEnter()
        {
            int col = 7;
            int row = 7;
            float offsetX = 2f;
            float offsetZ = 2f;

            for (int x = 0; x < col; ++x)
            {
                for (int y = 0; y < row; ++y)
                {
                    GameObject groundObject = gameObjectFactory.FetchGroundObject();
                    Vector3 newPos = new Vector3(x * offsetX, 0, y * offsetZ);
                    groundObject.transform.localPosition = newPos;
                    GroundHandler groundHandler = groundObject.GetComponent<GroundHandler>();
                    groundHandler.PlayEnter(0.5f, Utils.RandomRange(0f, 0.5f));

                    PlayController.main.GetGridController().AddGridPosition(newPos);
                }
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
