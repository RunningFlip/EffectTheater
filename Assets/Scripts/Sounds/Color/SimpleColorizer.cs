﻿using System;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Coloring {

    [Serializable]
    public class SimpleColorizer : ColorizerBase {

        //--------------------------------------------------------------------------------
        // Propertíes
        //--------------------------------------------------------------------------------

        public Color Color => this.color;

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] private Color color;     

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public override Color GetColor() {
            return color;
        }

        //--------------------------------------------------------------------------------
    }
}