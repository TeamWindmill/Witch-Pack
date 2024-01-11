using Sirenix.OdinInspector;
using UnityEngine;

public class CoreTemple : MonoBehaviour
{
    [SerializeField, TabGroup("Combat")] private Damageable damageable;
    
    public Damageable Damageable { get => damageable; }

}
