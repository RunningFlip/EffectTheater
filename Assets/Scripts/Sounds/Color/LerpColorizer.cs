using System;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Coloring {

    [Serializable]
    public class LerpColorizer : ColorizerBase {

        //--------------------------------------------------------------------------------
        // Propertíes
        //--------------------------------------------------------------------------------

        public float LerpSpeed => this.lerpSpeed;

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public Color fromColor;
        public Color toColor;
        [SerializeField, Range(0, 100)] private float lerpSpeed;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override Color GetColor() {
            return Color.Lerp(this.fromColor, this.toColor, Time.time * this.lerpSpeed);
        }

        //--------------------------------------------------------------------------------

        public void SetColor(Color fromColor, Color toColor) {

            this.fromColor = fromColor;
            this.toColor = toColor;
        }

        //--------------------------------------------------------------------------------
    }
}