using System.Collections;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ScoreView : MonoBehaviour {

        [SerializeField]
        private float _scoreCountDelay;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        [SerializeField]
        private Text _scoreLabel;

        private int _currentScore;
        private bool isBusy;

        private void Awake() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            if (_currentScoreValue.value > _currentScore && !isBusy) {
                StartCoroutine(SetScoreCoroutine(_currentScoreValue.value));
            }
        }

        public IEnumerator SetScoreCoroutine(int score) {
            isBusy = true;
            while (_currentScore < score) {
                _currentScore++;
                _scoreLabel.text = $"{_currentScore}";
                yield return new WaitForSeconds(_scoreCountDelay);
            }
            isBusy = false;
        }
    }
}