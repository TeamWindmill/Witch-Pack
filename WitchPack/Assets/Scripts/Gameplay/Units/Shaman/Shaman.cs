using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shaman : BaseUnit
{
    public override StatSheet BaseStats => shamanConfig.BaseStats;
    public ShamanConfig ShamanConfig => shamanConfig;
    public List<BaseAbility> KnownAbilities => knownAbilities;
    public List<UnitCastingHandler> CastingHandlers => castingHandlers;
    public bool MouseOverShaman => clicker.IsHover;
    public List<BaseAbility> RootAbilities => rootAbilities;

    [SerializeField, TabGroup("Visual")] private ShamanAnimator shamanAnimator;
    [SerializeField] private ClickHelper clicker;
    [SerializeField] private Indicatable indicatable;
    [SerializeField] private GroundCollider groundCollider;
    private ShamanConfig shamanConfig;
    private List<BaseAbility> rootAbilities = new List<BaseAbility>();
    private List<BaseAbility> knownAbilities = new List<BaseAbility>();
    private List<UnitCastingHandler> castingHandlers = new List<UnitCastingHandler>();

    private void OnValidate()
    {
        shamanAnimator ??= GetComponentInChildren<ShamanAnimator>();
    }

    public override void Init(BaseUnitConfig baseUnitConfig)
    {
        shamanConfig = baseUnitConfig as ShamanConfig;
        base.Init(shamanConfig);
        Targeter.SetRadius(Stats.BonusRange);
        Stats.OnStatChanged += Targeter.AddRadius;
        IntializeAbilities();
        Movement.OnDestenationSet += DisableAttacker;
        Movement.OnDestenationReached += EnableAttacker;
        shamanAnimator.Init(this);
        clicker.OnClick += SetSelectedShaman;
        groundCollider.Init(this);
        indicatable.Init(shamanConfig.UnitIcon);
    }

    private void OnShamanSelect()
    {
        SlowMotionManager.Instance.StartSlowMotionEffects();
        HeroSelectionUI.Instance.Show(this);
    }

    private void OnShamanDeselect()
    {
        SlowMotionManager.Instance.EndSlowMotionEffects();
        HeroSelectionUI.Instance.Hide();
    }


    private void IntializeAbilities()
    {
        foreach (var rootAbility in shamanConfig.RootAbilities)
        {
            rootAbilities.Add(rootAbility);
            foreach (var upgrade in rootAbility.GetUpgrades())
            {
                upgrade.ChangeUpgradeState(AbilityUpgradeState.Locked);
            }
            rootAbility.ChangeUpgradeState(AbilityUpgradeState.Open);
        }
        foreach (var ability in ShamanConfig.KnownAbilities)
        {
            ability.UpgradeAbility();
            knownAbilities.Add(ability);
            if (ability is not Passive)
            {
                ability.OnSetCaster(this);
                castingHandlers.Add(new UnitCastingHandler(this, ability));
            }
            else
            {
                (ability as Passive).SubscribePassive(this);
            }
        }
    }

    public void LearnAbility(BaseAbility ability)
    {
        knownAbilities.Add(ability);
        if (ability is not Passive)
        {
            ability.OnSetCaster(this);
            castingHandlers.Add(new UnitCastingHandler(this, ability));
        }
        else
        {
            (ability as Passive).SubscribePassive(this);
        }
    }

    public void RemoveAbility(BaseAbility ability)
    {
        if (ability is not Passive)
        {
            knownAbilities.Remove(ability);
            castingHandlers.Remove(GetCasterFromAbility(ability));
        }
    }

    public void UpgradeAbility(BaseAbility ability, BaseAbility upgrade)
    {
        RemoveAbility(ability);
        LearnAbility(upgrade);
    }


    public UnitCastingHandler GetCasterFromAbility(BaseAbility givenAbiltiy)
    {
        for (int i = 0; i < castingHandlers.Count; i++)
        {
            if (ReferenceEquals(castingHandlers[i].Ability, givenAbiltiy))
            {
                return castingHandlers[i];
            }
        }
        //Debug.LogError("Attempted to retrive a non existing caster");
        return null;
    }
    public BaseAbility GetActiveAbilityFromRoot(BaseAbility rootAbility)
    {
        if (KnownAbilities.Contains(rootAbility)) return rootAbility;
        
        var upgrades = rootAbility.GetUpgrades();
        foreach (var upgrade in upgrades)
        {
            if (KnownAbilities.Contains(upgrade)) return upgrade;
        }
        return null;
    }

    private void SetSelectedShaman(PointerEventData.InputButton button)
    {
        if (button == PointerEventData.InputButton.Left)
        {
            if (!ReferenceEquals(LevelManager.Instance.SelectionManager.SelectedShaman, this))
            {
                LevelManager.Instance.SelectionManager.SetSelectedShaman(this, SelectionType.Movement);
            }
        }
        else if (button == PointerEventData.InputButton.Right)
        {
            if (!ReferenceEquals(LevelManager.Instance.SelectionManager.SelectedShaman, this))
            {
                LevelManager.Instance.SelectionManager.SetSelectedShaman(this, SelectionType.Info);
            }
        }
    }
    public void ToggleClicker(bool state)
    {
        clicker.enabled = state;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        clicker.OnClick -= SetSelectedShaman;
        Movement.OnDestenationSet -= DisableAttacker;
        Movement.OnDestenationReached -= EnableAttacker;
    }

}
