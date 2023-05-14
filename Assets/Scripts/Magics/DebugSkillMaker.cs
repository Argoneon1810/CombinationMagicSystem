using System.Collections.Generic;
using UnityEngine;

public class DebugSkillMaker : MonoBehaviour
{
    [SerializeField] SkillSlot skillSlot;
    [SerializeField] int debugSlotSize = 3;
    [SerializeField] bool debugCreateSkill = false;

    [SerializeField] MemorizeMagic tempMagic;
    [SerializeField] bool inProgress;

    [SerializeField] MagicConstructor firstToAdd, secondToAdd;
    [SerializeField] bool debugAddFirst, debugAddSecond;

    [SerializeField] bool debugCheckValidity, debugFinalizeMagic;

    private void Update()
    {
        if(debugCreateSkill.Trigger())
        {
            if (!inProgress)
            {
                inProgress = true;
                tempMagic = new(debugSlotSize);
            }
        }
        if (debugAddFirst.Trigger())
            TryAdd(firstToAdd);
        if (debugAddSecond.Trigger())
            TryAdd(secondToAdd);
        if (debugCheckValidity.Trigger())
            Debug.Log("temp magic being created is " + (tempMagic.IsValid() ? "valid" : "invalid"));
        if (debugFinalizeMagic.Trigger())
        {
            if (tempMagic.IsEmpty())
                Debug.Log("you are trying to save an uneditted / empty magic");
            else if (!tempMagic.IsValid())
                Debug.Log("you are trying to save an invalid magic");
            else
            {
                skillSlot.AddMagic(tempMagic);
                tempMagic = null;
                inProgress = false;
            }
        }
    }

    void TryAdd(MagicConstructor toAdd)
    {
        if (!tempMagic.TryAdd(toAdd, out List<MagicConstructor> requisites))
            OnAddFailed(toAdd.magicName);
        else
            if(requisites != null)
                foreach (var requisite in requisites)
                    TryAdd(requisite);
    }

    void OnAddFailed(string magicName) => Debug.Log(magicName + " is either banned, overweighted or already added");
}