
namespace Utils.ProgressData
{
    public interface JsonNodeSerializable<T> : JsonNodeSaveable<T>, JsonNodeLoadable<T>
    {
    }
}