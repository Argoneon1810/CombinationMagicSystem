using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class MasterColorController : MonoBehaviour
{
    public Color mainColor, shadowColor;
    public List<Image> mainColorTargets = new();
    public List<Image> shadowColorTargets = new();
    public bool redraw;

    private void Update()
    {
        if(redraw.Trigger())
        {
            foreach(var target in mainColorTargets)
                target.color = mainColor;
            foreach(var target in shadowColorTargets)
                target.color = shadowColor;
        }
    }
}
