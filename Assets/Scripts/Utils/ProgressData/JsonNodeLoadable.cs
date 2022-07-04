// ReSharper disable InconsistentNaming

using Utils.Json;

namespace Utils.ProgressData
{
    public interface JsonNodeLoadable<T>
    {
        T Load(JsonElement<T> jsonElement);
    }
}

