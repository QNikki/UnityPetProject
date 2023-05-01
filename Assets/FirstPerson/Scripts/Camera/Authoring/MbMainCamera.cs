using UnityEngine;

namespace FP.Core.Character
{
    public class MbMainCamera : MonoBehaviour
    {
        public static Camera Camera { get; private set; }

        private void OnValidate()
        {
            Camera ??= GetComponent<Camera>();
        }
    }
}