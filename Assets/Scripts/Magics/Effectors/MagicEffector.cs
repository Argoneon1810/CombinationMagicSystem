using UnityEngine;

public class MagicEffector : MonoBehaviour
{
    protected bool startAction = false;
    public virtual void NotifyDoneCasting()
    {
        startAction = true;
    }
}