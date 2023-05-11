using System;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public event Action ValueChangeCallback;
    
    [SerializeField]
    private List<StatElement> statElements = new List<StatElement>();

    [ExecuteAlways]
    private void Start()
    {
        if(statElements.Count == 0)
        {
            statElements.Add(new StatElement("Insight", 1, OnValueChanged));
            statElements.Add(new StatElement("Creativity", 1, OnValueChanged));
            statElements.Add(new StatElement("Certainty", 1, OnValueChanged));
            statElements.Add(new StatElement("Doubt", 1, OnValueChanged));
            statElements.Add(new StatElement("Calm", 1, OnValueChanged));
        }
        OnValueChanged();
    }

    public Pair<string, float>[] GetStats()
    {
        Pair<string, float>[] statsPairArray = new Pair<string, float>[statElements.Count];
        int i = 0;
        foreach(StatElement elem in statElements)
            statsPairArray[i++] = new Pair<string, float>(elem.Name, elem.Value);
        return statsPairArray;
    }

    public Pair<string, float> GetStat(string name)
    {
        foreach(var elem in statElements)
            if (elem.Name == name)
                return new Pair<string, float>(elem.Name, elem.Value);
        return null;
    }

    public void OnValueChanged() => ValueChangeCallback?.Invoke();
}
