using System.Threading;
using UnityEngine;

[System.Serializable]
public class StatElement
{
    public delegate void ValueChangeCallback();

    [SerializeField] private string name = "Placeholder";
    [SerializeField] private float value = 1;
    public string Name => name;
    public float Value => value;

    public StatElement(string name, float value, ValueChangeCallback callback)
    {
        this.name = name;
        this.value = value;
        new Thread(delegate () {
            float value_old = Value;
            while (true)
            {
                Thread.Sleep(ToMillis(0.1f));
                if (value_old != Value)
                {
                    //Debug.Log(name + " is changed");
                    value_old = Value;
                    UnityMainThreadDispatcher.Instance().Enqueue(() => callback());
                }
            }
        }).Start();
    }

    int ToMillis(float second) => (int) (second * 1000);
}