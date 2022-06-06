namespace Frame
{
    public interface ITickUpdate
    {
        void OnUpdate();
        void OnLateUpdate();
        void OnFixedUpdate();
    }
}