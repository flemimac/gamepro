using System.Collections;
using Events;
using Game;
using UnityEngine;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSourcePlayer _gameMusicPlayer;

        [SerializeField]
        private float _fadeTime = .3f;

        [SerializeField]
        private EventListener _volumeChangeEventListener;

        [SerializeField]
        private ScriptableFloatValue _volume;

        private AudioSourcePlayer _currentPlayer;

        private void Awake() {
            _volumeChangeEventListener.OnEventHappened += OnVolumeChange;
        }

        public void PlayMenuMusic() {
            if (_menuMusicPlayer.IsPlaying) {
                return;
            }
            StartCoroutine(FadeMusicTo(_menuMusicPlayer));
        }

        public void PlayGameMusic() {
            if (_gameMusicPlayer.IsPlaying) {
                return;
            }
            StartCoroutine(FadeMusicTo(_gameMusicPlayer));
        }

        private void OnVolumeChange() {
            _currentPlayer.SetVolume(_volume.value);
        }

        private IEnumerator FadeMusicTo(AudioSourcePlayer to) {
            if (_currentPlayer == null) {
                yield return StartCoroutine(FadeMusicIn(to));
                yield break;
            }

            yield return StartCoroutine(FadeMusicOut(_currentPlayer));
            StartCoroutine(FadeMusicIn(to));
        }

        private IEnumerator FadeMusicIn(AudioSourcePlayer to) {
            if (!to.IsPlaying) {
                to.Play();
            }
            if (_currentPlayer == to) {
                yield break;
            }
            _currentPlayer = to;

            var timer = 0f;
            var halfFadeTime = _fadeTime / 2f;
            while (timer < halfFadeTime) {
                timer += Time.deltaTime;
                var volume = Mathf.Lerp(0f, _volume.value, timer / halfFadeTime);
                to.SetVolume(volume);
                yield return null;
            }
        }

        private IEnumerator FadeMusicOut(AudioSourcePlayer to) {
            var timer = 0f;
            var halfFadeTime = _fadeTime / 2f;
            while (timer < halfFadeTime) {
                timer += Time.deltaTime;
                var volume = Mathf.Lerp(_volume.value, 0f, timer / halfFadeTime);
                to.SetVolume(volume);
                yield return null;
            }
            to.Stop();
        }

    }
}