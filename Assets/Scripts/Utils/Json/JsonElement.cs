namespace Utils.Json
{
    public class JsonElement<T>
    {
        public string Name;
        public T SerilizedData;

        public JsonElement(string name, T serilizedData)
        {
            Name = name;
            SerilizedData = serilizedData;
        }
    }
}

