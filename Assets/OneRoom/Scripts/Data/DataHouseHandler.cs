using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    [Serializable]
    public class DataHouseHandler
    {
        public enum eHouseType { SMALL, MEDIUM, LARGE };
        public eHouseType houseType;

        public int additionalPopulation = 0;

        public Transform houseTransform;
    }
}
