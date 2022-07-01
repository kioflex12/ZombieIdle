using System;

namespace Utils.GameComponentAttributes
{
    [AttributeUsage(AttributeTargets.Field)]

    public sealed class NotNullAttribute : BaseGameComponentAttribute
    {
        public NotNullAttribute() : this(true) { }

        public NotNullAttribute(bool checkPrefab = true) : base(checkPrefab)
        {
        }
    }
}

