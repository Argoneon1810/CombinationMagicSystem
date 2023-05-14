using System.Collections;
using UnityEngine;

public class TimedActive : MonoBehaviour
{
    public float duration;
    private void OnEnable()
    {
        StartCoroutine(DelayDisableSelf());
    }

    IEnumerator DelayDisableSelf()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
