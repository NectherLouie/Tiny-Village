using Blocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

namespace OneRoom
{
    public class HouseHandler : MonoBehaviour
    {
        public DataHouseHandler dataHouseHandler;

        public void EnableBoxCollider()
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        private void OnMouseDown()
        {
            //PlayController.main.ShowUnitActionPanel<HouseHandler>(this);
        }
    }
}
