using System.Collections.Generic;
using UnityEngine;

public class DamageHandler
{
    private Stat _finalDamage;


    private bool hasPopupColor;
    private Color popupColor;

    public bool HasPopupColor { get => hasPopupColor; }
    public Color PopupColor { get => popupColor; }

    public DamageHandler(float baseAmount)
    {
        _finalDamage = new Stat(StatType.BaseDamage, baseAmount);
    }


    public void AddMultiplierMod(float mod)
    {
        _finalDamage.AddMultiplier(mod);
    }


    public void AddFlatMod(int flatMod)
    {
        _finalDamage.AddModifier(flatMod);
    }

    public int GetFinalDamage()
    {
        return _finalDamage.IntValue;
    }

    public void SetPopupColor(Color color)
    {
        hasPopupColor = true;
        popupColor = color;
    }

    public void SetNoPopupColor()
    {
        hasPopupColor = false;
    }

}
