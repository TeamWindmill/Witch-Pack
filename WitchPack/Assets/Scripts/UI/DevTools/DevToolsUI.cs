using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevToolsUI : UIElement
{
    [SerializeField] private Transform _holder;
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _holder.gameObject.SetActive(!_holder.gameObject.activeSelf);
        }
    }

    public void LevelUp()
    {
        if (LevelManager.Instance.SelectionHandler.SelectedShaman != null)
            LevelManager.Instance.SelectionHandler.SelectedShaman.EnergyHandler.ManualGainEnergy();
    }

    public void HealCore()
    {
        LevelManager.Instance.CurrentLevel.CoreTemple.Heal(500);
    }

    public void HealShamans()
    {
        foreach (var shaman in LevelManager.Instance.ShamanParty)
        {
            shaman.Damageable.Heal(500);
        }
    }
}