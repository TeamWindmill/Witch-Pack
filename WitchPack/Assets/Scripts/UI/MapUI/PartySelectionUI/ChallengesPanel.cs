using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengesPanel : UIElement<LevelConfig, PartySelectionWindow>
{

    [SerializeField] private Button[] _challengeButtons;
    [SerializeField] private TextMeshProUGUI[] _challengeButtonsText;
    [SerializeField] private TextMeshProUGUI _challengeBonusesText;

    private LevelConfig _levelConfig;
    private PartySelectionWindow _partySelectionWindow;

    public override void Init(LevelConfig levelConfig, PartySelectionWindow partySelectionWindow)
    {
        _levelConfig = levelConfig;
        _partySelectionWindow = partySelectionWindow;
        
        if (_levelConfig.LevelChallenges.Length == 0)
        {
            Debug.LogError("missing level challenges in config");
            return;
        }
        for (int i = 0; i < _challengeButtons.Length; i++)
        {
            _challengeButtonsText[i].text = levelConfig.LevelChallenges[i].DisplayName;
        }

        SelectChallenge(0);
    }

    public void SelectChallenge(int buttonIndex)
    {
        for (int i = 0; i < _challengeButtons.Length; i++)
        {
            _challengeButtons[i].interactable = true;
            if(i == buttonIndex) _challengeButtons[buttonIndex].interactable = false;
            
            _challengeBonusesText.text = GetBonusesDescriptions(_levelConfig.LevelChallenges[buttonIndex].BonusesDescription);
        }
        _levelConfig.SelectedChallenge = _levelConfig.LevelChallenges[buttonIndex];
        _partySelectionWindow.ReduceShamanSlots(_levelConfig.LevelChallenges[buttonIndex].ReduceShamanSlots);
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