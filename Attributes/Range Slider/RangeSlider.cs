using UnityEngine;

namespace Attributes
{ 
	public class RangeSlider : PropertyAttribute
	{
        #region Fields & Properties

        public float min;
		public float max;

		#endregion

		public RangeSlider(float min, float max)
		{
			this.min = min;
			this.max = max;
		}
	}
}
