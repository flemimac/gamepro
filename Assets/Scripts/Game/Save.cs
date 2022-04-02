using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
using UnityEngine;

namespace Game {

    public class Save : MonoBehaviour {

        [Serializable]
        public class SaveData {

            public string date;
            public string score;
        }

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;
        }

        private enum SaveType {

            PlayerPrefs,
            File,
        }

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventDispatcher _saveDispatcher;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private SaveType _saveType;

        [SerializeField]
        private int _maxRecords = 10;

        [SerializeField]
        private EventListener _settingsChangeEventListener;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private ScriptableIntValue _mode;

        [SerializeField]
        private ScriptableIntValue _dayMode;

        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private static int _currentRideInd = -1;
        public static int CurrentRideInd => _currentRideInd;

        private const string RECORDS_KEY = "records";
        private const string VOLUME_KEY = "volume";
        private const string MODE_KEY = "mode";
        private const string DAYMODE_KEY = "day_mode";
        private string _filePath;

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "data.txt");
            if (_saveType == SaveType.PlayerPrefs) {
                LoadFromPlayerPrefs();
            } else {
                LoadFromFile();
            }
        }

        private void OnEnable() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
            _settingsChangeEventListener.OnEventHappened += SaveSettings;
        }

        private void OnDisable() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
            _settingsChangeEventListener.OnEventHappened -= SaveSettings;
        }

        private void OnCarCollision() {
            var newRecord = new SaveData {
                date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                score = _currentScore.value.ToString()
            };

            _currentRideInd = -1;
            if (_saveDatas.Count < _maxRecords) {
                _saveDatas.Add(newRecord);
                _currentRideInd = _saveDatas.Count - 1;
            } else {
                for (int i = 0; i < _saveDatas.Count; i++) {
                    if (Int32.Parse(newRecord.score) >= Int32.Parse(_saveDatas[i].score)) {
                        _saveDatas[i] = newRecord;
                        _currentRideInd = i;
                        break;
                    }
                }
            }

            if (_saveType == SaveType.PlayerPrefs) {
                SaveToPlayerPrefs();
            } else {
                SaveToFile();
            }

            _saveDispatcher.Dispatch();
        }

        private void LoadFromPlayerPrefs() {
            if (PlayerPrefs.HasKey(RECORDS_KEY)) {
                var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
                _saveDatas = wrapper.saveDatas;
            }

            if (PlayerPrefs.HasKey(VOLUME_KEY)) {
                _volume.value = PlayerPrefs.GetFloat(VOLUME_KEY);
            }

            if (PlayerPrefs.HasKey(MODE_KEY)) {
                _mode.value = PlayerPrefs.GetInt(MODE_KEY);
            }

            if (PlayerPrefs.HasKey(DAYMODE_KEY)) {
                _dayMode.value = PlayerPrefs.GetInt(DAYMODE_KEY);
            }
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

        private void SaveSettings() {
            PlayerPrefs.SetFloat(VOLUME_KEY, _volume.value);
            PlayerPrefs.SetInt(MODE_KEY, _mode.value);
            PlayerPrefs.SetInt(DAYMODE_KEY, _dayMode.value);
        }

        private void SaveToPlayerPrefs() {
            var wrapper = GetWrapper();
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(RECORDS_KEY, json);
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.Open)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
            Debug.Log(_saveDatas.Count);
        }

        private void SaveToFile() {
            var wrapper = GetWrapper();
            var binaryFormatter = new BinaryFormatter();
            using(FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }
    }
}