using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShamanUIHandler : ClickableUIElement
{
    [SerializeField] private Image _fill;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _splash;
    [SerializeField] private Image _upgradeFrame;
    [SerializeField, Range(0, 1)] private float spriteDeathAlpha;
    [Space] 
    [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;
    [SerializeField] private Image redInjuryImage;

    private Shaman _shaman;


    public void Init(Shaman shaman)
    {
        _shaman = shaman;
        _splash.sprite = _shaman.ShamanConfig.UnitIcon;
        _healthBar.value = 1;
        Color upgradeColor = _upgradeFrame.color;
        if (shaman.EnergyHandler.HasSkillPoints)
            upgradeColor.a = 100;
        else
            upgradeColor.a = 0;
        _upgradeFrame.color = upgradeColor;
        shaman.EnergyHandler.OnShamanUpgrade += OnShamanUpgrade;
        shaman.EnergyHandler.OnShamanLevelUp += OnShamanLevelUp;
        shaman.Damageable.OnGetHit += OnChangeHealth;
        shaman.Damageable.OnHeal += OnChangeHealth;
        shaman.Damageable.OnDeath += ShamanDeathUI;
        OnClickEvent += GoToShaman;
        OnClickEvent += ShowShamanInfo;
        Show();
    }

    private void OnChangeHealth()
    {
        float hpRatio = _shaman.Damageable.CurrentHp / (float)_shaman.Damageable.MaxHp;
        redInjuryImage.fillAmount = 1 - hpRatio;
        _healthBar.value = hpRatio;
        _fill.color = Color.Lerp(Color.red, Color.green, hpRatio);
    }

    private void OnChangeHealth(Damageable arg1, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4, bool arg5)
    {
        OnChangeHealth();
    }

    private void OnChangeHealth(Damageable arg1, float healAmount)
    {
        OnChangeHealth();
    }

    public override void Hide()
    {
        _shaman.Damageable.OnGetHit -= OnChangeHealth;
        _shaman.Damageable.OnHeal -= OnChangeHealth;
        _shaman.Damageable.OnDeath -= ShamanDeathUI;
        OnClickEvent -= GoToShaman;
        OnClickEvent -= ShowShamanInfo;
        base.Hide();
    }

    private void ShamanDeathUI(Damageable arg1, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4)
    {
        Color upgradeColor = _upgradeFrame.color;
        upgradeColor.a = 0;
        _upgradeFrame.color = upgradeColor;
        var lowAlphaColor = _splash.color;
        lowAlphaColor.a = spriteDeathAlpha;
        _splash.color = lowAlphaColor;
    }

    private void OnShamanLevelUp(int obj)
    { 
       Color upgradeColor = _upgradeFrame.color;
        upgradeColor.a = 100;
        _upgradeFrame.color = upgradeColor;
    }

    private void OnShamanUpgrade(bool hasSkillPoints)
    {
        Color upgradeColor = _upgradeFrame.color;
        if (hasSkillPoints)
            upgradeColor.a = 100;
        else
            upgradeColor.a = 0;
        _upgradeFrame.color = upgradeColor;
    }

    private void GoToShaman(PointerEventData pointerData)
    {
        if(_shaman.IsDead) return;
        GameManager.Instance.CameraHandler.SetCameraPosition(_shaman.transform.position);
    }
    private void ShowShamanInfo(PointerEventData pointerData)
    {
        _shaman.SetSelectedShaman(pointerData.button);
    }
}