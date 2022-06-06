
namespace Frame
{
    public interface IMgr
    {
        void Init();
        void StartUp();
        void DisposeBefore();
        void Dispose();
    }
}