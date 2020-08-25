using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LookAtCamera : MonoBehaviour
{
    private Transform target;

    private void Awake()
    {
        target = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        transform.LookAt(target, Vector3.up);
    }
}
