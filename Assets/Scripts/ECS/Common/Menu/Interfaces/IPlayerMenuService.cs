namespace ECS.Common.Menu.Interfaces
{
    public interface IPlayerMenuService
    {
        bool IsOpen { get; }

        void Open();
        void Close();
    }
}
