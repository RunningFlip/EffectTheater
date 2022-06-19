using System;
using System.Collections.Generic;
using System.IO;
using Theater.Coloring;
using Theater.Sounds;
using UnityEditor;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Misc {

    public static class AudioObjectConverter {

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        [MenuItem("Sound Collections/Convert")]
        public static void ConvertSoundCollections() {

            AudioObjectConverter.GetCollection<SFXSoundCollection, SFXHandler, SimpleColorizer>(SoundType.SFX);
            AudioObjectConverter.GetCollection<AmbientSoundCollection, LoopHandler, LerpColorizer>(SoundType.Ambient);
            AudioObjectConverter.GetCollection<MusicSoundCollection, LoopHandler, LerpColorizer>(SoundType.Music);
        }

        //--------------------------------------------------------------------------------

        private static T GetCollection<T, R, X>(SoundType type)
            where T : SoundCollectionBase<R, X>
            where R : SoundHandlerBase<X>
            where X : ColorizerBase {

            string folderName = "";
            T collection = null; ;

            switch (type) {

                case SoundType.SFX:
                    folderName = "SFX";
                    collection = SFXSoundCollection.CreateSFXCollection() as T;
                    break;

                case SoundType.Ambient:
                    folderName = "Ambient";
                    collection = AmbientSoundCollection.CreateAmbientCollection() as T;
                    break;

                case SoundType.Music:
                    folderName = "Music";
                    collection = MusicSoundCollection.CreateMusicCollection() as T;
                    break;
            }

            string path = $"Resources/Debugging AudioObject/{folderName}";
            DirectoryInfo info = new DirectoryInfo(Path.Combine(Application.dataPath, path));
            List<R> handlers = new List<R>();

            foreach (DirectoryInfo child in info.GetDirectories()) {

                List<SoundTuple> tuples = AudioObjectConverter.ConverAudioObjects(AudioObjectConverter.GetAudioObjects(child), 
                    out string[] associations,
                    out SearchTag[] searchTags, 
                    out Color[] colors);

                R handler = Activator.CreateInstance(typeof(R), child.Name, searchTags, associations, tuples.ToArray()) as R;

                if (colors.Length >= 1) {

                    if (handler is SFXHandler sfxHandler) {
                        sfxHandler.Colorizer.SetColor(colors[0]);
                    }
                    else if (handler is LoopHandler loopHandler) {

                        Color toColor = colors.Length >= 2 ? colors[1] : colors[0];
                        loopHandler.Colorizer.SetColor(colors[0], toColor);
                    }
                }

                handlers.Add(handler);
            }

            collection.masterVolume = 0.5f;
            collection.soundHandlers = handlers.ToArray();

            AssetDatabase.SaveAssets();

            return collection;
        }

        //--------------------------------------------------------------------------------

        private static List<AudioObject> GetAudioObjects(DirectoryInfo info) {

            List<AudioObject> audioObjects = new List<AudioObject>();

            foreach (FileInfo file in info.GetFiles()) {
                audioObjects.Add(Resources.Load<AudioObject>(Path.Combine(file.FullName, Path.GetFileNameWithoutExtension(file.Name))));
            }

            return audioObjects;
        }

        //--------------------------------------------------------------------------------

        private static List<SoundTuple> ConverAudioObjects(List<AudioObject> audioObjects, out string[] associations, out SearchTag[] seachTags, out Color[] colors) {

            List<SoundTuple> soundTuples = new List<SoundTuple>();

            associations = null;
            seachTags = null;
            colors = null;

            foreach (AudioObject audioObject in audioObjects) {
                soundTuples.Add(new SoundTuple(audioObject.name, audioObject.clip, audioObject.volume));
            }

            if (audioObjects.Count > 0) {

                AudioObject audioObject = audioObjects[0];

                associations = audioObject.associations;
                seachTags = AudioObjectConverter.ConvertSearchTags(audioObject.seachTags);
                colors = audioObject.colors;
            }

            return soundTuples;
        }

        //--------------------------------------------------------------------------------

        private static SearchTag[] ConvertSearchTags(AudioObject.SearchTag[] soundTypes) {

            SearchTag[] tags = new SearchTag[soundTypes.Length];

            for (int i = 0; i < soundTypes.Length; i++) {
                tags[i] = (SearchTag) soundTypes[i];
            }

            return tags;
        }

        //--------------------------------------------------------------------------------
    }
}