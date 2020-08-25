using Blocks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneRoom
{
    public class ResultController : MonoBehaviour
    {
        public static ResultController main;

        public GameData gameData;

        private GameObject gameOverPanelObject;
        private GameObject congratsPanelObject;

        public void Load()
        {
            main = this;

            GameObjectFactory gameObjectFactory = GameObjectFactory.main;

            gameOverPanelObject = gameObjectFactory.gameOverPanelObject;
            congratsPanelObject = gameObjectFactory.congratsPanelObject;
        }

        public void EnableGameOverInput(bool pEnabled)
        {
        }

        public void EnableCongratsInput(bool pEnabled)
        {
        }

        public void ResetGameData()
        {
            gameData.Reset();
        }

        public void ShowGameOver()
        {
            GameOverPanel gameOverPanel = gameOverPanelObject.GetComponent<GameOverPanel>();
            gameOverPanel.Show(0.25f, 1.0f);
        }

        public void WaitForMainMenuClick(Action pOnMainMenuClicked)
        {
            GameOverPanel gameOverPanel = gameOverPanelObject.GetComponent<GameOverPanel>();
            gameOverPanel.OnMainMenuButtonClicked -= pOnMainMenuClicked;
            gameOverPanel.OnMainMenuButtonClicked += pOnMainMenuClicked;
        }
    }
}
