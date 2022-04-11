using System;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Coloring {

    [Serializable]
    public class LerpColorizer : ColorizerBase {

        //--------------------------------------------------------------------------------
        // Propertíes
        //--------------------------------------------------------------------------------

        public Color FromColor => this.fromColor;
        public Color ToColor => this.toColor;
        public float LerpSpeed => this.lerpSpeed;

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] private Color fromColor;
        [SerializeField] private Color toColor;
        [SerializeField, Range(0, 100)] private float lerpSpeed;

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public override Color GetColor() {
            return Color.Lerp(this.fromColor, toColor, Time.time * this.lerpSpeed);
        }

        //--------------------------------------------------------------------------------
    }
}