using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class LookAtCamera : MonoBehaviour
    {
        private void FixedUpdate()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
