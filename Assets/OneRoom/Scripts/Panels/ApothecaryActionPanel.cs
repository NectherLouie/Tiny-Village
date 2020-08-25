using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using OneRoom;
using System;

public class ApothecaryActionPanel : MonoBehaviour
{
    public TMP_Text populationCount;

    private void Update()
    {
        populationCount.text = PlayController.main.GetResourceController().GetApothecaryPopulationCount().ToString();
    }

    public void ClickAddButton()
    {
        PlayController.main.GetResourceController().AddApothecaryPopulation();
    }

    public void ClickRemoveButton()
    {
        PlayController.main.GetResourceController().RemoveApothecaryPopulation();
    }
}
