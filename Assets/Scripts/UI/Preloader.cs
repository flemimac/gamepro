using System.Collections;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {

    public class Preloader : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _sceneLoadingValue;

        private void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {
            var asyncOperation = SceneManager.LoadSceneAsync("Menu");
            while (!asyncOperation.isDone) {
                _sceneLoadingValue.value = asyncOperation.progress / .9f;
                yield return null;
            }
        }
    }
}