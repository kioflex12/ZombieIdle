namespace Utils.Json
{
    public class JsonElement<T>
    {
        public readonly string Name;
        public readonly T SerializedData;

        public JsonElement(string name, T serializedData)
        {
            Name = name;
            SerializedData = serializedData;
        }
    }
}

