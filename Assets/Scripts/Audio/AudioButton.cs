using UnityEngine;
using UnityEngine.UI;

namespace Audio {

    [RequireComponent(typeof(Button))]
    public class AudioButton : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _audioSourcePlayer;

        private Button _button;

        private void Awake() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick() {
            _audioSourcePlayer.Play();
        }
    }
}