using UnityEngine;

namespace Game {

    public class DayMode : MonoBehaviour {

        [SerializeField]
        private GameObject[] _lights;

        [SerializeField]
        private ScriptableIntValue _dayMode;

        [SerializeField]
        private bool _isReverted;

        private void OnEnable() {
            var active = false;
            if (_isReverted) {
                active = _dayMode.value == 1;
            } else {
                active = _dayMode.value == 0;
            }
            for (int i = 0; i < _lights.Length; i++) {
                _lights[i].SetActive(active);
            }
        }
    }
}