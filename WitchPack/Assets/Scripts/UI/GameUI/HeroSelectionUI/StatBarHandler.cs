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

    private BaseUnit _shaman;

    public StatBarType StatBarType => statBarType;

     public void Init(Shaman shaman)
     {
         _shaman = shaman;
         string name;
         int baseValue = 0;
         int currentValue = 0;
         switch (statBarType)
         {
             case StatBarType.HealthBar:
                 name = "Health:";
                 baseValue = shaman.Damageable.MaxHp;
                 currentValue = shaman.Damageable.CurrentHp;
                 shaman.Damageable.OnGetHit += UpdateStatBarHealth;
                 break;
             case StatBarType.EnergyBar:
                 name = "Energy:";
                 break;
             default:
                 throw new ArgumentOutOfRangeException();
         }

         _statBarName.text = name;
         _statBarBaseValue.text = baseValue.ToString();
         _statBarValue.text = currentValue.ToString();
         _statBarFill.fillAmount = currentValue / baseValue;
     }

     public void UpdateStatBarHealth(Damageable damageable, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4, bool arg5)
     {
         var currentHP = damageable.CurrentHp;
         var maxHP = damageable.MaxHp;
         _statBarValue.text = currentHP.ToString();
         _statBarFill.fillAmount = currentHP / maxHP;
     }
    
     public void Hide()
     {
         switch (statBarType)
         {
             case StatBarType.HealthBar:
                 _shaman.Damageable.OnGetHit -= UpdateStatBarHealth;
                 break;
             case StatBarType.EnergyBar:
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