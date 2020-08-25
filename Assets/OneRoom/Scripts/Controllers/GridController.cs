using Blocks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class GridController : MonoBehaviour
    {
        private GameData gameData;

        public void Load(ref GameData pGameData)
        {
            gameData = pGameData;
        }

        public void AddGridPosition(Vector3 pPosition)
        {
            gameData.gridPositions.Add(pPosition);
        }

        public Vector3 PickRandomGridPosition()
        {
            int randomPosIndex = Utils.RandomRange(0, gameData.gridPositions.Count);
            
            Vector3 output = Utils.Slice<Vector3>(gameData.gridPositions, randomPosIndex);

            gameData.usedGridPositions.Add(output);

            return output;
        }

        public bool HasGridPositionAvailable()
        {
            int totalPositionCountNeeded = gameData.treeObjectMaxCount + gameData.stoneObjectMaxCount + gameData.foodObjectMaxCount;
            
            return gameData.gridPositions.Count > (totalPositionCountNeeded * 2);
        }

        public void SliceGridPosition(Vector3 pPosition)
        {
            if (gameData.gridPositions.Contains(pPosition))
            {
                int posIndex = gameData.gridPositions.IndexOf(pPosition);

                Vector3 pos = Utils.Slice<Vector3>(gameData.gridPositions, posIndex);

                gameData.usedGridPositions.Add(pos);
            }
        }

        public void ReturnGridPosition(Vector3 pPosition)
        {
            if (gameData.usedGridPositions.Contains(pPosition))
            {
                int usedPosIndex = gameData.usedGridPositions.IndexOf(pPosition);
                Vector3 usedPosition = Utils.Slice<Vector3>(gameData.usedGridPositions, usedPosIndex);
                gameData.gridPositions.Add(usedPosition);
            }
        }

        public void AddTreeObjectCount(int pValue)
        {
            gameData.treeObjectCount += pValue;
        }

        public int GetTreeObjectMaxCount()
        {
            return gameData.treeObjectMaxCount;
        }

        public void AddStoneObjectCount(int pValue)
        {
            gameData.stoneObjectCount += pValue;
        }

        public int GetStoneObjectMaxCount()
        {
            return gameData.stoneObjectMaxCount;
        }

        public void AddFoodObjectCount(int pValue)
        {
            gameData.foodObjectCount += pValue;
        }

        public int GetFoodObjectMaxCount()
        {
            return gameData.foodObjectMaxCount;
        }
    }
}
