using UnityEngine;

namespace DZM.Camera
{
    using Camera = UnityEngine.Camera;
    
    public class PlayerCameraMb : MonoBehaviour
    {
        public static Camera Instance;

        private void Awake()
        {
            Instance = GetComponent<Camera>();
        }
    }
}