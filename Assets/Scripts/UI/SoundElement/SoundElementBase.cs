using UnityEngine;
using Theater.Sounds;
using UnityEngine.UI;
using TMPro;
using Theater.Coloring;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    [RequireComponent(typeof(Animator))]
    public abstract class SoundElementBase<T, R> : MonoBehaviour 
        where T : SoundHandlerBase<R> 
        where R : ColorizerBase {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [Header("Base")]
        [SerializeField] protected TextMeshProUGUI title;
        [SerializeField] protected Image outline;
        [SerializeField] protected Button playButton;
        [SerializeField] protected Button expandButton;
        
        [Header("Expand Button Image")]
        [SerializeField] private Sprite expandSprite;
        [SerializeField] private Sprite collapseSprite;

        private bool expanded;

        private Animator animator;
        private Image expandButtonImage;
        protected T soundHandler;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public void Initialize(T soundHandler) {

            this.soundHandler = soundHandler;
            this.title.text = this.soundHandler.Title;
            this.animator = this.GetComponent<Animator>();
            this.expandButtonImage = this.expandButton.image;

            this.OnInitialize(soundHandler);

            playButton.onClick.AddListener(this.soundHandler.Play);
            expandButton.onClick.AddListener(this.ToggleExpandButton);
        }

        //--------------------------------------------------------------------------------

        private void ToggleExpandButton() {

            this.expanded = !this.expanded;
            this.expandButtonImage.sprite = !this.expanded
                ? this.expandSprite
                : this.collapseSprite; 

            this.animator.SetTrigger("Toggle");
        }

        //--------------------------------------------------------------------------------

        protected abstract void OnInitialize(T soundHandler);

        //--------------------------------------------------------------------------------
    }
}