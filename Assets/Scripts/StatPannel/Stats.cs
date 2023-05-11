using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class ProgressableStatElement
{
    [SerializeField]
    private float _progress = 1;
    public float Progress => _progress;
    public string Name => _statElement.statName;

    [SerializeField]
    private StatElement _statElement;
    public ProgressableStatElement(StatElement statElement)
    {
        _statElement = statElement;
    }
}

public class Stats : MonoBehaviour
{
    public event Action ValueChangeCallback;

    [SerializeField]
    StatElement[] statElements;
    [SerializeField]
    private List<ProgressableStatElement> progressableStatElements = new List<ProgressableStatElement>();

    [ExecuteAlways]
    private void Start()
    {
        ProgressableStatElement statElement;
        foreach(var elem in statElements)
        {
            statElement = new ProgressableStatElement(elem);
            progressableStatElements.Add(statElement);
            StartObserver(statElement);
        }
        OnValueChanged();
    }

    public Pair<string, float>[] GetStats()
    {
        Pair<string, float>[] statsPairArray = new Pair<string, float>[progressableStatElements.Count];
        int i = 0;
        foreach(var elem in progressableStatElements)
            statsPairArray[i++] = new Pair<string, float>(elem.Name, elem.Progress);
        return statsPairArray;
    }

    public Pair<string, float> GetStat(string name)
    {
        foreach(var elem in progressableStatElements)
            if (elem.Name == name)
                return new Pair<string, float>(elem.Name, elem.Progress);
        return null;
    }

    void StartObserver(ProgressableStatElement elem)
    {
        new Thread(delegate () {
            float value_old = elem.Progress;
            while (true)
            {
                Thread.Sleep(ToMillis(0.1f));
                if (value_old != elem.Progress)
                {
                    Debug.Log(elem.Name + " is changed");
                    value_old = elem.Progress;
                    UnityMainThreadDispatcher.Instance().Enqueue(() => OnValueChanged());
                }
            }
        }).Start();
    }

    public void OnValueChanged() => ValueChangeCallback?.Invoke();

    int ToMillis(float second) => (int)(second * 1000);
}
