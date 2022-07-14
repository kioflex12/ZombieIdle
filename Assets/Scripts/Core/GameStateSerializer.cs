using System;
using System.Collections.Generic;
using Core.State;
using JetBrains.Annotations;
using UnityEngine;
using Utils;
using Utils.Json;
using Utils.Logger;

namespace Core
{
    public static class GameStateSerializer
    {
        private const string LocalSaveStateName = "localSave.xml";

        public static string GetStatePath(string stateName) => JsonUtils.GetDefaultDocumentPath(stateName);

        public static bool TryLoadSavedState(this GameState state, List<BaseStateController> stateControllers)
        {
            Log.Trace(LogTag.State, "TryLoadSavedState");
            var isLoaded = TryLoadStateByName(state,LocalSaveStateName, stateControllers);
            return isLoaded;
        }

        static bool TryLoadStateByName(GameState state, string stateName,
            List<BaseStateController> baseStateControllers)
        {
            var loadResult = JsonUtils.LoadJsonDocumentFromSave(stateName);
            var isLoaded = ValidateLoadResult(loadResult);
            Log.TraceFormat(
                LogTag.State,
                "LoadStateByName('{0}', isLoaded: {1})",
                stateName, isLoaded);
            if (loadResult.Success)
            {
                LoadJson(LocalSaveStateName, loadResult.Document, baseStateControllers);
            }

            return isLoaded;
        }

        private static void LoadJson(string stateName, string jsonDocument,
            List<BaseStateController> baseStateControllers)
        {
            var statePath = GetStatePath(stateName);
            var hasValues = jsonDocument != string.Empty;
            if (hasValues)
            {
                Log.TraceFormat(LogTag.State, "LoadStateByName: Path: '{0}'", statePath);
                var loadResult = JsonUtility.FromJson<GameStateSerializationData>(jsonDocument);
                var jsonElement = new JsonElement<GameStateSerializationData>(LocalSaveStateName,loadResult);
                foreach (var controller in baseStateControllers)
                {
                    controller.Load(jsonElement.SerializedData);
                }

            }

        }

        static bool ValidateLoadResult(JsonLoadResult loadResult) {
            if ( loadResult.Success ) {
                return true;
            }

            if ( loadResult.Type == JsonLoadResultType.NotFound ) {
                return false;
            }
            if ( loadResult.Exception == null ) {
                return false;
            }
            Log.TraceException(loadResult.Exception);
            return false;
        }

        public static bool TrySaveStateWithDocument(this GameState gameState, List<BaseStateController> stateControllers)
        {
            foreach (var stateController in stateControllers)
            {
                stateController.Save();
            }
            var document = JsonUtils.GenerateJsonDocument(gameState.SerializationData);
            if (document != string.Empty)
            {
                var saveResult = JsonUtils.SaveJsonContent(document, LocalSaveStateName);
                if (saveResult.Success)
                {
                    Log.Trace(LogTag.State, $"{gameState} save complete.");
                    return true;
                }
                Log.TraceError(LogTag.State, $"GameState: Save failed; expetion: {saveResult.Exception}");
            }
            return false;
        }


    }

}
