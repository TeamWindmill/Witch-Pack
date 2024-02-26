using UnityEngine;
using UnityEngine.UI;


public class ShamanUIHandler : ClickableUIElement
{
    [SerializeField] private Image _fill;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _splash;
    [SerializeField] private Image _frame;
    [SerializeField, Range(0, 1)] private float spriteDeathAlpha;
    [Space] 
    [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;
    
    private Shaman _shaman;


    public void Init(Shaman shaman)
    {
        _shaman = shaman;
        _splash.sprite = _shaman.ShamanConfig.UnitIcon;
        _healthBar.value = 1;
        _frame.sprite = shaman.EnergyHandler.HasSkillPoints ? upgradeReadyFrameSprite : defaultFrameSprite;
        shaman.EnergyHandler.OnShamanUpgrade += OnShamanUpgrade;
        shaman.EnergyHandler.OnShamanLevelUp += OnShamanLevelUp;
        shaman.Damageable.OnGetHit += OnHealthChange;
        shaman.Damageable.OnDeath += ShamanDeathUI;
        OnClickEvent += GoToShaman;
        Show();
    }

    private void OnHealthChange(Damageable arg1, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4, bool arg5)
    {
        float hpRatio = _shaman.Damageable.CurrentHp / _shaman.Damageable.MaxHp;
        _healthBar.value = hpRatio;
        _fill.color = Color.Lerp(Color.red, Color.green, hpRatio);
    }

    public override void Hide()
    {
        _shaman.Damageable.OnGetHit -= OnHealthChange;
        _shaman.Damageable.OnDeath -= ShamanDeathUI;
        OnClickEvent -= GoToShaman;
        base.Hide();
    }

    private void ShamanDeathUI(Damageable arg1, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4)
    {
        _frame.sprite = defaultFrameSprite;
        var lowAlphaColor = _splash.color;
        lowAlphaColor.a = spriteDeathAlpha;
        _splash.color = lowAlphaColor;
    }

    private void OnShamanLevelUp(int obj) => _frame.sprite = upgradeReadyFrameSprite;
    
    private void OnShamanUpgrade(bool hasSkillPoints) => 
        _frame.sprite = hasSkillPoints ? upgradeReadyFrameSprite : defaultFrameSprite;

    private void GoToShaman()
    {
        if(_shaman.Damageable.IsDead) return;
        GameManager.Instance.CameraHandler.SetCameraPosition(_shaman.transform.position);
    }
        
}