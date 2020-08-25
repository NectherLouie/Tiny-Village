using Blocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class QuarryHandler : MonoBehaviour
    {
        public void IncreaseStoneProduction()
        {
            PlayController.main.GetResourceController().UpdateStoneProduce();
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
