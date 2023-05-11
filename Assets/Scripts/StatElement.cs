using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "New Stat Element", menuName = "Stat Element/New Stat Element")]
public class StatElement : ScriptableObject
{
    public string statName;
    public int delayPoint;
    public int failPoint;
}
