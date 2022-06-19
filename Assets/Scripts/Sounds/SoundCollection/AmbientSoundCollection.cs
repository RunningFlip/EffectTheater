using System.IO;
using Theater.Coloring;
using UnityEditor;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    public class AmbientSoundCollection : SoundCollectionBase<LoopHandler, LerpColorizer> {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public static string ResourcePath => Path.Combine(AmbientSoundCollection.ResourcesPath, "AmbientSoundCollection.asset");

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        [MenuItem("Sound Collections/Create Ambient")]
        public static AmbientSoundCollection CreateAmbientCollection() {
            
            AmbientSoundCollection collection = CreateAsset<AmbientSoundCollection>(Path.Combine("Assets", ResourcePath));
            collection.soundType = SoundType.Ambient;

            return collection;
        }

        //--------------------------------------------------------------------------------
    }
}
