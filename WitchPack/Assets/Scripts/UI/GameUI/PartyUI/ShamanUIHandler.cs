using UnityEngine;
using UnityEngine.UI;


public class ShamanUIHandler : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _splash;
    [SerializeField, Range(0, 1)] private float spriteDeathAlpha;
    private Shaman _shaman;


    public void SetShamanData(Shaman shaman)
    {
        _shaman = shaman;
        _splash.sprite = _shaman.ShamanConfig.UnitIcon;
    }

    private void GoToShaman() =>
        GameManager.Instance.CameraHandler.SetCameraPosition(_shaman.transform.position);

    public void Show()
    {
        _shaman.Damageable.OnGetHit += OnHealthChange;
        _shaman.Damageable.OnDeath += ShamanDeathUI;
        //OnClickEvent += GoToShaman;
    }

    private void OnHealthChange(Damageable arg1, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4, bool arg5)
    {
        float hpRatio = _shaman.Damageable.CurrentHp / _shaman.Damageable.MaxHp;
        _healthBar.value = hpRatio;
        _fill.color = Color.Lerp(Color.red, Color.green, hpRatio);
    }

    public void Hide()
    {
        _shaman.Damageable.OnGetHit -= OnHealthChange;
        _shaman.Damageable.OnDeath -= ShamanDeathUI;
        //OnClickEvent -= GoToShaman;
    }

    private void ShamanDeathUI(Damageable arg1, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4)
    {
        var lowAlphaColor = _splash.color;
        lowAlphaColor.a = spriteDeathAlpha;
        _splash.color = lowAlphaColor;
    }
}