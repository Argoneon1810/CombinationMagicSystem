using System.Collections.Generic;
using UnityEngine;

public class MemorizeMagic : CastableMagic
{
    int rank = 5;
    List<CastableMagic> slots = new List<CastableMagic> { null, null, null, null, null };

    public void CastMagic(MagicCaster caster)
    {
        GameObject formulatedMagicBody = new GameObject();
        FormulatedMagic fm = formulatedMagicBody.AddComponent<FormulatedMagic>();
        fm.AssignCaster(caster);
        foreach (var slot in slots)
            if (slot != null)
                slot.Affect(fm);
    }
}