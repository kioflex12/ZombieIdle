using Behavior.Gameplay;
using Core;

namespace Starter
{
    public abstract class CommonStarter : GameComponent
    {
    }

    public abstract class CommonStarter<T> : CommonStarter where T : CommonStarter<T>
    {
        protected override void Awake()
        {
            base.Awake();
            GameState.TryCreate();
        }
    }
}

