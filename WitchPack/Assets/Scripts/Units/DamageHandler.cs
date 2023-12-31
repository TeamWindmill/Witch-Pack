using System.Collections.Generic;
using UnityEngine;

public class DamageHandler
{
    private float baseAmount;
    private List<float> mods = new List<float>();
    private List<int> flatMods = new List<int>();


    public DamageHandler(float baseAmount)
    {
        this.baseAmount = baseAmount;
    }


    public void AddMod(float mod)
    {
        mods.Add(mod);
    }


    public void AddFlatMod(int flatMod)
    {
        flatMods.Add(flatMod);
    }

    public int GetFinalDamage()
    {
        float amount = baseAmount;
        foreach (var item in mods)
        {
            if (item == 0)
            {
                amount = 0;
                break;
            }
            else if (item >= 1)
            {
                amount += (item * baseAmount) - baseAmount;//add damage
            }
            else
            {
                amount -= baseAmount - (item * baseAmount);//reduce damage
            }
        }
        foreach (var item in flatMods)
        {
            if (item < 0)
            {
                Debug.LogError("ey");
            }
            amount += item;
        }//need to speak to game design about how they want the damage to be calculated. this is for now
        return Mathf.RoundToInt(Mathf.Clamp(amount, 0, amount));
    }



}
