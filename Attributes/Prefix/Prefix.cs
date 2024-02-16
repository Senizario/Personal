using UnityEngine;

namespace Attributes
{
	/// <summary>
	/// This attribute does not work for generics
	/// </summary>
	public class Prefix : PropertyAttribute
	{
		#region Fields & Properties

		public string prefix;

		#endregion

		public Prefix(string prefix)
		{
			this.prefix = prefix;
		}
	}
}
