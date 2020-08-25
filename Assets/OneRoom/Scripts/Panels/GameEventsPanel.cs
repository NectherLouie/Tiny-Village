using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OneRoom
{
    public class GameEventsPanel : MonoBehaviour
    {
        public string dayStringKey;
        public string dayString;
        public TMP_Text dayText;
        public Slider dayBar;

        private bool dayStarted = false;
        private DayNightController dayNightController;

        public void StartDay()
        {
            dayNightController = PlayController.main.GetDayNightController();

            dayBar.minValue = dayNightController.GetTimerMinValue();
            dayBar.maxValue = dayNightController.GetTimerMaxValue();

            dayBar.value = dayBar.minValue;

            dayString = LocalisationService.GetTranslation(dayStringKey);

            dayStarted = true;
        }

        private void Update()
        {
            dayText.text = dayString + " " + dayNightController.GetDayCount().ToString();

            if (dayStarted && dayNightController != null)
            {
                dayBar.value = dayNightController.GetCurrentTime();
            }
        }
    }
}
