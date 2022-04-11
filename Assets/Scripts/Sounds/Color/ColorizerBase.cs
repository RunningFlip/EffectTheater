using System;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Coloring {

    [Serializable]
    public abstract class ColorizerBase {

        public abstract Color GetColor();
    }
}
