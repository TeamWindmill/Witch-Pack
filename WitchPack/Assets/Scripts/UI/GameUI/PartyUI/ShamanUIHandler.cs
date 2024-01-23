using UnityEngine;
using UnityEngine.UI;


public class ShamanUIHandler : ClickableUIElement
{
    [SerializeField] private Image _fill;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _splash;
    [SerializeField, Range(0, 1)] private float spriteDeathAlpha;
    private Shaman _shaman;


    public void Init(Shaman shaman)
    {
        _shaman = shaman;
        _splash.sprite = _shaman.ShamanConfig.UnitIcon;
        _healthBar.value = 1;
        Show();
    }

    private void GoToShaman() =>
        GameManager.Instance.CameraHandler.SetCameraPosition(_shaman.transform.position);

    public override void Show()
    {
        base.Show();
        _shaman.Damageable.OnGetHit += OnHealthChange;
        _shaman.Damageable.OnDeath += ShamanDeathUI;
        OnClickEvent += GoToShaman;
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
        var lowAlphaColor = _splash.color;
        lowAlphaColor.a = spriteDeathAlpha;
        _splash.color = lowAlphaColor;
    }
}