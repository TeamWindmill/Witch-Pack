using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShamanIconUI : UIElement
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _splash;

    public void Init(ShamanConfig shaman)
    {
        _name.text = shaman.Name;
        _splash.sprite = shaman.UnitIcon;
    }
}
