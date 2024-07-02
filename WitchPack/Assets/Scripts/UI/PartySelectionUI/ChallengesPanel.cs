using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengesPanel : UIElement<LevelConfig, PartySelectionWindow>
{
    [SerializeField] private LevelChallenge[] _levelChallengeConfig;
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
            _challengeButtonsText[i].text = _levelChallengeConfig[i].DisplayName;
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
            
            _challengeBonusesText.text = GetBonusesDescriptions(_levelChallengeConfig[buttonIndex].BonusesDescription);
        }
        _levelConfig.SelectedChallenge = _levelChallengeConfig[buttonIndex];
        _partySelectionWindow.ReduceShamanSlots(_levelChallengeConfig[buttonIndex].ReduceShamanSlots);
    }

    public string GetBonusesDescriptions(string[] bonusesDescription)
    {
        string finalValue = "";
        foreach (var str in bonusesDescription)
        {
            finalValue += str;
            finalValue += "\n";
        }

        return finalValue;
    }
}