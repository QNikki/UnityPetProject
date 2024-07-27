namespace DZM.Base
{
    public struct InputEvent
    {
        public bool IsSet => Count > 0;

        public void Set()
        {
            Count++;
        }

        public uint Count;
    }
}