using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class KeyBinder : MonoBehaviour
{
    private string _key;
    public string key;
    public TextMeshProUGUI textMeshProUGUI;
    public UnityEvent OnKeyDown;
    public bool updateKeyChange;

    private void Awake()
    {
        _key = key.ToLower();
    }

    private void Update()
    {
        if (updateKeyChange.Trigger())
        {
            _key = key.ToLower();
            textMeshProUGUI.text = key;
        }
        if(Input.GetKeyDown(_key))
            OnKeyDown?.Invoke();
    }
}
