using System.Collections.Generic;

[System.Serializable]
public class MemorizeMagic
{
    public int capacity;
    public int occupancy;
    public List<MagicConstructor> magicConstructors = new List<MagicConstructor>();
    public HashSet<MagicConstructor> banned = new HashSet<MagicConstructor>();

    public static implicit operator bool(MemorizeMagic self) => self != null;

    public MemorizeMagic() { }
    public MemorizeMagic(int capacity) => SetCapacity(capacity);

    public void SetCapacity(int capacity)
    {
        this.capacity = capacity;
    }

    public bool TryAdd(MagicConstructor toAdd, out List<MagicConstructor> requisites)
    {
        if (toAdd.slotOccupation > capacity - occupancy)
        {
            requisites = null;
            return false;
        }
        if (banned.Contains(toAdd))
        {
            requisites = null;
            return false;
        }

        magicConstructors.Add(toAdd);
        occupancy += toAdd.slotOccupation;
        foreach (var ban in toAdd.bans)
            banned.Add(ban);

        if (toAdd.requisites.Count > 0)
        {
            requisites = toAdd.requisites;
            return true;
        }

        requisites = null;
        return true;
    }

    public bool IsValid()
    {
        HashSet<MagicConstructor> allRequisites = new HashSet<MagicConstructor>();
        foreach (var constructor in magicConstructors)
            if (constructor.requisites != null)
                foreach (var requisite in constructor.requisites)
                    allRequisites.Add(requisite);
        foreach (var constructor in magicConstructors)
            if (allRequisites.Contains(constructor))
                allRequisites.Remove(constructor);
        if (allRequisites.Count > 0)
            return false;
        return true;
    }

    public bool IsEmpty() => occupancy == 0 && capacity == 0 && (banned == null || banned.Count == 0) && (magicConstructors == null || magicConstructors.Count == 0);
}
