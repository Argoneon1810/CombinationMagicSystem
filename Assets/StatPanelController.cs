using TMPro;
using UnityEngine;

[ExecuteAlways]
public class StatPanelController : MonoBehaviour
{
    public RadialUIPolygon mainRadial, bgRadial;
    [Range(0,360)] public int rotation;
    public float fontSize, labelDistance;
    public Color mainColor, backgroundColor, fontColor;

    //this does not apply on built game. think of different approach.
    private void OnValidate()
    {
        mainRadial.material.color = mainColor;
        bgRadial.material.color = backgroundColor;
        mainRadial.rotation = bgRadial.rotation = rotation;
        (mainRadial as StatGUI).labelDistanceFromVertex = labelDistance;
        mainRadial.NotifyValueChanged();
        bgRadial.NotifyValueChanged();
        foreach(var elem in mainRadial.transform.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            elem.fontSize = fontSize;
            elem.color = fontColor;
        }
    }
}
