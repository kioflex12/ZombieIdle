namespace Utils.GameComponentAttributes
{
	public sealed class NotNullOrEmptyAttribute : BaseGameComponentAttribute
	{
		public readonly bool AllowNullNodes;

		public NotNullOrEmptyAttribute() : this(false)
		{
		}

		public NotNullOrEmptyAttribute(bool allowNullNodes = false, bool checkPrefab = true) : base(checkPrefab) => AllowNullNodes = allowNullNodes;
	}
}