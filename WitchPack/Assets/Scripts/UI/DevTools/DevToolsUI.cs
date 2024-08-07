using UnityEngine;

public class DevToolsUI : UIElement
{
    [SerializeField] private Transform _holder;
    public override void Show() => _holder.gameObject.SetActive(true);
    public override void Hide() => _holder.gameObject.SetActive(false);

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _holder.gameObject.SetActive(!_holder.gameObject.activeSelf);
        }
    }

    public void EnergyGain()
    {
        PartyEnergyHandler.AddEnergy(1000);
    }

    public void HealCore()
    {
        LevelManager.Instance.CurrentLevel.CoreTemple.Damageable.Heal(500);
    }
    public void InvincibleCore(bool state)
    {
        LevelManager.Instance.CurrentLevel.CoreTemple.Damageable.ToggleHitable(!state);
    }

    public void HealShamans()
    {
        foreach (var shaman in LevelManager.Instance.ShamanParty)
        {
            shaman.Damageable.Heal(500);
        }
    }

    public void WinLevel()
    {
        LevelManager.Instance.EndLevel(true);
    }
}