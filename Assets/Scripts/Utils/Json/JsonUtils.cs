using System;
using System.IO;
using UnityEngine;
using Utils.Logger;

namespace Utils.Json
{
    public static class JsonUtils
    {
        public static JsonLoadResult LoadJsonDocumentFromSave(string name) => LoadJsonDocumentFromDirectory(name, GetDefaultDataDirectory());

        private static JsonLoadResult LoadJsonDocumentFromDirectory(string name, string rootPath)
        {
            var path = GetDocumentPath(rootPath,name);
            try
            {
                JsonLoadResult loadResult;
                if (File.Exists(path))
                {
                    var text = File.ReadAllText(path);
                    loadResult = JsonLoadResult.Loaded(text);
                }
                else
                {
                    loadResult = JsonLoadResult.NotFound(new FileNotFoundException());
                }

                if (!loadResult.Success)
                {
                    Log.TraceFormat(LogTag.Json, "Failed LoadJsonFromSave: name: '{0}' , expt: {1}", name, loadResult.Exception);
                    loadResult = JsonLoadResult.Failed();
                }

                return loadResult;

            }
            catch (Exception e)
            {
                Log.TraceErrorFormat(LogTag.Json, "Failed LoadXmlDocumentFromSave: name: '{0}', Exception: {1}", name, e);
                return JsonLoadResult.Failed();;
            }
        }

        private static string GetDocumentPath(string rootPath, string name) => Path.Combine(rootPath, name);

        private static string GetDefaultDataDirectory()
        {
            return Application.persistentDataPath;
        }

        public static string GetDefaultDocumentPath(string name) =>
            GetDocumentPath(GetDefaultDataDirectory(), name);

        public static JsonSaveResult SaveJsonContent(string content, string name) =>
            SaveJsonContentToDirectory(content, name, GetDefaultDataDirectory());

        public static JsonSaveResult SaveJsonElement<T>(JsonElement<T> jsonDoc) =>
            SaveJsonContentToDirectory(JsonUtility.ToJson(jsonDoc.SerilizedData), jsonDoc.Name, GetDefaultDataDirectory());

        public static JsonSaveResult SaveJsonContentToDirectory(string content, string name, string rootPath)
        {
            var dstPath = GetDocumentPath(rootPath, name);
            try
            {
                File.WriteAllText(dstPath, content);
                return JsonSaveResult.Saved();
            }
            catch (Exception e)
            {
                return JsonSaveResult.Failed(e);
            }
        }

        public static void DeleteSavedJsonDocument(string name) {
            DeleteSavedJsonDocumentInDirectory(name, GetDefaultDataDirectory());
        }

        private static void DeleteSavedJsonDocumentInDirectory(string name, string rootPath)
        {
            var path = GetDocumentPath(rootPath, name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
