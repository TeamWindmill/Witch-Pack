using System.Collections.Generic;
using UnityEngine;

public class RewardsPanel : UIElement
{
    [SerializeField] private LevelRewardUI _levelRewardUIPrefab;
    [SerializeField] private Transform _holder;

    private readonly List<LevelRewardUI> _activeLevelRewards = new();

    public void Init(LevelConfig levelConfig)
    {
        Hide();
        if (levelConfig.shamansToAddAfterComplete != null)
        {
            foreach (var shamanReward in levelConfig.shamansToAddAfterComplete)
            {
                var shamanRewardUI = Instantiate(_levelRewardUIPrefab, _holder);
                shamanRewardUI.Init(shamanReward.UnitIcon,shamanReward.Name);
                _activeLevelRewards.Add(shamanRewardUI);
            }
        }
    }

    public override void Hide()
    {
        if(_activeLevelRewards.Count <= 0) return;
        
        foreach (var activeLevelRewards in _activeLevelRewards)
        {
            Destroy(activeLevelRewards.gameObject); 
        }
        
        _activeLevelRewards.Clear();
    }
}
