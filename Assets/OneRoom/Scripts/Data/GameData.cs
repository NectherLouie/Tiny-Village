using OneRoom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data")]
public class GameData : ScriptableObject
{
    [Header("Game Settings")]
    public string languageCode = "en_GB";
    public float gameSpeed;
    public float daySpeedInSeconds;
    public float dayCountdownSeconds;
    public bool gameIsPaused;
    public int dayCount = 1;

    [Header("Gameplay Loop")]
    public GameObject buildPrefabObject;
    public List<Vector3> gridPositions = new List<Vector3>();
    public List<Vector3> usedGridPositions = new List<Vector3>();

    public int treeObjectCount;
    public int treeObjectMaxCount;
    public int moduloTreeObjectCount;

    public int stoneObjectCount;
    public int stoneObjectMaxCount;
    public int moduloStoneObjectCount;

    public int foodObjectCount;
    public int foodObjectMaxCount;
    public int moduloFoodObjectCount;

    public bool hasTriggeredGameOver = false;

    [Header("Resources")]
    public int foodCount;
    public int foodIncrement;
    public int foodProduce;
    public int foodHarvestIncrement;
    public int foodProduceIncrement;
    public int foodAdditional;

    public int woodCount;
    public int woodIncrement;
    public int woodProduce;
    public int woodTreeIncrement;
    public int woodProduceIncrement;
    public int woodAdditional;

    public int stoneCount;
    public int stoneIncrement;
    public int stoneProduce;
    public int stoneMineIncrement;
    public int stoneProduceIncrement;
    public int stoneAdditional;

    public int potionCount;
    public int potionIncrement;
    public int potionProduce;
    public int potionMineIncrement;
    public int potionProduceIncrement;
    public int potionAdditional;

    public int boozeCount;
    public int boozeIncrement;
    public int boozeProduce;
    public int boozeMineIncrement;
    public int boozeProduceIncrement;
    public int boozeAdditional;

    [Header("Population Resources")]
    public int villagerCost;
    public int villagerIncrement;

    public int idlePopulationCount;
    public int idlePopulationIncrement;

    public int maxPopulationCount;
    public int maxPopulationIncrement;

    public int farmingPopulationCount;
    public int farmingPopulationCountIncrement;
    public int maxFarmingPopulationCount;
    public int maxFarmingPopulationAdditional;

    public int woodcutterPopulationCount;
    public int woodcutterPopulationCountIncrement;
    public int maxWoodcutterPopulationCount;
    public int maxWoodcutterPopulationAdditional;

    public int minerPopulationCount;
    public int minerPopulationCountIncrement;
    public int maxMinerPopulationCount;
    public int maxMinerPopulationAdditional;

    public int apothecaryPopulationCount = 0;
    public int apothecaryPopulationCountIncrement = 1;
    public int maxApothecaryPopulationCount = 0;
    public int maxApothecaryPopulationAdditional = 1;

    public int barkeepPopulationCount = 0;
    public int barkeepPopulationCountIncrement = 1;
    public int maxBarkeepPopulationCount = 0;
    public int maxBarkeepPopulationAdditional = 2;

    [Header("Resource Costs - Houses")]
    public DataResourceCost houseSmallResourceCost = new DataResourceCost();
    public DataResourceCost houseMediumResourceCost = new DataResourceCost();
    public DataResourceCost houseLargeResourceCost = new DataResourceCost();

    [Header("Resource Costs - Resources")]
    public DataResourceCost foresterResourceCost = new DataResourceCost();
    public DataResourceCost quarryResourceCost = new DataResourceCost();
    public DataResourceCost cemeteryResourceCost = new DataResourceCost();

    [Header("Resource Costs - Satisfaction")]
    public DataResourceCost farmResourceCost = new DataResourceCost();
    public DataResourceCost clinicResourceCost = new DataResourceCost();
    public DataResourceCost tavernResourceCost = new DataResourceCost();

    [Header("House Data")]
    public DataHouseHandler houseSmallData = new DataHouseHandler();
    public DataHouseHandler houseMediumData = new DataHouseHandler();
    public DataHouseHandler houseLargeData = new DataHouseHandler();

