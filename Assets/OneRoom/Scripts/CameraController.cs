using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{ 
    public class CameraController : MonoBehaviour
    {
        public GameObject mainCamera;

        public List<Vector3> cameraZoomPositions = new List<Vector3>();
        public int cameraZoomIndex = 0;
        public float zoomTime = 5f;

        Transform cameraTransform;

        private void Start()
        {
            cameraTransform = mainCamera.transform;
        }

        void Update()
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, cameraZoomPositions[cameraZoomIndex], Time.deltaTime * zoomTime);
        }
}
}
