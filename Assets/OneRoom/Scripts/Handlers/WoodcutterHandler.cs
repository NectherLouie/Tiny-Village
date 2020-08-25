using Blocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class WoodcutterHandler : MonoBehaviour
    {
        public void IncreaseWoodProduction()
        {
            PlayController.main.GetResourceController().UpdateWoodProduce();
        }

        public void EnableBoxCollider()
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        private void OnMouseDown()
        {
            //PlayController.main.ShowUnitActionPanel<WoodcutterHandler>(this);
        }
    }
}
