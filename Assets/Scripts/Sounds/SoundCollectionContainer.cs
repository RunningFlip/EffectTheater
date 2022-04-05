using UnityEngine;
using System.IO;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    public class SoundCollectionContainer {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public SoundCollection SFXCollection { get; }
        public SoundCollection AmbientCollection { get; }
        public SoundCollection MusicCollection { get; }

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public SoundCollectionContainer() {

            this.SFXCollection = this.LoadSoundCollection(SoundCollection.SFX_ASSET_NAME);
            this.AmbientCollection = this.LoadSoundCollection(SoundCollection.AMBIENT_ASSET_NAME);
            this.MusicCollection = this.LoadSoundCollection(SoundCollection.MUSIC_ASSET_NAME);
        }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private SoundCollection LoadSoundCollection(string path) {

            string relativePath = Path.Combine("Assets/Resources", path);

            if (File.Exists(relativePath)) {

                string pathWithoutExtension =  Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
                return Resources.Load<SoundCollection>(pathWithoutExtension);
            }
            else {
                Debug.LogWarning($"Directory '{relativePath}' does not exist! SoundCollection was not loaded!");
            }

            return null;
        }

        //--------------------------------------------------------------------------------
    }
}