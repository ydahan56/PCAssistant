using DisplayPlugin.Enums;
using Sdk.Contracts;
using static DisplayPlugin.Native.user32;

namespace DisplayPlugin.Core
{
    public class DisplayApi : IApi
    {
        private readonly DisplayState state;

        public DisplayApi(DisplayState state)
        {
            this.state = state;

            Action = SetDisplayInState;
        }

        private void SetDisplayInState()
        {
            PostMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, (int)state);
        }
    }
}
