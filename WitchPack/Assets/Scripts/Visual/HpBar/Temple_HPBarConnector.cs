using UnityEngine;


public class Temple_HPBarConnector : MonoBehaviour
{
    [SerializeField] HP_Bar hP_Bar;
    
    //IEntityHealthComponent healthComponent; //TBF after IEntityHealthComponent has its own method for subbing to an OnValueChanged
    [SerializeField] CoreTemple coreTemple;
    
    private void Start()
    {
        hP_Bar.Init(coreTemple.MaxHp);
    }
    
    private void OnEnable()
    {
        coreTemple.OnGetHit += SetBarToHealth;
    }

    private void SetBarToHealth(int amount)
    {
        hP_Bar.SetBarValue(amount);
    }

    private void OnDisable()
    {
        coreTemple.OnGetHit -= SetBarToHealth;
    }
}