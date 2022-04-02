using System;
using UnityEngine;

namespace Events {

    public class EventListener : MonoBehaviour {

        [SerializeField]
        private ScriptableEvent _someEvent;

        public event Action OnEventHappened = delegate { };

        private void OnEnable() {
            _someEvent.AddListener(EventHappened);
        }

        private void OnDisable() {
            _someEvent.RemoveListener(EventHappened);
        }

        private void EventHappened() {
            OnEventHappened.Invoke();
        }
    }
}