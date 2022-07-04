using System;
using JetBrains.Annotations;

namespace Utils.Json
{
    public class JsonSaveResult : CustomResult
    {
        public JsonSaveResult(bool success, [CanBeNull] Exception exception) : base(success, exception)
        {
        }

        public static JsonSaveResult Saved() => new JsonSaveResult(true, null);

        public static JsonSaveResult Failed(Exception exception) => new JsonSaveResult(false, exception);
    }
}

