using UnityEngine;
using Utils.GameComponentAttributes;

namespace Behavior.Gameplay
{
    public class CraftingPlace : GameComponent
    {
        [SerializeField] [NotNull] private GameObject _test;

        protected override void Init()
        {
        }

        protected override void DeInit()
        {
        }
        }
}
