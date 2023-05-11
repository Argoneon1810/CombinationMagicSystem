using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatGUI : RadialUIPolygon
{
    [SerializeField] private GameObject LabelPrefab;
    public float labelDistanceFromVertex = 1.5f;
    public List<TextMeshProUGUI> labels = new List<TextMeshProUGUI>();

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);

        float radius = Mathf.Min(rt.sizeDelta.x, rt.sizeDelta.y) / 2;
        float theta = 360.0f / verticeCount;

        for (int i = 0; i < verticeCount; ++i)
            labels[i].rectTransform.anchoredPosition = (Quaternion.Euler(0, 0, theta * i + rotation) * Vector3.up) * (radius + labelDistanceFromVertex);
    }

    protected override void MatchVertexCountToListLength()
    {
        base.MatchVertexCountToListLength();

        labels.Clear();
        var existings = transform.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (var existing in existings)
        {
            existing.enabled = true;
            labels.Add(existing);
        }

        int diff = verticeCount - labels.Count;
        if (diff > 0)
        {
            for (int i = 0; i < diff; ++i)
            {
                GameObject t = Instantiate(LabelPrefab);
                t.transform.SetParent(rt, false);
                labels.Add(t.GetComponent<TextMeshProUGUI>());
            }
        }
        else if (diff < 0)
        {
            for (int i = 0; i < -diff; ++i)
            {
                labels[labels.Count - 1].enabled = false;
                labels.RemoveAt(labels.Count - 1);
            }
        }
    }
}