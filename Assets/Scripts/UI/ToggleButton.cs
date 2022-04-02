using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ToggleButton : MonoBehaviour {

        public enum State {

            Right,
            Left,
        }

        [SerializeField]
        private Button _button;

        [SerializeField]
        private Image _rightImage;

        [SerializeField]
        private Image _leftImage;

        private State _state;

        public event Action<State> OnStateChanged = delegate { };

        private void Awake() {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick() {
            SwapState();
        }

        private void SwapState() {
            _state = _state == State.Right?State.Left : State.Right;
            SetImagesActive();
            OnStateChanged(_state);
        }

        private void SetImagesActive() {
            _rightImage.enabled = _state == State.Right;
            _leftImage.enabled = _state == State.Left;
        }

        public void SetState(State state) {
            _state = state;
            SetImagesActive();
        }
    }
}