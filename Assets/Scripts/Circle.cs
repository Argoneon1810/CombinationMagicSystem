using UnityEngine;
using UnityEngine.UI;

public class Circle : Graphic
{
    [Range(3, 32)]
    public int vertices = 16;
    protected RectTransform rt;

    protected override void Start()
    {
        base.Start();
        if(!TryGetComponent(out CanvasRenderer cr))
            gameObject.AddComponent<CanvasRenderer>();
        rt = transform as RectTransform;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        UIVertex vertex = UIVertex.simpleVert;
        Vector3 center = new Vector3((0.5f - rt.pivot.x) * rt.sizeDelta.x, (0.5f - rt.pivot.y) * rt.sizeDelta.y, 0);

        vertex.position = center;
        vh.AddVert(vertex);

        float theta = 360.0f / vertices;
        float radius = Mathf.Min(rt.sizeDelta.x, rt.sizeDelta.y) / 2;
        for (int i = 0; i < vertices; ++i)
        {
            vertex.position = center + (Quaternion.Euler(0, 0, theta * i) * Vector3.up) * radius;
            vh.AddVert(vertex);
        }

        for (int i = 1; i < vertices; ++i)
            vh.AddTriangle(0, i, i + 1);
        vh.AddTriangle(0, vertices, 1);
    }
}
