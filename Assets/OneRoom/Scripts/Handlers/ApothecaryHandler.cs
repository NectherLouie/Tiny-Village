using Blocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class ApothecaryHandler : MonoBehaviour
    {
        public void IncreasePotionProduction()
        {
            PlayController.main.GetResourceController().UpdatePotionProduce();
        }

        public void EnableBoxCollider()
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        private void OnMouseDown()
        {
            //PlayController.main.ShowUnitActionPanel<QuarryHandler>(this);
        }
    }
}
