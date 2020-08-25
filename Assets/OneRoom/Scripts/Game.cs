using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class Game : MonoBehaviour
    {
        public GameData gameData;

        [Header("Services")]
        public GameObjectFactory gameObjectFactory;
        public Services services;

        [Header("States")]
        public GameLoad gameLoad;
        public PlayEnter playEnter;
        public GameOver gameOver;

        private bool gameOn = false;

        private void Update()
        {
            if (!gameOn)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    gameOn = true;

                    Init();
                }
            }
        }

        private void Init()
        {
            // Init Services
            services.Init();
            services.SetLanguageCode(gameData.languageCode);
            services.LoadLocalisationService();

            // Load Play Controller
            GameObject playControllerObject = Instantiate(gameObjectFactory.prefabPlayController);
            playControllerObject.SetActive(true);

            PlayController playController = playControllerObject.GetComponent<PlayController>();
            playController.Load();

            PlayController.main.ResetGameData();

            // Load Result Controller
            GameObject resultControllerObject = Instantiate(gameObjectFactory.prefabResultController);
            resultControllerObject.SetActive(true);

            ResultController resultController = resultControllerObject.GetComponent<ResultController>();
            resultController.Load();

            // inject factory to states
            gameLoad.gameObjectFactory = gameObjectFactory;
            playEnter.gameObjectFactory = gameObjectFactory;

            // load
            gameLoad.OnLoadComplete += OnLoadComplete;
            gameLoad.Enter();
        }

        private void OnLoadComplete()
        {
            playEnter.OnComplete += OnPlayEnterComplete;
            playEnter.Enter();
        }

        private void OnPlayEnterComplete(GameData pGameData)
        {
            if (gameData.hasTriggeredGameOver)
            {
                gameOver.Enter();
            }
            else
            {
                /// Congrats
            }
        }
    }
}
