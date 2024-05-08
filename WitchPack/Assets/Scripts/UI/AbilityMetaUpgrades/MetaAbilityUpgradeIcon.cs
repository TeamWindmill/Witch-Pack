using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetaAbilityUpgradeIcon : UIElement
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _amount;

    public void Init()
    {
        _name.text = "";
        _amount.text = "";
    }
}
