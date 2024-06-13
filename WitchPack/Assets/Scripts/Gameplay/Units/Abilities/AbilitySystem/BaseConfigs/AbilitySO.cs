using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    [BoxGroup("General Settings/left/Details",centerLabel: true)]
    [LabelWidth(50)][VerticalGroup("General Settings/left")]
    [HorizontalGroup("General Settings")]
    [SerializeField] private string _name;
    
    [BoxGroup("General Settings/left/Details",centerLabel: true)]
    [TextArea(4, 14)][VerticalGroup("General Settings/left")]
    [HorizontalGroup("General Settings")]
    [SerializeField] private string discription;
    
    [PreviewField(63)]
    [BoxGroup("General Settings/Icons",centerLabel: true)]
    [HorizontalGroup("General Settings")]
    [SerializeField] private Sprite defaultIcon;
    
    [PreviewField(63)]
    [BoxGroup("General Settings/Icons",centerLabel: true)]
    [HorizontalGroup("General Settings")]
    [SerializeField] private Sprite disabledIcon;
    
    [PreviewField(63)]
    [BoxGroup("General Settings/Icons",centerLabel: true)]
    [HorizontalGroup("General Settings")]
    [SerializeField] private Sprite upgradeIcon;
    
    [HorizontalGroup("General Settings")][VerticalGroup("General Settings/left")]
    [BoxGroup("General Settings/left/Skill Tree")][SerializeField] private AbilitySO[] _upgrades;
    
    [BoxGroup("Popup Numbers")][SerializeField] private bool hasPopupColor;
    [BoxGroup("Popup Numbers")][SerializeField, ShowIf(nameof(hasPopupColor))] private Color popupColor;

    public bool HasPopupColor { get => hasPopupColor; }
    public Color PopupColor { get => popupColor; }
    public Sprite DefaultIcon => defaultIcon;
    public Sprite DisabledIcon => disabledIcon;
    public Sprite UpgradeIcon => upgradeIcon;
    public string Name => _name;
    public string Discription => discription;
    public AbilitySO[] Upgrades => _upgrades;

    
    public List<AbilitySO> GetUpgrades()
    {
        var upgrades = new List<AbilitySO>();
        foreach (var upgrade in Upgrades)
        {
            upgrades.Add(upgrade);
            foreach (var secondUpgrade in upgrade.Upgrades)
            {
                upgrades.Add(secondUpgrade);
            }
        }

        return upgrades;
    }

}