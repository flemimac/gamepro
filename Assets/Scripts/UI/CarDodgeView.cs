using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private Text _dodgeCountLabel;

        [SerializeField]
        private EventListener _carDodgedEventListener;

        [SerializeField]
        private ScriptableStringValue _dodgedCarName;

        private CarSettings _settings;
        private int _counter;

        private void OnEnable() {
            _carDodgedEventListener.OnEventHappened += OnCarDodged;
            _counter = 0;
            _dodgeCountLabel.text = $"{_counter}";
        }

        private void OnDisable() {
            _carDodgedEventListener.OnEventHappened -= OnCarDodged;
        }

        private void OnCarDodged() {
            if (!_dodgedCarName.value.Equals(_settings.name)) {
                return;
            }
            _dodgeCountLabel.text = $"{++_counter}";
        }

        public void Init(CarSettings settings) {
            _settings = settings;
            _carImage.texture = RenderManager.Instance.Render(settings.renderCarPrefab, settings.renderCameraPos, settings.renderCameraRot);
        }
    }
}