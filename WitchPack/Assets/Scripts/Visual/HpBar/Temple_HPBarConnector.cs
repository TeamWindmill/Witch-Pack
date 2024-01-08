using UnityEngine;


public class Temple_HPBarConnector : MonoBehaviour
{
    [SerializeField] HP_Bar hP_Bar;
    
    //IEntityHealthComponent healthComponent; //TBF after IEntityHealthComponent has its own method for subbing to an OnValueChanged
    [SerializeField] CoreTemple coreTemple;
    
    private void Start()
    {
        hP_Bar.Init(coreTemple.Damageable.MaxHp);
    }
    
    private void OnEnable()
    {
        coreTemple.Damageable.OnGetHit += SetBarToHealth;
    }

    private void SetBarToHealth(Damageable arg1, DamageDealer damageDealer, DamageHandler damageHandler, BaseAbility arg4, bool arg5)
    {
        hP_Bar.SetBarValue(damageHandler.GetFinalDamage());
    }

    private void OnDisable()
    {
        coreTemple.Damageable.OnGetHit -= SetBarToHealth;
    }
}