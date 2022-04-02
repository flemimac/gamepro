using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsScreen : MonoBehaviour {

        [SerializeField]
        private ToggleButton _modeToggle;

        [SerializeField]
        private ToggleButton _dayModeToggle;

        [SerializeField]
        private EventDispatcher _settingsChangeDispatcher;

        [SerializeField]
        private EventDispatcher _volumeChangeDispatcher;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private ScriptableIntValue _mode;

        [SerializeField]
        private ScriptableIntValue _dayMode;

        [SerializeField]
        private Button _confirmButton;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private Slider _volumeSlider;

        private float _initialVolume;
        private int _initialMode;
        private int _initialDayMode;

        private void Awake() {
            _modeToggle.OnStateChanged += OnModeToggleChanged;
            _dayModeToggle.OnStateChanged += OnDayModeToggleChanged;
            _confirmButton.onClick.AddListener(() => {
                _settingsChangeDispatcher.Dispatch();
                UIManager.Instance.ShowMenuScreen();
            });
            _cancelButton.onClick.AddListener(() => {
                RestoreSettings();
                UIManager.Instance.ShowMenuScreen();
            });
        }

        private void OnEnable() {
            _volumeSlider.onValueChanged.RemoveListener(OnVolumeSliderValueChnaged);
            ShowSettings();
            _volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChnaged);
        }

        private void ShowSettings() {
            _initialVolume = _volume.value;
            _initialMode = _mode.value;
            _initialDayMode = _dayMode.value;

            _modeToggle.SetState(_initialMode == 1 ? ToggleButton.State.Right : ToggleButton.State.Left);
            _dayModeToggle.SetState(_initialDayMode == 1 ? ToggleButton.State.Right : ToggleButton.State.Left);
            _volumeSlider.value = _initialVolume;
        }

        private void RestoreSettings() {
            _volume.value = _initialVolume;
            _mode.value = _initialMode;
            _dayMode.value = _initialDayMode;

            _volumeChangeDispatcher.Dispatch();
        }

        private void OnModeToggleChanged(ToggleButton.State state) {
            _mode.value = state == ToggleButton.State.Right?1 : 0;
        }

        private void OnDayModeToggleChanged(ToggleButton.State state) {
            _dayMode.value = state == ToggleButton.State.Right?1 : 0;
        }

        private void OnVolumeSliderValueChnaged(float value) {
            _volume.value = value;
            _volumeChangeDispatcher.Dispatch();
        }
    }
}