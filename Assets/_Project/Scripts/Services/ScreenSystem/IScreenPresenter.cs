namespace Services.ScreenSystem
{
    public interface IScreenPresenter
    {
        void DestroyView();
        void CloseScreen();
        void OpenScreen(object payload = null);
    }
}