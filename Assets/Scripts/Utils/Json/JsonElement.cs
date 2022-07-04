namespace Utils.Json
{
    public class JsonElement<T>
    {
        public string Name;
        public T Data;

        public JsonElement(string name, T data)
        {
            Name = name;
            Data = data;
        }
    }
}

