using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "BoolValue", menuName = "Value/BoolValue")]
    public class ScriptableBoolValue : ScriptableObject
    {
        public bool value;
    }
}