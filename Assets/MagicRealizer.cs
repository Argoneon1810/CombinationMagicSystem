using UnityEngine;

public class MagicRealizer : MonoBehaviour
{
    private static MagicRealizer _instance;
    public static MagicRealizer Instance => _instance;

    private void Awake()
    {
        if(_instance)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public void RealizeMagic(MemorizeMagic memorizeMagic, MagicCaster caster)
    {
        //if (!memorizeMagic.IsValid()) return;     //better doing it, but it will cost resource so quite a controversy

        GameObject realizedMagic = new GameObject();
        realizedMagic.transform.position = caster.CastingPosition();
        foreach(var constructor in memorizeMagic.magicConstructors)
            AddComponentOf(realizedMagic, constructor);
    }

    private void AddComponentOf(GameObject target, MagicConstructor constructor)
    {
        if (constructor.magicName.Equals("Projectilefy"))
            target.AddComponent<ProjectilefyMagicExtra>();
        else if (constructor.magicName.Equals("Rigidbodyfy"))
            target.AddComponent<RigidbodyfyMagicExtra>();
    }
}