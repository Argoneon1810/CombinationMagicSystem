using UnityEngine;

public class DebugGetTangent : MonoBehaviour
{
    [SerializeField] GameObject from, to;
    [SerializeField] float tanMultiplier;

    private void OnDrawGizmos()
    {
        Vector3 fromV = from.transform.position;
        Vector3 toV = to.transform.position;
        Vector3 fromToV = toV - fromV;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(fromV, toV);

        Vector3 tan = Vector3.Cross(fromToV, Vector3.forward);

        Gizmos.color = Color.magenta;
        Vector3 lineCenter = fromV + fromToV / 2;
        Gizmos.DrawLine(lineCenter, lineCenter + tan * tanMultiplier);
    }
}
