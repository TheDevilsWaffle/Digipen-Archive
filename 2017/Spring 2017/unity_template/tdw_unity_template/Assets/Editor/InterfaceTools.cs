using UnityEditor;
using UnityEngine;

public class InterfaceTools : MonoBehaviour
{
    [MenuItem("Custom/Set Anchors to Bounds %]")]
	static void SetAnchorsToBounds()
    {
        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            RectTransform _rectTransform = Selection.transforms[i] as RectTransform;
			RectTransform _parentTransform = Selection.activeTransform.parent as RectTransform;

			if(_rectTransform == null || _parentTransform == null) return;
			
			Vector2 _newAnchorsMin = new Vector2(_rectTransform.anchorMin.x + _rectTransform.offsetMin.x / _parentTransform.rect.width,
			                                    _rectTransform.anchorMin.y + _rectTransform.offsetMin.y / _parentTransform.rect.height);
			Vector2 _newAnchorsMax = new Vector2(_rectTransform.anchorMax.x + _rectTransform.offsetMax.x / _parentTransform.rect.width,
			                                    _rectTransform.anchorMax.y + _rectTransform.offsetMax.y / _parentTransform.rect.height);

			_rectTransform.anchorMin = _newAnchorsMin;
			_rectTransform.anchorMax = _newAnchorsMax;
			_rectTransform.offsetMin = _rectTransform.offsetMax = new Vector2(0, 0);
		}
	}
    [MenuItem("Custom/Set Bounds to Anchors %[")]
    static void SetBoundsToAnchors()
    {
        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            RectTransform _rectTransform = Selection.transforms[i] as RectTransform;

            if (_rectTransform == null) return;

            _rectTransform.offsetMin = _rectTransform.offsetMax = new Vector2(0, 0);
        }
    }
}
