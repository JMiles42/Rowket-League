using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerDisplayArrow : JMilesBehaviour
{
    public Vector3 minScale;
    public Vector3 maxScale;
    public LerpableColour OutlineColour;
    public LerpableColour FillColour;
    public SpriteRenderer ArrowOutline;
    public SpriteRenderer ArrowFill;
    public float TEST;

    private void Start()
    {
        if (ArrowFill == null) ArrowFill = new SpriteRenderer();
        if (ArrowOutline == null) ArrowOutline = new SpriteRenderer();
        transform.localScale = minScale;
    }

    public void SetScale(float scale)
    {
        ArrowFill.color = FillColour.GetColor(scale);
        ArrowOutline.color = OutlineColour.GetColor(scale);
        transform.localScale = Vector3.Lerp(minScale, maxScale, scale);
        TEST = scale;
    }
}
[Serializable]
public struct LerpableColour
{
    public Color MinColour;
    public Color MaxColour;

    public LerpableColour(Color colMin, Color colMax)
    {
        MinColour = colMin;
        MaxColour = colMax;
    }

    public Color GetColor(float time)
    {
        return Color.Lerp(MinColour,MaxColour,time);
    }
}
