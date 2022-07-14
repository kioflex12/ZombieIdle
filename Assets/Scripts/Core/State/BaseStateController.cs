using Utils.Json;

namespace Core.State
{
    public abstract class BaseStateController
    {
        public abstract string Name { get; }

        protected GameState Owner { get; }

        protected BaseStateController(GameState owner) => Owner = owner;

        public abstract void Reset();

        public virtual void Update()
        {
        }

        public virtual void Init()
        {
        }

        public virtual void PostInit()
        {
        }

        public abstract void Load<T>(T serializedData) where T : ISerializationData;

        public virtual void PostLoad() {}


        public abstract void Save();
    }
}