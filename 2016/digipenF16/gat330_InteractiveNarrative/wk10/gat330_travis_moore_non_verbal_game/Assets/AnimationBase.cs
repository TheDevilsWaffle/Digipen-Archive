using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationBase : MonoBehaviour
{
    virtual protected void Awake() { }
    virtual protected void Start() { }
    virtual public void ButtonActive(Image _bImage, Text _bText, Color _tColor, Color _bColor, Vector3 _originalPos, Vector3 _originalScale, Quaternion _originalRotation) { }
    virtual public void ButtonInactive(Image _bImage, Text _bText, Color _tColor, Color _bColor, Vector3 _originalPos, Vector3 _originalScale, Quaternion _originalRotation) { }
    virtual public void ButtonOnHover(Image _bImage, Text _bText, Color _tColor, Color _bColor, Vector3 _originalPos, Vector3 _originalScale, Quaternion _originalRotation) { }
    virtual public void ButtonDisabled(Image _bImage, Text _bText, Color _tColor, Color _bColor, Vector3 _originalPos, Vector3 _originalScale, Quaternion _originalRotation) { }
}
