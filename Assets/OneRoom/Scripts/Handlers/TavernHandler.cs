using Blocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class TavernHandler : MonoBehaviour
    {
        public void IncreaseBoozeProduction()
        {
            PlayController.main.GetResourceController().UpdateBoozeProduce();
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
