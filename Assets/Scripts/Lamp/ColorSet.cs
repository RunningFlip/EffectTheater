using UnityEngine;
using System;


[Serializable]
public class ColorSet
{
    public Color mainColor;
    public Color lerpColor;


    public ColorSet(Color _mainColor, Color _lerpColor)
    {
        mainColor = _mainColor;
        lerpColor = _lerpColor;
    }
}