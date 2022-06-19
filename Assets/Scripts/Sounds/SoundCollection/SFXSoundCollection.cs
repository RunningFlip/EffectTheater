using System.IO;
using Theater.Coloring;
using UnityEditor;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    public class SFXSoundCollection : SoundCollectionBase<SFXHandler, SimpleColorizer> {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public static string ResourcePath => Path.Combine(SFXSoundCollection.ResourcesPath, "SFXSoundCollection.asset");

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        [MenuItem("Sound Collections/Create SFX")]
        public static SFXSoundCollection CreateSFXCollection() {

            SFXSoundCollection collection = CreateAsset<SFXSoundCollection>(Path.Combine("Assets", ResourcePath));
            collection.soundType = SoundType.SFX;

            return collection;
        }

        //--------------------------------------------------------------------------------
    }
}
