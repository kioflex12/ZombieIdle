using Core;
using Core.State;
using Utils;
using Utils.Logger;

namespace Behavior.Gameplay.Controllers
{
    public class ZombieController : BaseStateController
    {
        public override string Name { get; }

        public int ZombiesCount { get; private set; }

        public ZombieController(GameState owner) : base(owner)
        {
        }

        public override void Reset()
        {
        }

        public override void Load<T>(T serializedData)
        {
            if (serializedData is GameStateSerializationData serializationData)
            {
                ZombiesCount = serializationData.ZombiesCount;
            }
            else
            {
                Log.TraceError(LogTag.State,$"Load failed: Invalid type T: {typeof(T)}");
            }
        }

        public override void Save()
        {
            Owner.SerializationData.ZombiesCount = ZombiesCount;
        }


    }
}
