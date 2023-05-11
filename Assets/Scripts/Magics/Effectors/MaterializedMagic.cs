using UnityEngine;

public class MaterializedMagic : MagicEffector
{
    public override void NotifyDoneCasting()
    {
        base.NotifyDoneCasting();
        GetComponent<Rigidbody>().isKinematic = false;
    }
}