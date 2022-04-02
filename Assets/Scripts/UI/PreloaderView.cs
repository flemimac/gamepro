using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class PreloaderView : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _sceneLoadingValue;

        [SerializeField]
        private EventListener _updateListener;

        [SerializeField]
        private Image _progressImage;

        private void Awake() {
            _updateListener.OnEventHappened += UpdateBehaviour;
        }

        private void OnDestroy() {
            _updateListener.OnEventHappened -= UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            _progressImage.fillAmount = _sceneLoadingValue.value;
        }
    }
}