using System;
using TMPro;
using UnityEngine;

public class CounterUIElement : UIElement
{
    [SerializeField] private TextMeshProUGUI currentValueText;
    [SerializeField] private TextMeshProUGUI maxValueText;

    public virtual void Init(int maxValue, int currentValue = -1)
    {
        maxValueText.text = maxValue.ToString();
        currentValueText.text = currentValue == -1 ? maxValue.ToString() : currentValue.ToString();
        Show();
    }
    public sealed override void Show()
    {
        base.Show();
    }

    protected void UpdateUIData(int value)
    {
        currentValueText.text = value.ToString();
    }
}
