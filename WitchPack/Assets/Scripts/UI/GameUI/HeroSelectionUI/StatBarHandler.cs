using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatBarHandler : MonoBehaviour
{
    [SerializeField] private StatBarType statBarType;
    [SerializeField] private TextMeshProUGUI _statBarName;
    [SerializeField] private TextMeshProUGUI _statBarBaseValue;
    [SerializeField] private TextMeshProUGUI _statBarValue;
    [SerializeField] private Image _statBarFill;

    private Shaman _shaman;

    public StatBarType StatBarType => statBarType;

     public void Init(Shaman shaman)
     {
         _shaman = shaman;
         string name = "";
         int baseValue = 0;
         int currentValue = 0;
         switch (statBarType)
         {
             case StatBarType.HealthBar:
                name = "Health:";
                baseValue = shaman.Damageable.MaxHp;
                currentValue = shaman.Damageable.CurrentHp;
                shaman.Damageable.OnGetHit += UpdateStatBarHealth;
                shaman.Damageable.OnHeal += UpdateStatBarHealthBasedOnShaman;
                break;
             case StatBarType.EnergyBar:
                 name = "Energy:";
                 baseValue = shaman.EnergyHandler.MaxEnergyToNextLevel;
                 currentValue = shaman.EnergyHandler.CurrentEnergy;
                 shaman.EnergyHandler.OnShamanGainEnergy += UpdateStatbarEnergy;
                 break;
         }

         _statBarName.text = name;
         _statBarBaseValue.text = baseValue.ToString();
         _statBarValue.text = currentValue.ToString();
         _statBarFill.fillAmount = (float)currentValue / baseValue;
     }

     private void UpdateStatbarEnergy(int currentEnergy, int maxEnergy)
     {
         _statBarBaseValue.text = maxEnergy.ToString();
         _statBarValue.text = currentEnergy.ToString();
         _statBarFill.fillAmount = (float)currentEnergy / maxEnergy;
     }

     public void UpdateStatBarHealth(Damageable damageable, DamageDealer arg2, DamageHandler arg3, BaseAbilitySO arg4, bool arg5)
     {
         var currentHP = damageable.CurrentHp;
         var maxHP = damageable.MaxHp;
         _statBarValue.text = currentHP.ToString();
        _statBarBaseValue.text = maxHP.ToString();
         _statBarFill.fillAmount = (float)currentHP / maxHP;
     }

    public void UpdateStatBarHealthBasedOnShaman(Damageable damageable, float uselessAmount)
    {
        UpdateStatBarHealth(damageable, null, null, null, false);
    }
    
     public void Hide()
     {
         switch (statBarType)
         {
             case StatBarType.HealthBar:
                _shaman.Damageable.OnGetHit -= UpdateStatBarHealth;
                _shaman.Damageable.OnHeal -= UpdateStatBarHealthBasedOnShaman;
                break;
             case StatBarType.EnergyBar:
                 _shaman.EnergyHandler.OnShamanGainEnergy -= UpdateStatbarEnergy;
                 break;
             default:
                 throw new ArgumentOutOfRangeException();
         }
         
     }
}

public enum StatBarType
{
    HealthBar,
    EnergyBar
}