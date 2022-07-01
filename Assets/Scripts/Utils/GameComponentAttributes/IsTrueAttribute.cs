using System;

namespace Utils.GameComponentAttributes {
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IsTrueAttribute : BaseGameComponentAttribute {
		public IsTrueAttribute(bool checkPrefab = true) : base(checkPrefab) {}
	}
}