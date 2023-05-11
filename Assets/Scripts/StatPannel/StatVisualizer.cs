using System.Collections.Generic;
using UnityEngine;

public class StatVisualizer : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private StatGUI rup;

    public bool normalize = true;

    private void Start()
    {
        stats.ValueChangeCallback += FeedRadialUI;
    }

    void FeedRadialUI()
    {
        var statPair = Split(normalize ? Normalize(stats.GetStats()) : stats.GetStats());
        string[] names = statPair.First;
        float[] values = statPair.Second;
        rup.verticeCount = values.Length;
        rup.verticeDistances = new List<float>(values);
        int i = 0;
        foreach(var label in rup.labels)
            label.text = names[i++];
        rup.NotifyValueChanged();
    }

    Pair<string, float>[] Normalize(Pair<string, float>[] statsPairArray)
    {
        float max = float.MinValue;
        foreach (var stat in statsPairArray)
        {
            if (max < stat.Second)
                max = stat.Second;
        }
        for (int i = 0; i < statsPairArray.Length; ++i)
            statsPairArray[i].Second /= max;
        return statsPairArray;
    }

    Pair<string[], float[]> Split(Pair<string, float>[] orig)
    {
        string[] firsts = new string[orig.Length];
        float[] seconds = new float[orig.Length];
        int i = 0;
        foreach(var pair in orig)
        {
            firsts[i] = pair.First;
            seconds[i] = pair.Second;
            i++;
        }
        return new Pair<string[], float[]>(firsts, seconds);
    }
}