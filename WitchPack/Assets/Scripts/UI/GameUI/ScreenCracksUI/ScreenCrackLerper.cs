using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ScreenCrackLerper : EffectTransitionLerp<CracksValueLerp>
{
    public Image Image => _image;

    [SerializeField] private Image _image;

    public void SetImage(Image image)
    {
        _image = image;
    }
    public void SetStartValue()
    {
        foreach (var effectValue in EffectValues)
        {
            _image.material.SetFloat("_Offset_X",effectValue.StartValue);
        }
    }

    protected override void SetValue(CracksValueLerp type, float value)
    {
        _image.material.SetFloat("_Offset_X",value);
    }
}
public enum CracksValueLerp
{
    OffsetX,
}