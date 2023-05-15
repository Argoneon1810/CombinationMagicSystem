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

    private List<Thread> observerThreads = new List<Thread>();

    [ExecuteAlways]
    private void Start()
    {
        ProgressableStatElement statElement;
        foreach(var elem in statElements)
        {
            statElement = new ProgressableStatElement(elem);
            progressableStatElements.Add(statElement);
            observerThreads.Add(StartObserver(statElement));
        }
        OnValueChanged();
    }

    private void OnApplicationPause(bool pause)
    {
        foreach(Thread thread in observerThreads)
        {
            if (pause) thread.Suspend();
            else thread.Resume();
        }
    }

    private void OnDestroy()
    {
        foreach(Thread thread in observerThreads)
            thread.Abort();
        observerThreads.Clear();
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

    Thread StartObserver(ProgressableStatElement elem)
    {
        Thread thread = new Thread(delegate () {
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
        });
        thread.Start();
        return thread;
    }

    public void OnValueChanged() => ValueChangeCallback?.Invoke();

    int ToMillis(float second) => (int)(second * 1000);
}
