using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PenOnKillPiercingShot")]

public class PenOnKillPeircing : PiercingShot
{
    [SerializeField] private int extraPenPerKill;
    [SerializeField] private int numberOfKillsRequiredToIncreasePierce;
    [SerializeField] private Color popupColor;

    public override void OnSetCaster(BaseUnit caster)
    {
        ExperiencedHunterCounter eventCounter = new ExperiencedHunterCounter(
                                                caster, this, ref caster.DamageDealer.OnKill, 
                                                numberOfKillsRequiredToIncreasePierce);
        eventCounter.OnCountIncrement += IncreasePen;
    }
    private void IncreasePen(AbilityEventCounter counter, Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability)
    {
        dealer.Owner.Stats.AddValueToStat(StatType.AbilityProjectilePenetration, extraPenPerKill);
        // Play Sound
        SoundManager.Instance.PlayAudioClip(SoundEffectType.ExperiencedHunterLevelUp);
        // Popup
        LevelManager.Instance.PopupsManager.SpawnGeneralPopup("PIERCE UP+", popupColor, dealer.Owner.transform.position, yOffset: true);
    }
}
