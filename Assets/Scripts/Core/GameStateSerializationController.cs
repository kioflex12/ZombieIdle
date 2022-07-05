
using UnityEngine;
using Utils.Json;
using Utils.ProgressData;

namespace Core
{
    public class GameStateSerializationController : JsonNodeSerializable<GameStateSerializationController>
    {
        public int TestValue { get; private set; }

        public GameStateSerializationController Load(JsonElement<GameStateSerializationController> jsonElement)
        {
            TestValue = jsonElement.SerilizedData.TestValue;
            return this;
        }

        public void Save(JsonElement<GameStateSerializationController> jsonElement)
        {
        }
    }
}


