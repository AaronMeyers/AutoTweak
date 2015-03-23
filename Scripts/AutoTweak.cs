using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

namespace AutoTweak {

	public class AutoTweak : MonoBehaviour {
		
		private static AutoTweak instance;
		public static AutoTweak Instance {
			get {
				if ( instance == null ) {
					instance = FindObjectOfType( typeof( AutoTweak ) ) as AutoTweak;
					
					if ( instance == null ) {
						return null;
					}
				}
				
				return instance;
			}
		}
	
		private Canvas canvas;
		public RectTransform rootPanel;
		public RectTransform tweakPanelHolder;
		public Button showButton;
		
		public GameObject dropDownPrefab;
		public GameObject tweakPanelPrefab;
		public GameObject tweakSliderPrefab;
		
		DropDown dropDownSelector;
		Dictionary<object, TweakPanel> tweakPanels;
		
		void Awake() {
			
		
			canvas = GetComponent<Canvas>();
			
			dropDownSelector = (Instantiate( dropDownPrefab ) as GameObject).GetComponent<DropDown>();
			dropDownSelector.GetComponent<RectTransform>().SetParent( rootPanel, false );
			dropDownSelector.GetComponent<RectTransform>().SetAsFirstSibling();
			dropDownSelector.dropDownButton.onClick.AddListener( DropDownExpanded );
			dropDownSelector.hideButton.onClick.AddListener( HidePanel );
			
			showButton.onClick.AddListener( ShowPanel );
			
			tweakPanels = new Dictionary<object, TweakPanel>();
			
			if ( EventSystem.current == null ) {
				canvas.gameObject.AddComponent<EventSystem>();
				canvas.gameObject.AddComponent<TouchInputModule>();
				canvas.gameObject.AddComponent<StandaloneInputModule>();
			}
		}
	
		public static void RegisterTweakableObject( object tweakee ) {
			
			Instance.SetupTweakableObject( tweakee );
			
		}
		
		void SetupTweakableObject( object tweakee ) {
			
			Type tweakType = tweakee.GetType();
			// add it to the drop down
			Button b = dropDownSelector.AddItem( tweakType.Name );
			// register callback to do stuff when its clicked
			b.onClick.AddListener( () => ButtonCallback( tweakee ) );
			
			// make a panel
			SetupTweakPanel( tweakee );
			
			if ( tweakPanels.Count == 1 ) {
				// make it active
				SetActivePanel( tweakee );
			}
		}
		
		void SetupTweakPanel( object target ) {
			
			TweakPanel panel = (Instantiate( tweakPanelPrefab ) as GameObject).GetComponent<TweakPanel>();
			panel.rectTransform.SetParent( tweakPanelHolder, false );
			
			// go through the fields and add the UI stuff
			Type type = target.GetType();
			panel.name = type.Name;
			
			foreach ( FieldInfo fi in type.GetFields( BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public ) )
			{
				object[] sliderAttributes = fi.GetCustomAttributes( typeof( SliderAttribute ), true );
				if ( sliderAttributes.Length > 0 )
				{
					SliderAttribute sliderAttribute = sliderAttributes[0] as SliderAttribute;
					
					TweakSlider slider = (Instantiate(tweakSliderPrefab) as GameObject).GetComponent<TweakSlider>();
					slider.rectTransform.SetParent( panel.content, false );
					slider.slider.minValue = sliderAttribute.Min;
					slider.slider.maxValue = sliderAttribute.Max;
					slider.SetTarget( target, fi );
				}
				
			}	
			tweakPanels.Add( target, panel );
		}
		
		void SetActivePanel( object target ) {
		
			dropDownSelector.activeLabel.text = target.GetType().Name;
		
			foreach ( KeyValuePair<object,TweakPanel> kvp in tweakPanels ) {
				
				kvp.Value.gameObject.SetActive( kvp.Key == target ? true : false );
			}
			tweakPanelHolder.gameObject.SetActive( true );
			tweakPanels[target].ResizeContent();
		}
		
		void HidePanel() {
			rootPanel.gameObject.SetActive( false );
		}
		
		void ShowPanel() {
			rootPanel.gameObject.SetActive( true );
		}
		
		void DropDownExpanded() {
			tweakPanelHolder.gameObject.SetActive( false );
		}
		
		void ButtonCallback( object target ) {
			dropDownSelector.Close();
			SetActivePanel( target );
		}
	}
}
