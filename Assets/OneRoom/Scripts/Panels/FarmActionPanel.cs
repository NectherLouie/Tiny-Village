using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using OneRoom;
using System;

public class FarmActionPanel : MonoBehaviour
{
    public TMP_Text populationCount;

    private void Update()
    {
        populationCount.text = PlayController.main.GetResourceController().GetFarmingPopulationCount().ToString();
    }

    public void ClickAddButton()
    {
        PlayController.main.GetResourceController().AddFarmingPopulation();
    }

    public void ClickRemoveButton()
    {
        PlayController.main.GetResourceController().RemoveFarmingPopulation();
    }
}
