using Blocks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class DayNightController : MonoBehaviour
    {
        public event Action OnDayStarted;
        public event Action OnDayComplete;

        private GameData gameData;

        private bool timerStarted = false;
        private bool hasTriggeredNightStart = false;

        public void Load(ref GameData pGameData)
        {
            gameData = pGameData;
        }

        public void StartDay()
        {
            timerStarted = true;
        }

        private void Update()
        {
            if (timerStarted)
            {
                if (!gameData.gameIsPaused)
                {
                    gameData.dayCountdownSeconds += Time.deltaTime * gameData.gameSpeed;

                    float nightTime = Mathf.Floor(gameData.dayCountdownSeconds);
                    if (nightTime == gameData.daySpeedInSeconds * 0.5f && !hasTriggeredNightStart)
                    {
                        hasTriggeredNightStart = true;
                        OnDayComplete?.Invoke();
                    }

                    if (gameData.dayCountdownSeconds >= gameData.daySpeedInSeconds)
                    {
                        gameData.dayCountdownSeconds = 0;

                        hasTriggeredNightStart = false;

                        OnDayStarted?.Invoke();
                    }
                }
            }
        }

        public float GetCurrentTime()
        {
            return gameData.dayCountdownSeconds;
        }

        public float GetTimerMinValue()
        {
            return 0;
        }

        public float GetTimerMaxValue()
        {
            return gameData.daySpeedInSeconds;
        }

        public int GetDayCount()
        {
            return gameData.dayCount;
        }
    }
}
