using UnityEngine;

namespace Attributes
{
	/// <summary>
	/// This attribute does not work for generics
	/// </summary>
	public class Name : PropertyAttribute
	{
		#region Fields & Properties

		public string name;

		#endregion

		public Name(string name)
		{
			this.name = name;
		}
	}
}
