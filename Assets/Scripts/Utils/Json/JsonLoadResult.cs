using System;
using JetBrains.Annotations;
using Utils;
using Utils.GameComponentAttributes;
using Utils.Json;

public sealed class JsonLoadResult : CustomResult
{
    public JsonLoadResultType Type { get; }

    private string _doc;

    public string Document => _doc;

    public JsonLoadResult(JsonLoadResultType type, string doc, [CanBeNull] Exception exception) : base(type == JsonLoadResultType.Loaded, exception)
    {
        Type = type;
        _doc = doc;
    }

    public static JsonLoadResult Loaded([NotNullOrEmpty] string doc) =>
        new JsonLoadResult(JsonLoadResultType.Loaded, doc, null);

    public static JsonLoadResult Failed([CanBeNull] Exception exception = null) =>
        new JsonLoadResult(JsonLoadResultType.Failed, string.Empty, exception);

    public static JsonLoadResult NotFound([CanBeNull] Exception exception = null) =>
        new JsonLoadResult(JsonLoadResultType.NotFound, string.Empty, exception);

    public static JsonLoadResult InvalidJson([CanBeNull] Exception exception = null) =>
        new JsonLoadResult(JsonLoadResultType.InvalidJson, string.Empty, exception);
}
