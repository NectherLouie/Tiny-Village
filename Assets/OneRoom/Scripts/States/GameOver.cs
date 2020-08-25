using Blocks;
using System;
using UnityEngine;

namespace OneRoom
{
    public class GameOver : MonoBehaviour
    {
        public event Action OnComplete;
        public GameObjectFactory gameObjectFactory;

        public void Enter()
        {
            ResultController.main.EnableGameOverInput(true);
            ResultController.main.ShowGameOver();
            ResultController.main.WaitForMainMenuClick(MainMenuClicked);
        }

        private void MainMenuClicked()
        {
            ResultController.main.EnableGameOverInput(false);
            PlayController.main.ResetScene();

            OnComplete?.Invoke();
        }
    }
}
