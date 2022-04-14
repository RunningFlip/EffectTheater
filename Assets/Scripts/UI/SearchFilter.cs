using UnityEngine;
using TMPro;
using Theater.Sounds;
using Theater.Coloring;
using UnityEngine.UI;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    [RequireComponent(typeof(TMP_InputField))]
    public class SearchFilter : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public SoundCollectionContainer SoundCollectionContainer { get; set; }

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] private Button clearButton;

        private TMP_InputField inputField;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private void Awake() {

            this.inputField = this.GetComponent<TMP_InputField>();
            this.inputField.onValueChanged.AddListener((input) => this.ProcessFilter(input));

            this.clearButton.onClick.AddListener(() => this.inputField.text = string.Empty);
        }

        //--------------------------------------------------------------------------------

        private void ProcessFilter(string input) {

            this.FilterCollection(this.SoundCollectionContainer.SFXCollection, input);
            this.FilterCollection(this.SoundCollectionContainer.AmbientCollection, input);
            this.FilterCollection(this.SoundCollectionContainer.MusicCollection, input);
        }

        //--------------------------------------------------------------------------------

        private void FilterCollection<T, R>(SoundCollectionBase<T, R> soundCollection, string input)
            where T : SoundHandlerBase<R>
            where R : ColorizerBase {

            bool noFilter = string.IsNullOrEmpty(input);
            input = input.ToLower();

            for (int i = 0; i < soundCollection.soundHandlers.Length; i++) {

                T handler = soundCollection.soundHandlers[i];
                bool titleMatch = handler.Title.ToLower().Contains(input);

                handler.UIElement.SetActive(noFilter || titleMatch);
            }
        }

        //--------------------------------------------------------------------------------
    }
}