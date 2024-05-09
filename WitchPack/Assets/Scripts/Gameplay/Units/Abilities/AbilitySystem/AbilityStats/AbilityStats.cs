
using System;

[Serializable]
public class AbilityStatInt : AbilityStat<int>
{
    protected override int GetStatValue()
    {
        var value = StatValue;
        foreach (var modifier in _modifiers)
        {
            value += modifier;
        }

        return value;
    }
}
[Serializable]
public class AbilityStatFloat : AbilityStat<float>
{
    protected override float GetStatValue()
    {
        var value = StatValue;
        foreach (var modifier in _modifiers)
        {
            value += modifier;
        }

        return value;
    }
}