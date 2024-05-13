using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PenOnKillPiercingShot")]

public class ExperiencedHunterSO : PiercingShotSO
{
    [SerializeField] private int extraPenPerKill;
    [SerializeField] private int numberOfKillsRequiredToIncreasePierce;
    [SerializeField] private Color textPopupColor;

    public override void OnSetCaster(BaseUnit caster)
    {
        ExperiencedHunterCounter eventCounter = new ExperiencedHunterCounter(
                                                caster, this, ref caster.DamageDealer.OnKill, 
                                                numberOfKillsRequiredToIncreasePierce);
        eventCounter.OnCountIncrement += IncreasePen;
    }
    private void IncreasePen(AbilityEventCounter counter, Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbilitySO abilitySo)
    {
        dealer.Owner.Stats.AddValueToStat(StatType.AbilityProjectilePenetration, extraPenPerKill);
        // Play Sound
        SoundManager.Instance.PlayAudioClip(SoundEffectType.ExperiencedHunterLevelUp);
        // Popup
        LevelManager.Instance.PopupsManager.SpawnGeneralPopup("PIERCE UP+", textPopupColor, dealer.Owner.transform.position, yOffset: true);
    }
}
