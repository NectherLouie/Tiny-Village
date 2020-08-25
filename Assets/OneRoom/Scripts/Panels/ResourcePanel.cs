using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OneRoom
{
    public class ResourcePanel : MonoBehaviour
    {
        [Header("Main Resources")]
        public TMP_Text woodText;
        public TMP_Text stoneText;
        public TMP_Text foodText;
        public TMP_Text potionText;
        public TMP_Text boozeText;

        [Header("Population Count")]
        public TMP_Text maxPopulationText;

        private void Update()
        {
            ResourceController resourceController = PlayController.main.GetResourceController();

            maxPopulationText.text = resourceController.GetMaxPopulationCount().ToString();
            foodText.text = resourceController.GetFoodCount().ToString();
            woodText.text = resourceController.GetWoodCount().ToString();
            stoneText.text = resourceController.GetStoneCount().ToString();
            potionText.text = resourceController.GetPotionCount().ToString();
            boozeText.text = resourceController.GetBoozeCount().ToString();

        }

        public void ClickGetVillagerButton()
        {
            PlayController.main.GetResourceController().CreateVillager();
        }
    }
}
