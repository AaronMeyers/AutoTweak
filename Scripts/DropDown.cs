using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AutoTweak {

	public class DropDown : MonoBehaviour {
	
		public Button dropDownButton;
		public RectTransform dropDownScrollRect;
		public RectTransform dropDownScrollContent;
		public Text activeLabel;
		public Button hideButton;
		
		public GameObject itemPrefab;
	
	
		void Start() {
			
			RectTransform rt = GetComponent<RectTransform>();
			rt.anchorMax = Vector2.up;
			rt.anchorMin = Vector2.up;
		
			ResizeScrollContent();
			dropDownScrollRect.gameObject.SetActive( false );
		}
		
		void Update() {
			if ( Input.GetKeyDown( KeyCode.A ) ) {
			
				RectTransform parentRT = transform.parent.GetComponent<RectTransform>();
				Debug.Log( parentRT.name );
				Debug.Log( parentRT.rect.size.y );
			}
		}
		
		public void DropDownButtonClicked() {
			dropDownButton.gameObject.SetActive( false );
			dropDownScrollRect.gameObject.SetActive( true );
			ResizeScrollContent();
		}
		
		public void Close() {
			dropDownButton.gameObject.SetActive( true );
			dropDownScrollRect.gameObject.SetActive( false );
		}
		
		public Button AddItem( string label ) {
			DropDownItem item = (Instantiate( itemPrefab ) as GameObject).GetComponent<DropDownItem>();
			item.rectTransform.SetParent( dropDownScrollContent, false );
			item.label.text = label;
			
			if ( dropDownScrollContent.childCount > 1 ) {
				// only need to drop down if more than one thing is added
				dropDownButton.enabled = true;
			}
			
			return item.button;
		}
	
		void ResizeScrollContent() {
		
		
			float totalHeight = 0;
			foreach ( LayoutElement le in dropDownScrollContent.GetComponentsInChildren<LayoutElement>() ) {
				totalHeight += le.preferredHeight;
			}
			
			dropDownScrollContent.sizeDelta = new Vector2( dropDownScrollContent.sizeDelta.x, totalHeight );
			
			
			float maxHeight = transform.parent.GetComponent<RectTransform>().rect.size.y;
			dropDownScrollRect.sizeDelta = new Vector2( dropDownScrollRect.sizeDelta.x, Mathf.Min( maxHeight, totalHeight ) );
		}
	}

}
