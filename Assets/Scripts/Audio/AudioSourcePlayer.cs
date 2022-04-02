using Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio {

    public class AudioSourcePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ScriptableFloatValue _volume;

        public bool IsPlaying => _audioSource.isPlaying;

        [Button]
        public void Play() {
            SetVolume(_volume.value);
            _audioSource.Play();
        }

        [Button]
        public void Stop() {
            _audioSource.Stop();
        }

        public void SetVolume(float value) {
            _audioSource.volume = value;
        }
    }
}