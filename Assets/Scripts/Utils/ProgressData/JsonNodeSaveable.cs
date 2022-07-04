// ReSharper disable InconsistentNaming
// ReSharper disable UnusedTypeParameter
// ReSharper disable IdentifierTypo

namespace Utils.ProgressData
{
    public interface JsonNodeSaveable<T>
    {
        void Save(T jsonNode);
    }
}