using System;

//--------------------------------------------------------------------------------

namespace Theater {

    [Flags]
    public enum SearchTag {

        None = 0,
        
        Nature = 1,
        Weapon = 2,
        Monster = 3,
        Human = 4,
    }
}