    public void Reset()
    {
        gameSpeed = 1.0f;
        daySpeedInSeconds = 12.0f;
        dayCountdownSeconds = 0;
        dayCount = 1;
        gameIsPaused = false;

        // gameplay
        buildPrefabObject = null;
        gridPositions = new List<Vector3>();
        usedGridPositions = new List<Vector3>();

        treeObjectCount = 0;
        treeObjectMaxCount = 4;
        moduloTreeObjectCount = 2;
        
        stoneObjectCount = 0;
        stoneObjectMaxCount = 2;
        moduloStoneObjectCount = 2;

        foodObjectCount = 0;
        foodObjectMaxCount = 2;
        moduloFoodObjectCount = 3;

        hasTriggeredGameOver = false;

        // resources
        foodCount = 0;
        foodIncrement = 1;
        foodProduce = 0;
        foodHarvestIncrement = 2;
        foodProduceIncrement = 2;
        foodAdditional = 4;

        woodCount = 0;
        woodIncrement = 1;
        woodProduce = 0;
        woodTreeIncrement = 1;
        woodProduceIncrement = 5;
        woodAdditional = 1;

        stoneCount = 0;
        stoneIncrement = 1;
        stoneProduce = 0;
        stoneMineIncrement = 2;
        stoneProduceIncrement = 5;
        stoneAdditional = 1;

        potionCount = 0;
        potionIncrement = 1;
        potionProduce = 0;
        potionMineIncrement = 1;
        potionProduceIncrement = 3;
        potionAdditional = 2;

        boozeCount = 0;
        boozeIncrement = 1;
        boozeProduce = 0;
        boozeMineIncrement = 1;
        boozeProduceIncrement = 5;
        boozeAdditional = 5;

        // population resources
        villagerCost = 4;
        villagerIncrement = 1;
        
        idlePopulationCount = 0;
        idlePopulationIncrement = 1;

        maxPopulationCount = 0;
        maxPopulationIncrement = 1;

        farmingPopulationCount = 0;
        farmingPopulationCountIncrement = 1;
        maxFarmingPopulationCount = 0;
        maxFarmingPopulationAdditional = 5;

        woodcutterPopulationCount = 0;
        woodcutterPopulationCountIncrement = 1;
        maxWoodcutterPopulationCount = 0;
        maxWoodcutterPopulationAdditional = 3;

        minerPopulationCount = 0;
        minerPopulationCountIncrement = 1;
        maxMinerPopulationCount = 0;
        maxMinerPopulationAdditional = 3;

        apothecaryPopulationCount = 0;
        apothecaryPopulationCountIncrement = 1;
        maxApothecaryPopulationCount = 0;
        maxApothecaryPopulationAdditional = 1;

        barkeepPopulationCount = 0;
        barkeepPopulationCountIncrement = 1;
        maxBarkeepPopulationCount = 0;
        maxBarkeepPopulationAdditional = 2;

        // Houses
        houseSmallResourceCost.woodCost = 5;
        houseSmallResourceCost.stoneCost = 5;

        houseMediumResourceCost.woodCost = 10;
        houseMediumResourceCost.stoneCost = 10;

        houseLargeResourceCost.woodCost = 20;
        houseLargeResourceCost.stoneCost = 20;

        houseSmallData.houseType = DataHouseHandler.eHouseType.SMALL;
        houseSmallData.additionalPopulation = 5;

        houseMediumData.houseType = DataHouseHandler.eHouseType.MEDIUM;
        houseMediumData.additionalPopulation = 10;

        houseLargeData.houseType = DataHouseHandler.eHouseType.LARGE;
        houseLargeData.additionalPopulation = 20;

        // Resources
        foresterResourceCost.woodCost = 10;
        foresterResourceCost.stoneCost = 10;

        quarryResourceCost.woodCost = 10;
        quarryResourceCost.stoneCost = 10;

        farmResourceCost.woodCost = 10;
        farmResourceCost.stoneCost = 10;

        // Services
        cemeteryResourceCost.woodCost = 100;
        cemeteryResourceCost.stoneCost = 100;

        clinicResourceCost.woodCost = 10;
        clinicResourceCost.stoneCost = 10;

        tavernResourceCost.woodCost = 10;
        tavernResourceCost.stoneCost = 10;
    }
}
