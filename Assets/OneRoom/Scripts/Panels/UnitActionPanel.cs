using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using OneRoom;
using System;

public class UnitActionPanel : MonoBehaviour
{
    public void Show<T>(T pHandler)
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {

    }
}
