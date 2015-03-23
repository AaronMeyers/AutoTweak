using UnityEngine;
using System;
using System.Collections;

namespace AutoTweak {

	[AttributeUsage(AttributeTargets.All)]	
	public class SliderAttribute : Attribute {
	
		float min;
		public float Min
		{
			get
			{
				return min;
			}
		}
		
		float max;
		public float Max
		{
			get
			{
				return max;
			}
		}
	
		public SliderAttribute( float min, float max )
		{
			this.min = min;
			this.max = max;
		}
		
	}

}