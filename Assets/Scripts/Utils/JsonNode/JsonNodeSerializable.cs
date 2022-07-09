// ReSharper disable InconsistentNaming

namespace Utils.JsonNode
{
    public interface JsonNodeSerializable<T> : JsonNodeSaveable<T>, JsonNodeLoadable<T>
    {
    }
}