using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class GameLoad : MonoBehaviour
    {
        public event Action OnLoadComplete;

        public GameObjectFactory gameObjectFactory;

        public void Enter()
        {
            gameObjectFactory.OnLoadComplete += LoadComplete;
            gameObjectFactory.Load();
        }

        private void LoadComplete()
        {
            gameObjectFactory.OnLoadComplete -= LoadComplete;

            OnLoadComplete?.Invoke();
        }
    }
}
