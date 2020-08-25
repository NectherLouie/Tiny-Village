using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class ResourceController : MonoBehaviour
    {
        private GameData gameData;

        public void Load(ref GameData pGameData)
        {
            gameData = pGameData;
        }

        // --- Food -------------------------------------------------
        public void AddFood()
        {
            gameData.foodCount += gameData.foodIncrement;

            if (gameData.foodCount < 0)
            {
                gameData.foodCount = 0;
            }
        }

        public int GetFoodCount()
        {
            return gameData.foodCount;
        }

        public void UpdateFoodCount()
        {
            gameData.foodIncrement = gameData.foodHarvestIncrement;
            AddFood();
        }

        public void UpdateFoodProduce()
        {
            gameData.foodIncrement = gameData.foodProduceIncrement;
            gameData.foodProduce += gameData.foodIncrement;
        }

        public void CollectFoodProduce()
        {
            gameData.foodIncrement = gameData.foodProduce;
            AddFood();
        }

        public void FeedPopulation()
        {
            int sumPopulation = GetSumPopulationCount();

            int tempFoodCount = gameData.foodCount;
            int tempFoodDifference = tempFoodCount - sumPopulation;
            if (tempFoodDifference >= 0)
            {
                gameData.foodIncrement = -sumPopulation;
                AddFood();
            }
            else
            {
                gameData.foodIncrement = -sumPopulation;
                AddFood();

                if (gameData.idlePopulationCount > 0)
                {
                    gameData.idlePopulationIncrement = -1;
                    AddIdlePopulation();
                }
                else if (gameData.farmingPopulationCount > 0)
                {
                    gameData.farmingPopulationCountIncrement = -1;
                    UpdateFarmingPopulation();
                }
                else if (gameData.woodcutterPopulationCount > 0)
                {
                    gameData.woodcutterPopulationCountIncrement = -1;
                    UpdateWoodcutterPopulation();
                }
                else if (gameData.minerPopulationCount > 0)
                {
                    gameData.minerPopulationCountIncrement = -1;
                    UpdateMinerPopulation();
                }
                else if (gameData.apothecaryPopulationCount > 0)
                {
                    gameData.apothecaryPopulationCountIncrement = -1;
                    UpdateApothecaryPopulation();
                }
                else if (gameData.barkeepPopulationCount > 0)
                {
                    gameData.barkeepPopulationCountIncrement = -1;
                    UpdateBarkeepPopulation();
                }
            }
        }

        // --- Wood -------------------------------------------------
        public void AddWood()
        {
            gameData.woodCount += gameData.woodIncrement;
        }

        public int GetWoodCount()
        {
            return gameData.woodCount;
        }

        public void UpdateWoodCount()
        {
            gameData.woodIncrement = gameData.woodTreeIncrement;
            AddWood();
        }

        public void UpdateWoodProduce()
        {
            gameData.woodIncrement = gameData.woodProduceIncrement;
            gameData.woodProduce += gameData.woodIncrement;
        }

        // --- Stone -------------------------------------------------
        public void AddStone()
        {
            gameData.stoneCount += gameData.stoneIncrement;
        }

        public int GetStoneCount()
        {
            return gameData.stoneCount;
        }

        public void UpdateStoneCount()
        {
            gameData.stoneIncrement = gameData.stoneMineIncrement;
            AddStone();
        }

        public void UpdateStoneProduce()
        {
            gameData.stoneIncrement = gameData.stoneProduceIncrement;
            gameData.stoneProduce += gameData.stoneIncrement;
        }

        // --- Potion -------------------------------------------------
        public void AddPotion()
        {
            gameData.potionCount += gameData.potionIncrement;
        }

        public int GetPotionCount()
        {
            return gameData.potionCount;
        }

        public void UpdatePotionCount()
        {
            gameData.potionIncrement = gameData.potionMineIncrement;
            AddPotion();
        }

        public void UpdatePotionProduce()
        {
            gameData.potionIncrement = gameData.potionProduceIncrement;
            gameData.potionProduce += gameData.potionIncrement;
        }

        // --- Booze -------------------------------------------------
        public void AddBooze()
        {
            gameData.boozeCount += gameData.boozeIncrement;
        }

        public int GetBoozeCount()
        {
            return gameData.boozeCount;
        }

        public void UpdateBoozeCount()
        {
            gameData.boozeIncrement = gameData.boozeMineIncrement;
            AddBooze();
        }

        public void UpdateBoozeProduce()
        {
            gameData.boozeIncrement = gameData.boozeProduceIncrement;
            gameData.boozeProduce += gameData.boozeIncrement;
        }

        // --- Population -------------------------------------------------
        public int GetVillagerFoodCost()
        {
            return gameData.villagerCost;
        }

        public void CreateVillager()
        {
            // can player afford ?
            if (gameData.foodCount >= gameData.villagerCost)
            {
                // is it maxed?
                int sumPopulation = GetSumPopulationCount();
                if (sumPopulation < gameData.maxPopulationCount)
                {
                    // remove food cost
                    gameData.foodIncrement = -gameData.villagerCost;
                    AddFood();

                    // add population
                    gameData.idlePopulationIncrement = gameData.villagerIncrement;
                    AddIdlePopulation();
                }
            }
        }

        public int GetSumPopulationCount()
        {
            int idlers = gameData.idlePopulationCount;
            int farmers = gameData.farmingPopulationCount;
            int woodcutters = gameData.woodcutterPopulationCount;
            int miners = gameData.minerPopulationCount;

            return idlers + farmers + woodcutters + miners;
        }

        public void AddIdlePopulation()
        {
            gameData.idlePopulationCount += gameData.idlePopulationIncrement;
        }

        public int GetIdlePopulationCount()
        {
            return gameData.idlePopulationCount;
        }

        public void AddMaxPopulation()
        {
            gameData.maxPopulationCount += gameData.maxPopulationIncrement;
        }

        public int GetMaxPopulationCount()
        {
            return gameData.maxPopulationCount;
        }

        public void UpdateMaxPopulationIncrease(DataHouseHandler.eHouseType pHouseType)
        {
            switch (pHouseType)
            {
                case DataHouseHandler.eHouseType.SMALL:
                    gameData.maxPopulationIncrement = gameData.houseSmallData.additionalPopulation;
                    break;
                case DataHouseHandler.eHouseType.MEDIUM:
                    gameData.maxPopulationIncrement = gameData.houseMediumData.additionalPopulation;
                    break;
                case DataHouseHandler.eHouseType.LARGE:
                    gameData.maxPopulationIncrement = gameData.houseLargeData.additionalPopulation;
                    break;
            }

            AddMaxPopulation();
        }

        // --- Gatherers ----------------------------------------------------------------
        // Farmers
        public void AddFarmingPopulation()
        {
            gameData.farmingPopulationCountIncrement = 1;
            if (gameData.farmingPopulationCount >= gameData.maxFarmingPopulationCount)
            {
                return;
            }

            if (gameData.idlePopulationCount > 0)
            {
                gameData.farmingPopulationCount += gameData.farmingPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.farmingPopulationCountIncrement;
                AddIdlePopulation();

                gameData.foodProduceIncrement = gameData.foodAdditional;
                UpdateFoodProduce();
            }
        }

        public void UpdateFarmingPopulation()
        {
            gameData.farmingPopulationCount += gameData.farmingPopulationCountIncrement;

            gameData.foodProduceIncrement = -gameData.foodAdditional;
            UpdateFoodProduce();
        }

        public void RemoveFarmingPopulation()
        {
            gameData.farmingPopulationCountIncrement = -1;
            if (gameData.farmingPopulationCount > 0)
            {
                gameData.farmingPopulationCount += gameData.farmingPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.farmingPopulationCountIncrement;
                AddIdlePopulation();

                gameData.foodProduceIncrement = -gameData.foodAdditional;
                UpdateFoodProduce();
            }
        }

        public void UpdateMaxFarmingPopulation()
        {
            gameData.maxFarmingPopulationCount += gameData.maxFarmingPopulationAdditional;
        }

        public int GetFarmingPopulationCount()
        {
            return gameData.farmingPopulationCount;
        }

        // Woodcutter
        public void AddWoodcutterPopulation()
        {
            gameData.woodcutterPopulationCountIncrement = 1;

            if (gameData.woodcutterPopulationCount >= gameData.maxWoodcutterPopulationCount)
            {
                return;
            }

            if (gameData.idlePopulationCount > 0)
            {
                gameData.woodcutterPopulationCount += gameData.woodcutterPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.woodcutterPopulationCountIncrement;
                AddIdlePopulation();

                gameData.woodProduceIncrement = gameData.woodAdditional;
                UpdateWoodProduce();
            }
        }

        public void UpdateWoodcutterPopulation()
        {
            gameData.woodcutterPopulationCount += gameData.woodcutterPopulationCountIncrement;

            gameData.woodProduceIncrement = -gameData.woodAdditional;
            UpdateWoodProduce();
        }

        public void RemoveWoodcutterPopulation()
        {
            gameData.woodcutterPopulationCountIncrement = -1;

            if (gameData.woodcutterPopulationCount > 0)
            {
                gameData.woodcutterPopulationCount += gameData.woodcutterPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.woodcutterPopulationCountIncrement;
                AddIdlePopulation();

                gameData.woodProduceIncrement = -gameData.woodAdditional;
                UpdateWoodProduce();
            }
        }

        public void UpdateMaxWoodcutterPopulation()
        {
            gameData.maxWoodcutterPopulationCount += gameData.maxWoodcutterPopulationAdditional;
        }

        public int GetWoodcutterPopulationCount()
        {
            return gameData.woodcutterPopulationCount;
        }

        // Miner
        public void AddMinerPopulation()
        {
            gameData.minerPopulationCountIncrement = 1;

            if (gameData.minerPopulationCount >= gameData.maxMinerPopulationCount)
            {
                return;
            }

            if (gameData.idlePopulationCount > 0)
            {
                gameData.minerPopulationCount += gameData.minerPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.minerPopulationCountIncrement;
                AddIdlePopulation();

                gameData.stoneProduceIncrement = gameData.stoneAdditional;
                UpdateStoneProduce();
            }
        }

        public void UpdateMinerPopulation()
        {
            gameData.minerPopulationCount += gameData.minerPopulationCountIncrement;

            gameData.stoneProduceIncrement = -gameData.stoneAdditional;
            UpdateStoneProduce();
        }

        public void RemoveMinerPopulation()
        {
            gameData.minerPopulationCountIncrement = -1;

            if (gameData.minerPopulationCount > 0)
            {
                gameData.minerPopulationCount += gameData.minerPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.minerPopulationCountIncrement;
                AddIdlePopulation();

                gameData.stoneProduceIncrement = -gameData.stoneAdditional;
                UpdateStoneProduce();
            }
        }

        public void UpdateMaxMinerPopulation()
        {
            gameData.maxMinerPopulationCount += gameData.maxMinerPopulationAdditional;
        }

        public int GetMinerPopulationCount()
        {
            return gameData.minerPopulationCount;
        }

        // --- Specialist ----------------------------------------------------------------
        // Apothecary
        public void AddApothecaryPopulation()
        {
            gameData.apothecaryPopulationCountIncrement = 1;
            if (gameData.apothecaryPopulationCount >= gameData.maxApothecaryPopulationCount)
            {
                return;
            }

            if (gameData.idlePopulationCount > 0)
            {
                gameData.apothecaryPopulationCount += gameData.apothecaryPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.apothecaryPopulationCountIncrement;
                AddIdlePopulation();

                gameData.potionProduceIncrement = gameData.potionAdditional;
                UpdatePotionProduce();
            }
        }

        public void UpdateApothecaryPopulation()
        {
            gameData.apothecaryPopulationCount += gameData.apothecaryPopulationCountIncrement;

            gameData.potionProduceIncrement = -gameData.potionAdditional;
            UpdatePotionProduce();
        }

        public void RemoveApothecaryPopulation()
        {
            gameData.apothecaryPopulationCountIncrement = -1;
            if (gameData.apothecaryPopulationCount > 0)
            {
                gameData.apothecaryPopulationCount += gameData.apothecaryPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.apothecaryPopulationCountIncrement;
                AddIdlePopulation();

                gameData.potionProduceIncrement = -gameData.potionAdditional;
                UpdatePotionProduce();
            }
        }

        public void UpdateMaxApothecaryPopulation()
        {
            gameData.maxApothecaryPopulationCount += gameData.maxApothecaryPopulationAdditional;
        }

        public int GetApothecaryPopulationCount()
        {
            return gameData.apothecaryPopulationCount;
        }

        // Barkeep
        public void AddBarkeepPopulation()
        {
            gameData.barkeepPopulationCountIncrement = 1;
            if (gameData.barkeepPopulationCount >= gameData.maxBarkeepPopulationCount)
            {
                return;
            }

            if (gameData.idlePopulationCount > 0)
            {
                gameData.barkeepPopulationCount += gameData.barkeepPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.barkeepPopulationCountIncrement;
                AddIdlePopulation();

                gameData.boozeProduceIncrement = gameData.boozeAdditional;
                UpdateBoozeProduce();
            }
        }

        public void UpdateBarkeepPopulation()
        {
            gameData.barkeepPopulationCount += gameData.barkeepPopulationCountIncrement;

            gameData.boozeProduceIncrement = -gameData.boozeAdditional;
            UpdateBoozeProduce();
        }

        public void RemoveBarkeepPopulation()
        {
            gameData.barkeepPopulationCountIncrement = -1;
            if (gameData.barkeepPopulationCount > 0)
            {
                gameData.barkeepPopulationCount += gameData.barkeepPopulationCountIncrement;

                gameData.idlePopulationIncrement = -1 * gameData.barkeepPopulationCountIncrement;
                AddIdlePopulation();

                gameData.boozeProduceIncrement = -gameData.boozeAdditional;
                UpdateBoozeProduce();
            }
        }

        public void UpdateMaxBarkeepPopulation()
        {
            gameData.maxBarkeepPopulationCount += gameData.maxBarkeepPopulationAdditional;
        }

        public int GetBarkeepPopulationCount()
        {
            return gameData.barkeepPopulationCount;
        }
    }
}
