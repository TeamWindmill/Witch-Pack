using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Shaman : BaseUnit
{
    [SerializeField, TabGroup("Combat")] private EnemyTargeter enemyTargeter;

    protected override void Awake()
    {
        base.Awake();
        enemyTargeter.SetRadius(Stats.BonusRange);
        Stats.OnStatChanged += enemyTargeter.AddRadius;
    }


    public EnemyTargeter EnemyTargeter { get => enemyTargeter; }

}
