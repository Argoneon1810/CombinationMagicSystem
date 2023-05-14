using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "New Magic Constructor", menuName = "Magic Constructor")]
public class MagicConstructor : ScriptableObject
{
    public string magicName;
    public int slotOccupation;
    public List<MagicConstructor> bans;
    public List<MagicConstructor> requisites;
}
