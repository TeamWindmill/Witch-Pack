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
    
    [SerializeField, TabGroup("Visual")] private ShamanAnimator shamanAnimator;
    [SerializeField] private ClickHelper clicker;
    [SerializeField] private Indicatable indicatable;
    [SerializeField] private GroundCollider groundCollider;
    private ShamanConfig shamanConfig;
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
        IntializeCastingHandlers();
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


    private void IntializeCastingHandlers()
    {
        foreach (var item in ShamanConfig.KnownAbilities)
        {
            knownAbilities.Add(item);
            castingHandlers.Add(new UnitCastingHandler(this, item));
        }
    }

    public void LearnAbility(BaseAbility ability)
    {
        knownAbilities.Add(ability);
        castingHandlers.Add(new UnitCastingHandler(this, ability));
    }

    public void RemoveAbility(UnitCastingHandler caster)
    {
        knownAbilities.Remove(caster.Ability);
        castingHandlers.Remove(caster);
    }

    //testing
    [ContextMenu("UpgradeTest")]
    public void UpgradeAbility()
    {
        BaseAbility ability = castingHandlers[0].Ability;
        RemoveAbility(castingHandlers[0]);
        LearnAbility(ability.Upgrades[0]);
    }

    private void SetSelectedShaman(PointerEventData.InputButton button)
    {
        if (button == PointerEventData.InputButton.Left)
        {
            if (!ReferenceEquals(LevelManager.Instance.SelectionManager.SelectedShaman, this))
            {
                LevelManager.Instance.SelectionManager.SetSelectedShaman(this,SelectionType.Movement);
            }
        }
        else if (button == PointerEventData.InputButton.Right)
        {
            if (!ReferenceEquals(LevelManager.Instance.SelectionManager.SelectedShaman, this))
            {
                LevelManager.Instance.SelectionManager.SetSelectedShaman(this,SelectionType.Info);
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
