using Events;
using UnityEngine;

namespace Game {

    public class RoadPart : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _roadTriggerEventDispatcher;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _roadTriggerEventDispatcher.Dispatch();
            }
        }
    }
}