using Blocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class FarmHandler : MonoBehaviour
    {
        public void IncreaseFoodProduction()
        {
            PlayController.main.GetResourceController().UpdateFoodProduce();
        }

        public void EnableBoxCollider()
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        private void OnMouseDown()
        {
            //PlayController.main.ShowUnitActionPanel<FarmHandler>(this);
        }
    }
}
