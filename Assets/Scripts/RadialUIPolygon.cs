using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Draws a UI polygon.
/// There is a vertex at the centre of the shape to solve a flipping/overlapping issue when the shape goes concave.
/// </summary>
public class RadialUIPolygon : Graphic
{
    [Range(3, 32)] public int verticeCount = 3;
    [Range(0, 360)] public int rotation = 0;
    public List<float> verticeDistances = new List<float> { 1, 1, 1 };

    int rotation_old = 0;
    protected RectTransform rt;

    protected override void Start()
    {
        base.Start();
        rt = transform as RectTransform;
        NotifyValueChanged();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        canvasRenderer.Clear();
        vh.Clear();

        UIVertex vertex = UIVertex.simpleVert;

        float radius = Mathf.Min(rt.sizeDelta.x, rt.sizeDelta.y) / 2;
        float theta = 360.0f / verticeCount;

        vertex.position = Vector3.zero;
        vh.AddVert(vertex);
        for (int i = 0; i < verticeCount; ++i)
        {
            vertex.position = (Quaternion.Euler(0, 0, theta * i + rotation) * Vector3.up) * radius * verticeDistances[i];
            vh.AddVert(vertex);
        }

        for (int i = 1; i < verticeCount; ++i)
            vh.AddTriangle(0, i, i + 1);
        vh.AddTriangle(0, verticeCount, 1);
    }

    public void NotifyValueChanged()
    {
        MatchVertexCountToListLength();
        SetAllDirty();
    }

    protected virtual void MatchVertexCountToListLength()
    {
        int diff = verticeCount - verticeDistances.Count;
        if (diff > 0)
        {
            //Debug.Log("increased");
            for (int i = 0; i < diff; ++i)
                verticeDistances.Add(1);
        }
        else if (diff < 0)
        {
            //Debug.Log("reduced");
            for (int i = 0; i < -diff; ++i)
                verticeDistances.RemoveAt(verticeDistances.Count - 1);
        }
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if (verticeCount != verticeDistances.Count)
        {
            MatchVertexCountToListLength();
            SetVerticesDirty();
        }
        if (rotation_old != rotation)
        {
            rotation_old = rotation;
            SetVerticesDirty();
        }
    }
}
