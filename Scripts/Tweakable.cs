using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using AutoTweak;

namespace AutoTweak {
	public class Tweakable : MonoBehaviour {
		
		public MonoBehaviour target;
		
		void Start() {
			AutoTweak.RegisterTweakableObject( target );	
		}
	}
}
