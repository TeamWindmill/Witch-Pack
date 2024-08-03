using UnityEngine;

public class EnergyParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private ShamanTargeter particleCollider;
    [SerializeField] private float magnetSpeed;
    [SerializeField] private int energyValue;
    private bool _shamanInRadius;
    private Shaman _shamanTarget;

    private void Awake()
    {
        particleCollider.OnTargetAdded += PopEnergyParticle;
    }

    private void PopEnergyParticle(Shaman shaman)
    {
        Debug.Log(energyValue +" energy gained");
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_shamanInRadius) return;
        if (other.TryGetComponent<Shaman>(out var shaman))
        {
            _shamanInRadius = true;
            _shamanTarget = shaman;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Shaman>(out var shaman))
        {
            if (shaman != _shamanTarget) return;
            _shamanInRadius = false;
            _shamanTarget = null;
        }
    }

    private void Update()
    {
        if (_shamanInRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, _shamanTarget.transform.position, magnetSpeed * Time.deltaTime);
        }
    }
}
