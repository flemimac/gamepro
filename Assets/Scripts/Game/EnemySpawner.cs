using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private List<Car> _carPrefabs;

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private ScriptableBoolValue _hardGameMode;

        private float _currentTimer;
        private List<Car> _cars = new List<Car>();

        private Dictionary<string, SimpleGenericPool<Car>> _carPools;

        private void Awake() {
            if (_hardGameMode.value)
            {
                _spawnCooldown /= 2.5f;
            }

            _carPools = new Dictionary<string, SimpleGenericPool<Car>>();
            for (int i = 0; i < _carPrefabs.Count; i++) {
                _carPools[_carPrefabs[i].Name] = new SimpleGenericPool<Car>(_carPrefabs[i]);
            }
        }

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }

        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnsubscribeToEvents();
        }

        private void UpdateBehaviour() {
            HandleCarsBehindPlayer();

            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown) {
                return;
            }
            _currentTimer = 0f;

            SpawnRandomCar();
        }

        private void SpawnRandomCar() {
            var randomRoad = Random.Range(-1, 2);
            var randomCarInd = Random.Range(0, 3);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = _carPools[_carPrefabs[randomCarInd].name].Pop();
            car.transform.position = position;
            car.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    _carPools[_cars[i].Name].Push(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }
    }
}