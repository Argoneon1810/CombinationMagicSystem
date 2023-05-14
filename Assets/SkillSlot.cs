using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 한번에 하나만 만들 것
/// </summary>
public class SkillSlot : MonoBehaviour
{
    [SerializeField] List<MemorizeMagic> magics;

    [SerializeField] MagicCaster caster;
    [SerializeField] MagicRealizer realizer;
    [SerializeField] bool debugCastMagic = false;
    [SerializeField] int debugCastSlotNumber = 0;

    private void Start()
    {
        realizer = MagicRealizer.Instance;
    }

    private void Update()
    {
        if(debugCastMagic.Trigger())
        {
            DebugCastMagic(debugCastSlotNumber);
        }
    }

    public void AddMagic(MemorizeMagic magic)
    {
        magics.Add(magic);
    }

    public void DebugCastMagic(int debugCastSlotNumber)
    {
        if(TryGetMagic(debugCastSlotNumber, out MemorizeMagic magicToCast))
            realizer.RealizeMagic(magicToCast, caster);
    }

    public bool TryGetMagic(int index, out MemorizeMagic memorizeMagic)
    {
        if (magics.Count - 1 >= debugCastSlotNumber)
        {
            memorizeMagic = magics[debugCastSlotNumber];
            return true;
        }
        memorizeMagic = null;
        return false;
    }
}
