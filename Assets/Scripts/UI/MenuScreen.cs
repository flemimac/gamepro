using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _settingsButton;

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadGameplay();
        }

        private void OnSettingsButtonClick() {
            UIManager.Instance.ShowSettingsScreen();
        }

    }
}