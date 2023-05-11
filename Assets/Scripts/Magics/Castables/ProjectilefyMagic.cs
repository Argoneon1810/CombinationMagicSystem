using UnityEngine;

public class ProjectilefyMagic : CastableMagic
{
    [SerializeField] float strength = 100;

    public override void Affect(FormulatedMagic target)
    {
        ProjectilefiedMagic pm = target.TryGetComponent(out ProjectilefiedMagic projectilefiedMagic) ? projectilefiedMagic : target.gameObject.AddComponent<ProjectilefiedMagic>();
        pm.SetStrength(strength);
        if(target.HasRequisite(typeof(MaterializedMagic)))
        {
            target.SetValidity(target.IndexOf(pm), true);
            return;
        }
        else
        {
            target.SetValidity(target.IndexOf(pm), false);
            Destroy(pm);
        }
    }
}