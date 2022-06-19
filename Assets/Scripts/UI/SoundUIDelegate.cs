using Theater.Sounds;
using UnityEditor;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    public class SoundUIDelegate : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] private SoundScrollView sfxScrollView;
        [SerializeField] private SoundScrollView ambientScrollView;
        [SerializeField] private SoundScrollView musicScrollView;
        [Space]
        [SerializeField] private SearchFilter searchFilter;

        private SoundCollectionContainer container;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private void Awake() {

            this.container = new SoundCollectionContainer();
            this.searchFilter.SoundCollectionContainer = this.container;

            if (this.container.SFXCollection != null) {
                this.sfxScrollView.InitScrollView(this.container.SFXCollection);
            }

            if (this.container.AmbientCollection != null) {
                this.ambientScrollView.InitScrollView(this.container.AmbientCollection);
            }

            if (this.container.MusicCollection != null) {
                this.musicScrollView.InitScrollView(this.container.MusicCollection);
            }
        }

        //--------------------------------------------------------------------------------

        private void OnDestroy() {

            EditorUtility.SetDirty(this.container.SFXCollection);
            EditorUtility.SetDirty(this.container.AmbientCollection);
            EditorUtility.SetDirty(this.container.MusicCollection);
        }

        //--------------------------------------------------------------------------------
    }
}