using UnityEngine;

namespace Services.ScreenSystem
{
    public interface IScreenService
    {
        void InitializeScreens(ScreenType[] screenTypes);
        void OpenScreen(ScreenType screen, object payload = null);
        void CloseScreen(ScreenType screen);
        void ClearScreens();
    }
}
