using System;
using System.Collections.Generic;
using UnityEngine;
public class FormulatedMagic : MonoBehaviour
{
    [SerializeField] private MagicCaster caster;
    [SerializeField] List<MagicEffector> effectors = new List<MagicEffector>();
    [SerializeField] List<bool> validityFlags = new List<bool>();

    public bool isValid()
    {
        foreach (var validityFlag in validityFlags)
            if (!validityFlag)
                return false;
        return true;
    }

    public void AssignCaster(MagicCaster caster)
    {
        this.caster = caster;
        transform.LookAt(transform.position + caster.transform.forward * 100, caster.transform.up);
    }

    public bool HasRequisite(Type type)
    {
        foreach (var effector in effectors)
            if (effector.GetType().Equals(type))
                return true;
        return false;
    }

    public int IndexOf(MagicEffector effector)
    {
        return effectors.IndexOf(effector);
    }

    public void SetValidity(int index, bool value)
    {
        validityFlags[index] = value;
    }
}