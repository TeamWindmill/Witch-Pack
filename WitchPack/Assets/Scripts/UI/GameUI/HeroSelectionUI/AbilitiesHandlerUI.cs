using System.Collections.Generic;
using UnityEngine;

public class AbilitiesHandlerUI : MonoBehaviour
{
    [SerializeField] private AbilityUI[] _abilityUIBlocks;
    
    public void Show(List<UnitCastingHandler> abilities)
    {
        foreach (var ability in abilities)
        {
            foreach (var uiBlock in _abilityUIBlocks)
            {
                if (uiBlock.IsActive) return;
                uiBlock.Init(ability);
                uiBlock.Show();
                break;
            }
        }
    }

    public void Hide()
    {
        foreach (var uiBlock in _abilityUIBlocks)
        {
            if (!uiBlock.IsActive) return;
            uiBlock.Hide();
        }
    }
}
