using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : BaseUnit
{
    [SerializeField, TabGroup("Combat")] private List<BaseAbility> abilities = new List<BaseAbility>();
    [SerializeField, TabGroup("Combat")] private EnemyTargeter enemyTargeter;


    private float lastCast;


    public List<BaseAbility> Abilities { get => abilities; }
    public EnemyTargeter EnemyTargeter { get => enemyTargeter; }



    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("trigger");
    }


    [ContextMenu("CastTestAbility")]
    public void CastTestAbility()
    {
        if (Time.time - lastCast >= abilities[0].Cd)
        {
            abilities[0].CastAbility(this);
            lastCast = Time.time;
        }
    }
}
