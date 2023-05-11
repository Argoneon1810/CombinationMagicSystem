using UnityEngine;

public class ProjectilefiedMagic : MagicEffector
{
    [SerializeField] float strength = 100;
    public override void NotifyDoneCasting()
    {
        base.NotifyDoneCasting();
        GetComponent<Rigidbody>().AddForce(transform.forward * strength);
    }

    public void SetStrength(float value)
    {
        strength = value;   
    }
}