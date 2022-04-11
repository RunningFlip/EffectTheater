using System.IO;
using Theater.Coloring;
using UnityEditor;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    public class AmbientSoundCollection : SoundCollectionBase<SoundLoopHandler, LerpColorizer> {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public static string ResourcePath => Path.Combine(AmbientSoundCollection.ResourcesPath, "AmbientSoundCollection.asset");

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        [MenuItem("Sound Collections/Create Ambient")]
        public static void CreateAmbientCollection() {
            AmbientSoundCollection collection = CreateAsset<AmbientSoundCollection>(Path.Combine("Assets", ResourcePath));
            collection.soundType = SoundType.Ambient;
        }

        //--------------------------------------------------------------------------------
    }
}
