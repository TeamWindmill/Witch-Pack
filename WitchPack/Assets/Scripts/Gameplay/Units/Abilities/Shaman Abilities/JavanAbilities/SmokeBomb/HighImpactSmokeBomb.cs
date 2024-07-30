public class HighImpactSmokeBomb : SmokeBomb
{
    private HighImpactSO Config;
    public HighImpactSmokeBomb(HighImpactSO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
    }

    protected override bool Cast(BaseUnit caster, IDamagable target)
    {
        HighImpactSmokeBombMono highImpact = PoolManager.GetPooledObject<HighImpactSmokeBombMono>();
        highImpact.transform.position = target.GameObject.transform.position;
        highImpact.gameObject.SetActive(true);
        highImpact.SpawnBomb(this, caster);
        return true;
    }
}