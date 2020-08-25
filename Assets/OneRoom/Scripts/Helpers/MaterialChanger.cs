using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Renderer rendererToChange;
    public Material[] materialsToUse;

    public void ChangeMaterial(int index)
    {
        rendererToChange.material = materialsToUse[index];
    }
}
