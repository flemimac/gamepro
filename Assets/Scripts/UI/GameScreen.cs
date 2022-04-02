using System.Collections;
using Events;
using Game;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _saveEventListener;

        [SerializeField]
        private CarSettings[] _carSettings;

        [SerializeField]
        private CarDodgeView[] _carDodgeViews;

        private void Awake() {
            _saveEventListener.OnEventHappened += OnGameSaved;
        }

        private void OnEnable() {
            StartCoroutine(InitCarDodgeViews());
        }

        private IEnumerator InitCarDodgeViews() {
            for (int i = 0; i < _carSettings.Length; i++) {
                _carDodgeViews[i].Init(_carSettings[i]);
                yield return null;
            }
        }

        private void OnGameSaved() {
            UIManager.Instance.ShowLeaderboardScreen();
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}