using System;


[System.Serializable]
public class Affector
{
    private BaseUnit owner;

    public Action<Effectable, Affector, StatusEffect> OnAffect;
    public BaseUnit Owner { get => owner; }
    public Affector(BaseUnit owner)
    {
        this.owner = owner;
    }

}
