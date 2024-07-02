using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengesPanel : UIElement<LevelConfig, PartySelectionWindow>
{
    public LevelChallenge[] LevelChallengeConfigs => _levelChallengeConfigs;

    [SerializeField] private LevelChallenge[] _levelChallengeConfigs;
    [SerializeField] private Button[] _challengeButtons;
    [SerializeField] private TextMeshProUGUI[] _challengeButtonsText;
    [SerializeField] private TextMeshProUGUI _challengeBonusesText;

    private LevelConfig _levelConfig;
    private PartySelectionWindow _partySelectionWindow;
    private int _selectedChallengeIndex;

    public override void Init(LevelConfig levelConfig, PartySelectionWindow partySelectionWindow)
    {
        _levelConfig = levelConfig;
        _partySelectionWindow = partySelectionWindow;
        for (int i = 0; i < _challengeButtons.Length; i++)
        {
            _challengeButtonsText[i].text = _levelChallengeConfigs[i].DisplayName;
        }

        SelectChallenge(0);
    }

    public void SelectChallenge(int buttonIndex)
    {
        _selectedChallengeIndex = buttonIndex;
        for (int i = 0; i < _challengeButtons.Length; i++)
        {
            _challengeButtons[i].interactable = true;
            if(i == buttonIndex) _challengeButtons[buttonIndex].interactable = false;
            
            _challengeBonusesText.text = GetBonusesDescriptions(_levelChallengeConfigs[buttonIndex].BonusesDescription);
        }
        _levelConfig.SelectedChallenge = _levelChallengeConfigs[buttonIndex];
        _partySelectionWindow.ReduceShamanSlots(_levelChallengeConfigs[buttonIndex].ReduceShamanSlots);
    }

    public string GetBonusesDescriptions(string[] bonusesDescription)
    {
        string finalValue = "";
        foreach (var str in bonusesDescription)
        {
            finalValue += "-" + str;
            finalValue += "\n";
        }

        return finalValue;
    }
}