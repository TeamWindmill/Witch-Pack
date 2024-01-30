using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shaman : BaseUnit
{
    public override StatSheet BaseStats => shamanConfig.BaseStats;
    public ShamanConfig ShamanConfig => shamanConfig;
    public List<BaseAbility> ActiveAbilities => activeAbilities;
    public List<BaseAbility> RootAbilities => rootAbilities;
    public List<UnitCastingHandler> CastingHandlers => castingHandlers;
    public bool MouseOverShaman => clicker.IsHover;

    [SerializeField, TabGroup("Visual")] private ShamanAnimator shamanAnimator;
    [SerializeField] private ClickHelper clicker;
    [SerializeField] private Indicatable indicatable;
    [SerializeField] private GroundCollider groundCollider;
    private ShamanConfig shamanConfig;
    
    private List<BaseAbility> rootAbilities = new List<BaseAbility>();
    private List<BaseAbility> activeAbilities = new List<BaseAbility>();
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
        foreach (var ability in ShamanConfig.KnownAbilities)
        {
            rootAbilities.Add(ability);
            activeAbilities.Add(ability);
            castingHandlers.Add(new UnitCastingHandler(this, ability));
        }
    }

    public void LearnAbility(BaseAbility ability)
    {
        activeAbilities.Add(ability);
        castingHandlers.Add(new UnitCastingHandler(this, ability));
    }

    public void RemoveAbility(BaseAbility ability)
    {
        activeAbilities.Remove(ability);
        castingHandlers.Remove(GetCasterFromAbility(ability));
    }

    //testing
    [ContextMenu("UpgradeTest")]
    public void UpgradeAbility(BaseAbility caster, BaseAbility upgrade)
    {
        RemoveAbility(caster);
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
        Debug.LogError("Attempted to retrive a non existing caster");
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

    private void OnDisable()
    {
        clicker.OnClick -= SetSelectedShaman;
        Movement.OnDestenationSet -= DisableAttacker;
        Movement.OnDestenationReached -= EnableAttacker;
    }

}
