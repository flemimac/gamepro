using Events;
using UnityEngine;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private EventDispatcher _carDodgedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgedScore;

        [SerializeField]
        private ScriptableStringValue _dodgedCarName;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("PlayerDodge")) {
                _dodgedScore.value = _carSettings.dodgeScore;
                _dodgedCarName.value = _carSettings.name;
                _carDodgedEventDispatcher.Dispatch();
            }
        }
    }
}