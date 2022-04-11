using System.IO;
using Theater.Coloring;
using UnityEditor;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    public abstract class SoundCollectionBase<T, R> : ScriptableObject 
        where T : SoundHandlerBase<R> 
        where R : ColorizerBase  {

        //--------------------------------------------------------------------------------
        // Constants
        //--------------------------------------------------------------------------------

        private const string SOUND_COLLECTION_DIRECTORY_NAME = "Sound Collections";
        protected const string ResourcesPath = "Resources" + "/" + SOUND_COLLECTION_DIRECTORY_NAME;

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [Header("General")]
        public SoundType soundType;
        [Range(0f, 1f)] public float masterVolume;
        [Space]
        public T[] soundHandlers;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        protected static X CreateAsset<X>(string path) where X : SoundCollectionBase<T, R> {

            X asset = ScriptableObject.CreateInstance<X>();

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            asset.masterVolume = 0.5f;
            return asset;
        }

        //--------------------------------------------------------------------------------

        public static X LoadSoundCollection<X>(string assetPath) where X : SoundCollectionBase<T, R> {

            string absPath = Path.Combine(Application.dataPath, assetPath);

            if (File.Exists(absPath)) {
                return Resources.Load<X>(Path.Combine(SOUND_COLLECTION_DIRECTORY_NAME, Path.GetFileNameWithoutExtension(assetPath)));
            }
            else {
                Debug.LogWarning($"Directory '{absPath}' does not exist! SoundCollection was not loaded!");
            }

            return null;
        }

        //--------------------------------------------------------------------------------
    }
}