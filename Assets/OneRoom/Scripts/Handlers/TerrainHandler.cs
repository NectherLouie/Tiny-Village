using Blocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class TerrainHandler : MonoBehaviour
    {
        public List<GameObject> grounds = new List<GameObject>();

        public List<GameObject> Load()
        {
            grounds.Clear();

            for (int childIndex = 0; childIndex < transform.childCount; ++childIndex)
            {
                GameObject child = transform.GetChild(childIndex).gameObject;

                if (child.GetComponent<GroundHandler>() != null)
                {
                    GroundHandler groundHandler = child.GetComponent<GroundHandler>();
                    groundHandler.Load(transform);
                    grounds.Add(child);
                }
            }

            return grounds;
        }

        public List<GameObject> GetGrounds()
        {
            return grounds;
        }
    }
}
