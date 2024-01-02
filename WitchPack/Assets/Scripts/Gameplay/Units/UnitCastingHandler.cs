using UnityEngine;

public class UnitCastingHandler : MonoBehaviour
{
    //manage cds, send abilities to cast -> specific interactions come from the so itself? 

    [SerializeField] private Shaman owner;

    //testing
    public void CastAbility(BaseAbility ability)
    {

    }


    private void Update()
    {
        if (!ReferenceEquals(owner.EnemyTargeter.GetClosestTarget(), null))
        {
            owner.CastTestAbility();
        }
    }


}
