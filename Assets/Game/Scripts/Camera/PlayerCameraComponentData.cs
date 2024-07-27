using Unity.Entities;

namespace DZM.Camera
{
    public struct PlayerCameraComponentData : IComponentData
    {
        public float BaseFoV;

        public float CurrentFoV;

        public PlayerCameraComponentData(float fov)
        {
            BaseFoV = fov;
            CurrentFoV = fov;
        }
    }
}