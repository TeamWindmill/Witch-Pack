using System;

[System.Serializable]
public class Effectable
{
    private BaseUnit owner;
    public Action<Effectable, Affector, StatusEffect> OnAffected;
    public Action<StatusEffect> OnEffectRemoved;

    public BaseUnit Owner { get => owner;}

    public Effectable(BaseUnit owner)
    {
        this.owner = owner;
    }

}
