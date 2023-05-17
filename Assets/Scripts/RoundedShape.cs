using UnityEngine;
using UnityEngine.UI;

public class RoundedShape : Graphic
{
    protected RectTransform rt;
    protected CanvasRenderer cr;

    [Range(3, 32)]
    public int vertices = 4;
    [Range(3, 32)]
    public int extentResolution = 3;
    [Range(0, 360)]
    public float rotation = 0;

    [SerializeField]
    private float innerExtentRadius = 1;
    [SerializeField]
    private float outerExtentRadius = 1.5f;

    [SerializeField]
    bool fill;

    public float InnerExtentRadius => innerExtentRadius <= 0 ? 0 : innerExtentRadius;
    public float OuterExtentRadius => outerExtentRadius <= 0 ? 0 : outerExtentRadius;

    [Range(0, 8)]
    public float radiusMultiplier = 1.414f, extentMultiplier = 1;

    protected override void Awake()
    {
        base.Awake();
        if (!TryGetComponent(out CanvasRenderer cr))
            this.cr = gameObject.AddComponent<CanvasRenderer>();
        else
            this.cr = cr;
        rt = transform as RectTransform;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        cr.SetColor(color);

        Rect rect = rt.rect;
        UIVertex vertex = UIVertex.simpleVert;
        Vector3 center = new Vector3((0.5f - rt.pivot.x) * rect.width, (0.5f - rt.pivot.y) * rect.height, 0);
        float radius = Mathf.Min(rect.width, rect.height) / 2;
        float theta = 360.0f / vertices;

        Vector3[] corners = new Vector3[vertices];
        for (int i = 0; i < vertices; ++i)
            corners[i] = center + (Quaternion.Euler(0, 0, theta * i - rotation) * Vector3.up) * radius * radiusMultiplier;

        int extentVertices = vertices * extentResolution;
        Vector3[] innerExtent = new Vector3[extentVertices];
        Vector3[] outerExtent = new Vector3[extentVertices];

        Vector3 ns_n, cs;
        ns_n = cs = Vector3.zero;
        Calc(corners, innerExtent, outerExtent, ref center, ref ns_n, ref cs, 0, vertices - 1);
        for (int i = 1; i < vertices; ++i)
            Calc(corners, innerExtent, outerExtent, ref center, ref ns_n, ref cs, i, i - 1);

        for (int i = 0; i < extentVertices; ++i)
        {
            vertex.position = innerExtent[i];
            vh.AddVert(vertex);
            vertex.position = outerExtent[i];
            vh.AddVert(vertex);
        }

        int extentVertices_double = extentVertices * 2;
        for (int i = 0; i < extentVertices_double-2; i += 2)
        {
            vh.AddTriangle(i, i + 1, i + 3);
            vh.AddTriangle(i, i + 3, i + 2);
        }
        vh.AddTriangle(extentVertices_double-2, extentVertices_double - 1, 1);
        vh.AddTriangle(extentVertices_double-2, 1, 0);

        if (!fill) return;

        vertex.position = center;
        vh.AddVert(vertex);
        for (int i = 0; i < extentVertices_double - 1; i += 2)
            vh.AddTriangle(extentVertices_double, i, i + 2);
        vh.AddTriangle(extentVertices_double, extentVertices_double - 2, 0);
    }

    void Calc(
        Vector3[] corners, 
        Vector3[] innerExtent, Vector3[] outerExtent, 
        ref Vector3 center, 
        ref Vector3 ns_n, ref Vector3 cs_n, 
        int first, int second
    ) {
        ns_n = Vector3.Cross(corners[first] - corners[second], Vector3.forward).normalized;
        cs_n = (corners[first] - center).normalized;
        float halfAngle = Mathf.Acos(Vector3.Dot(ns_n, cs_n)) * Mathf.Rad2Deg;
        float fullAngle = halfAngle * (2.0f / (extentResolution - 1));
        for (int j = 0; j < extentResolution; ++j)
        {
            innerExtent[first * extentResolution + j]
                = corners[first] + (Quaternion.Euler(0, 0, fullAngle * j) * ns_n) * InnerExtentRadius * extentMultiplier;
            outerExtent[first * extentResolution + j]
                = corners[first] + (Quaternion.Euler(0, 0, fullAngle * j) * ns_n) * OuterExtentRadius * extentMultiplier;
        }
    }
}
