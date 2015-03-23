using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AutoTweak
{
	public class TweakPanel : MonoBehaviour {
	
		public RectTransform rectTransform;
		public RectTransform content;
		
		public void ResizeContent() {
			
			float totalHeight = 0;
			foreach ( LayoutElement le in content.GetComponentsInChildren<LayoutElement>() ) {
				totalHeight += le.preferredHeight;
			}
			
			content.sizeDelta = new Vector2( content.sizeDelta.x, totalHeight );
			
			float maxHeight = transform.parent.GetComponent<RectTransform>().rect.size.y;
			rectTransform.sizeDelta = new Vector2( rectTransform.sizeDelta.x, Mathf.Min( maxHeight, totalHeight ) );
			
			if ( content.sizeDelta.y <= rectTransform.sizeDelta.y ) {
				rectTransform.GetComponent<ScrollRect>().vertical = false;
			}
			
			if ( maxHeight == 0.0f ) {
				// weird hack because sometimes maxHeight is 0 because of something with the order of Layout scripts?
				StartCoroutine( RetryResize() );
			}
		}
		
		IEnumerator RetryResize() {
			
			yield return new WaitForSeconds( .01f );
			
			ResizeContent();
			
		}
		
	}

}