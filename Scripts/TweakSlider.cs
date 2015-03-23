using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Reflection;

namespace AutoTweak {

	public class TweakSlider : MonoBehaviour {
	
		public Text label;
		public Text readout;
		public Slider slider;
		public RectTransform rectTransform;
		
		object target;
		FieldInfo fieldInfo;
		
		void Start() {
			float poop = 1;
			slider.onValueChanged.AddListener( OnValueChanged );
		}
		
		void OnValueChanged( float value ) {
			readout.text = slider.value.ToString( "0.00" );
			fieldInfo.SetValue( target, slider.value );
		}
		
		public void SetTarget( object target, FieldInfo fi ) {
			this.target = target;
			fieldInfo = fi;
			
			label.text = fi.Name;
			slider.value = (float)fieldInfo.GetValue( target );
			readout.text = slider.value.ToString( "0.00" );
		}
		
		void Update()
		{
			if ( target != null ) {
				float value = (float)fieldInfo.GetValue( target );
				if ( value != slider.value ) {
					slider.value = value;
				}
			}
			
		}
	}

}