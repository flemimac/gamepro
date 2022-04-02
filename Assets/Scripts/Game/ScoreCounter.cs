using Events;
using UnityEngine;

namespace Game {

    public class ScoreCounter : MonoBehaviour {

        [SerializeField]
        private EventListener _carDodgedEventListener;

        [SerializeField]
        private ScriptableIntValue _dodgedScore;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private void OnEnable() {
            _carDodgedEventListener.OnEventHappened += OnCarDodged;
        }

        private void OnDisable() {
            _currentScore.value = 0;
            _carDodgedEventListener.OnEventHappened -= OnCarDodged;
        }

        private void OnCarDodged() {
            _currentScore.value += _dodgedScore.value;
            _dodgedScore.value = 0;
        }
    }
}