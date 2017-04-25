using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonAnimations : AnimationBase
{
    /// <summary>
    /// ButtonActive()
    /// </summary>
    /// <param name="_tColor"></param>
    /// <param name="_bColor"></param>
    /// <param name="_originalPos"></param>
    /// <param name="_originalScale"></param>
    /// <param name="_originalRotation"></param>
    public override void ButtonActive(Image _bImage, Text _bText, Color _tColor, Color _bColor, Vector3 _originalPos, Vector3 _originalScale, Quaternion _originalRotation)
    {
        base.ButtonActive(_bImage, _bText, _tColor, _bColor, _originalPos, _originalScale, _originalRotation);

        //code here
    }

    /// <summary>
    /// ButtonInactive()
    /// </summary>
    /// <param name="_tColor"></param>
    /// <param name="_bColor"></param>
    /// <param name="_originalPos"></param>
    /// <param name="_originalScale"></param>
    /// <param name="_originalRotation"></param>
    public override void ButtonInactive(Image _bImage, Text _bText, Color _tColor, Color _bColor, Vector3 _originalPos, Vector3 _originalScale, Quaternion _originalRotation)
    {
        base.ButtonInactive(_bImage, _bText, _tColor, _bColor, _originalPos, _originalScale, _originalRotation);

        //code here

    }

    /// <summary>
    /// ButtonOnHover()
    /// </summary>
    /// <param name="_tColor"></param>
    /// <param name="_bColor"></param>
    /// <param name="_originalPos"></param>
    /// <param name="_originalScale"></param>
    /// <param name="_originalRotation"></param>
    public override void ButtonOnHover(Image _bImage, Text _bText, Color _tColor, Color _bColor, Vector3 _originalPos, Vector3 _originalScale, Quaternion _originalRotation)
    {
        base.ButtonOnHover(_bImage, _bText, _tColor, _bColor, _originalPos, _originalScale, _originalRotation);
        //code here
        //_bText.color = _tColor;
    }

    /// <summary>
    /// ButtonDisabled()
    /// </summary>
    /// <param name="_tColor"></param>
    /// <param name="_bColor"></param>
    /// <param name="_originalPos"></param>
    /// <param name="_originalScale"></param>
    /// <param name="_originalRotation"></param>
    public override void ButtonDisabled(Image _bImage, Text _bText, Color _tColor, Color _bColor, Vector3 _originalPos, Vector3 _originalScale, Quaternion _originalRotation)
    {
        base.ButtonDisabled(_bImage, _bText, _tColor, _bColor, _originalPos, _originalScale, _originalRotation);

        //code here
    }
}
