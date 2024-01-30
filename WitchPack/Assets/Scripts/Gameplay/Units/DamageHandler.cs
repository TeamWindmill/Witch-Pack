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
        foreach (var item in flatMods)
        {
            if (item < 0)
            {
                Debug.LogError("ey");
            }
            amount += item;
        }
        foreach (var item in mods)
        {
            if (item == 0)
            {
                amount = 0;
                break;
            }
            else if (item > 1)
            {
                amount += (item * amount) - amount;//add damage
            }
            else
            {
                amount -= amount - (item * amount);//reduce damage
            }
        }
       
        return Mathf.RoundToInt(Mathf.Clamp(amount, 0, amount));
    }



}
