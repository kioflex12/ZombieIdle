using System;
using Behavior.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Json;
using Utils.Logger;

namespace Core
{
    public sealed class GameState : GameComponent
    {
        public GameStateSerializationController SerializationController { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void OnDestroy()
        {
            Deinit();
        }

        private void Init()
        {
           SerializationController = new GameStateSerializationController();
           this.TryLoadSavedState();

        }

        private void Deinit()
        {
            this.TrySaveStateWithDocument(SerializationController);
            SerializationController = null;
        }

    }
}

