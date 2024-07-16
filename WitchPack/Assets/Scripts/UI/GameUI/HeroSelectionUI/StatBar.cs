using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : UIElement
{
    public StatBarType StatBarType => _statBarType;

    [SerializeField] private StatBarType _statBarType;
    [SerializeField] private TextMeshProUGUI _statBarName;
    [SerializeField] private TextMeshProUGUI _statBarMaxValue;
    [SerializeField] private TextMeshProUGUI _statBarValue;
    [SerializeField] private Image _statBarFill;

    private StatBarData _data;
    public void Init(StatBarData statBarData)
    {
        _data = statBarData;
        _statBarName.text = $"{statBarData.Name}";
        _statBarMaxValue.text = statBarData.MaxValue.ToString();
        _statBarValue.text = statBarData.Value.ToString();
        _statBarFill.fillAmount = (float)statBarData.Value / statBarData.MaxValue;
        Show();
    }
    public void UpdateStatbar(int currentValue,int maxValue)
    {
        _data.Value = currentValue;
        _data.MaxValue = maxValue;
        _statBarValue.text = currentValue.ToString();
        _statBarMaxValue.text = maxValue.ToString();
        _statBarFill.fillAmount = (float)currentValue / maxValue;

    }
     public void UpdateStatbarValue(int currentValue)
     {
         _data.Value = currentValue;
         _statBarValue.text = currentValue.ToString();
         _statBarFill.fillAmount = (float)currentValue / _data.MaxValue;
     }
     public void UpdateStatbarMaxValue(int maxValue)
     {
         _data.MaxValue = maxValue;
         _statBarMaxValue.text = maxValue.ToString();
         _statBarFill.fillAmount = (float)_data.Value / maxValue;
     }
     public void UpdateStatbarText(string Text)
     {
         _statBarName.text = Text;
     }
}

public enum StatBarType
{
    Health,
    Energy,
    Exp,
}
public struct StatBarData
{
    public string Name;
    public int Value;
    public int MaxValue;

    public StatBarData(string name, int value, int maxValue)
    {
        Name = name;
        Value = value;
        MaxValue = maxValue;
    }
}