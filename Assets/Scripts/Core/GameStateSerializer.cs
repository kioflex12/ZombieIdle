using UnityEngine;
using Utils;
using Utils.Json;
using Utils.Logger;

namespace Core
{
    static class GameStateSerializer
    {
        private const string LocalSaveStateName = "localSave.xml";

        public static string GetStatePath(string stateName) => JsonUtils.GetDefaultDocumentPath(stateName);

        public static bool TryLoadSavedState(this GameState state)
        {
            Log.Trace(LogTag.State, "TryLoadSavedState");
            var isLoaded = TryLoadStateByName(state,LocalSaveStateName);
            return isLoaded;
        }

        static bool TryLoadStateByName(GameState state, string stateName)
        {
            var loadResult = JsonUtils.LoadJsonDocumentFromSave(stateName);
            var isLoaded = ValidateLoadResult(loadResult);
            Log.TraceFormat(
                LogTag.State,
                "LoadStateByName('{0}', isLoaded: {1})",
                stateName, isLoaded);
            if (loadResult.Success)
            {
                LoadJson(state, LocalSaveStateName, loadResult.Document);
            }

            return isLoaded;
        }

        private static void LoadJson(GameState state, string stateName, string jsonDocument )
        {
            var statePath = GetStatePath(stateName);
            var hasValues = jsonDocument != string.Empty;
            if (hasValues)
            {
                Log.TraceFormat(LogTag.State, "LoadStateByName: Path: '{0}'", statePath);
                var loadResult = JsonUtility.FromJson<GameStateSerializationController>(jsonDocument);
                var jsonElement = new JsonElement<GameStateSerializationController>(LocalSaveStateName,loadResult);
                state.SerializationController.Load(jsonElement);
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

        public static bool TrySaveStateWithDocument<T>(this GameState _, T serializedData)
        {
            var document = GenerateJsonDocument(serializedData);
            if (document != string.Empty)
            {
                var saveResult = JsonUtils.SaveJsonContent(document, LocalSaveStateName);
                if (saveResult.Success)
                {
                    Log.Trace(LogTag.State, "GameState save complete.");
                    return true;
                }
                Log.TraceError(LogTag.State, $"GameState: Save failed; expetion: {saveResult.Exception}");
            }
            return false;
        }

        private static string GenerateJsonDocument<T>(T serializedData)
        {
            var jsonElement = new JsonElement<T>(LocalSaveStateName, serializedData);
            return JsonUtility.ToJson(jsonElement.SerilizedData);
        }
    }

}
