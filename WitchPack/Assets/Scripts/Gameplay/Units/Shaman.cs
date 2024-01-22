using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shaman : BaseUnit
{
    [SerializeField, TabGroup("Visual")] private ShamanAnimator shamanAnimator;
    [SerializeField] private ClickHelper clicker;
    [SerializeField] private Indicatable indicatable;
    [SerializeField] private GroundCollider groundCollider;
    private ShamanConfig shamanConfig;
    private List<BaseAbility> knownAbilities = new List<BaseAbility>();
    private List<UnitCastingHandler> castingHandlers = new List<UnitCastingHandler>();
    public override StatSheet BaseStats => shamanConfig.BaseStats;
    public ShamanConfig ShamanConfig { get => shamanConfig; }
    public List<BaseAbility> KnownAbilities { get => knownAbilities; }
    public List<UnitCastingHandler> CastingHandlers { get => castingHandlers; }
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

    public void RemoveAbility(BaseAbility ability)
    {
        knownAbilities.Remove(ability);
        foreach (var item in castingHandlers)
        {
            if (ReferenceEquals(item.Ability, ability))
            {
                castingHandlers.Remove(item);
                break;
            }
        }
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

    private void OnDisable()
    {
        clicker.OnClick -= SetSelectedShaman;
        Movement.OnDestenationSet -= DisableAttacker;
        Movement.OnDestenationReached -= EnableAttacker;
    }

}
