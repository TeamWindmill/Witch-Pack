public class HighImpactSmokeBomb : SmokeBomb
{
    private HighImpactSO Config;
    public HighImpactSmokeBomb(HighImpactSO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
    }

    protected override bool Cast(BaseUnit caster, BaseUnit target)
    {
        HighImpactSmokeBombMono highImpact = LevelManager.Instance.PoolManager.HighImpactPool.GetPooledObject();
        highImpact.transform.position = target.transform.position;
        highImpact.gameObject.SetActive(true);
        highImpact.SpawnBomb(this, caster);
        return true;
    }
}