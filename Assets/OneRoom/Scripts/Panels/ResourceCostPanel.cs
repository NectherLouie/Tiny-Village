using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCostPanel : MonoBehaviour
{
    public TMP_Text buildingNameText;
    public TMP_Text woodCostText;
    public TMP_Text stoneCostText;

    public Image woodIcon;
    public Image stoneIcon;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 pPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pPos);
        transform.position = screenPos;
    }

    public void Show(string pBuildingName, int pWoodCost, int pStoneCost)
    {
        buildingNameText.gameObject.SetActive(true);
        woodIcon.gameObject.SetActive(true);
        stoneIcon.gameObject.SetActive(true);

        buildingNameText.text = pBuildingName;
        woodCostText.text = pWoodCost.ToString();
        stoneCostText.text = pStoneCost.ToString();

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        buildingNameText.gameObject.SetActive(false);
        woodIcon.gameObject.SetActive(false);
        stoneIcon.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }
}
