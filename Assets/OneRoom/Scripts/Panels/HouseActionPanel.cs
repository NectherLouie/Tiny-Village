using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using OneRoom;
using System;

public class HouseActionPanel : MonoBehaviour
{
    public TMP_Text populationCount;
    public TMP_Text foodCost;

    private void Update()
    {
        populationCount.text = PlayController.main.GetResourceController().GetIdlePopulationCount().ToString();
    }

    public void ClickAddButton()
    {
        PlayController.main.GetResourceController().CreateVillager();
    }
}
