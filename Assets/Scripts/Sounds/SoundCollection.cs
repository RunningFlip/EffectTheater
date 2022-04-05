using UnityEditor;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    public class SoundCollection : ScriptableObject {

        //--------------------------------------------------------------------------------
        // Inner struct
        //--------------------------------------------------------------------------------

        public enum SoundType {

            SFX,
            Ambient,
            Music,
        }

        private const string MENU_PATH = "Sound Collections";
        private const string ASSET_PATH = "Assets/Resources/Sound Collections/";

        public const string SFX_ASSET_NAME = "Sound Collections/SFXCollection.asset";
        public const string AMBIENT_ASSET_NAME = "Sound Collections/AmbientCollection.asset";
        public const string MUSIC_ASSET_NAME = "Sound Collections/MusicCollection.asset";

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public SoundType soundType = SoundType.SFX;
        public float masterVolume = 0.5f;
        public SoundHandlerBase[] soundHandlers;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        [MenuItem(SoundCollection.MENU_PATH + "/Create SFX")]
        public static void CreateSFXCollection() {
            SoundCollection.CreateAsset(SoundCollection.ASSET_PATH + SoundCollection.SFX_ASSET_NAME);
        }

        //--------------------------------------------------------------------------------

        [MenuItem(SoundCollection.MENU_PATH + "/Create Ambient")]
        public static void CreateAmbientCollection() {
            SoundCollection.CreateAsset(SoundCollection.ASSET_PATH + SoundCollection.AMBIENT_ASSET_NAME);
        }

        //--------------------------------------------------------------------------------

        [MenuItem(SoundCollection.MENU_PATH + "/Create Music")]
        public static void CreateMusicCollection() {
            SoundCollection.CreateAsset(SoundCollection.ASSET_PATH + SoundCollection.MUSIC_ASSET_NAME);
        }

        //--------------------------------------------------------------------------------

        private static void CreateAsset(string name) {

            SoundCollection asset = ScriptableObject.CreateInstance<SoundCollection>();
            AssetDatabase.CreateAsset(asset, name);

            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }

        //--------------------------------------------------------------------------------
    }
}