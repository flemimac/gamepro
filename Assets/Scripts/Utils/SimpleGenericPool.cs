using System.Collections.Generic;

namespace Utils {

    public class SimpleGenericPool<T> where T : UnityEngine.MonoBehaviour {

        private Stack<T> _stack;
        private T _prefab;

        public SimpleGenericPool(T prefab, int length = 1) {
            _prefab = prefab;

            _stack = new Stack<T>();
            for (int i = 0; i < length; i++) {
                var t = Instantiate();
                t.gameObject.SetActive(false);
                _stack.Push(t);
            }
        }

        public T Pop() {
            if (_stack.Count > 0) {
                var t = _stack.Pop();
                t.gameObject.SetActive(true);
                return t;
            }

            return Instantiate();
        }

        public void Push(T t) {
            t.gameObject.SetActive(false);
            _stack.Push(t);
        }

        private T Instantiate() {
            var t = UnityEngine.GameObject.Instantiate(_prefab);
            return t;
        }
    }
}