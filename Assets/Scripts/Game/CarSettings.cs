using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

    
        public float maxSpeed;
    
        public float acceleration;

        public int dodgeScore;
        public int dodgeScore2;

        [BoxGroup("Render")]
        public GameObject renderCarPrefab;

        [BoxGroup("Render")]
        public Vector3 renderCameraPos;

        [BoxGroup("Render")]
        public Vector3 renderCameraRot;
    }
}