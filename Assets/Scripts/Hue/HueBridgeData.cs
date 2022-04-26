using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Hue {

    [CreateAssetMenu(fileName = "HueBridgeData", menuName = "Create HueBridgeData", order = 1)]
    public class HueBridgeData : ScriptableObject {

        [Header("User")]
        public string userName;
        public string deviceType;

        [Header("Hue Bridge")]
        public string id;
        public string ip;
        public int port;
    }
}