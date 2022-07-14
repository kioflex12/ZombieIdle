using System;
using System.Collections.Generic;
using Behavior.Gameplay.Controllers;
using Core.State;
using Utils.Logger;

namespace Core
{
    public sealed class GameState
    {
        private readonly List<BaseStateController> _controllers = new List<BaseStateController>();
        public GameStateSerializationData SerializationData { get; private set; }

        public static GameState Instance { get; private set; }

        public bool SavedWithoutUpload { get; private set; }


        public ZombieController ZombieController { get; private set; }


        private GameState()
        {
            SerializationData = new GameStateSerializationData();
            AddControllers();
        }

        private void AddControllers()
        {
            ZombieController = Add(new ZombieController(this));

        }

        private void Init()
        {
            this.TryLoadSavedState(_controllers);

        }

        private void DeInit()
        {
            this.TrySaveStateWithDocument(_controllers);
            SerializationData = default;
        }

        public static GameState TryCreate()
        {
            if ( Instance == null )
            {
                Instance = new GameState();
                Instance.Reload();
            }
            return Instance;
        }
        void Preload() {
            Reset();
            Init();
            PostInit();
        }

        private void Reset()
        {
            foreach ( var controller in _controllers )
            {
                try
                {
                    controller.Reset();
                } catch ( Exception e )
                {
                    Log.TraceException(e);
                }
            }
        }

        void PostInit() {
            foreach ( var controller in _controllers )
            {
                try
                {
                    controller.PostInit();
                } catch ( Exception e )
                {
                    Log.TraceException(e);
                }
            }
        }

        private void Reload() {

            if ( this.TryLoadSavedState(_controllers) ) {
                return;
            }
            Preload();

        }

        private T Add<T>(T controller) where T : BaseStateController {
            _controllers.Add(controller);
            return controller;
        }

    }
}

