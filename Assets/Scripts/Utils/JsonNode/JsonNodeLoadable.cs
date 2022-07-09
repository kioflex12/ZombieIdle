// ReSharper disable InconsistentNaming

using Utils.Json;

namespace Utils.JsonNode
{
    public interface JsonNodeLoadable<T>
    {
        T Load(JsonElement<T> jsonElement);
    }
}

