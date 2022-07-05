// ReSharper disable InconsistentNaming
// ReSharper disable UnusedTypeParameter
// ReSharper disable IdentifierTypo

using Utils.Json;

namespace Utils.ProgressData
{
    public interface JsonNodeSaveable<T>
    {
        void Save(JsonElement<T> jsonElement);
    }
}