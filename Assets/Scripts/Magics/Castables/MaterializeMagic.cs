using Unity.VisualScripting;
using UnityEngine;

public class MaterializeMagic : CastableMagic
{
    [SerializeField] Mesh shape;
    [SerializeField] Material material;

    public override void Affect(FormulatedMagic target)
    {
        var mf = target.TryGetComponent(out MeshFilter meshFilter) ? meshFilter : target.gameObject.AddComponent<MeshFilter>();
        mf.mesh = shape;
        var mr = target.TryGetComponent(out MeshRenderer meshRenderer) ? meshRenderer : target.gameObject.AddComponent<MeshRenderer>();
        mr.material = material;
        var rb = target.TryGetComponent(out Rigidbody rigidbody) ? rigidbody : target.gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        var mm = target.TryGetComponent(out MaterializedMagic materializedMagic) ? materializedMagic : target.AddComponent<MaterializedMagic>();
        target.SetValidity(target.IndexOf(mm), true);
    }
}
