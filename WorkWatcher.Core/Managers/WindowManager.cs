using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Lorenzo.WorkWatcher.Core.Managers
{
    /// <summary>
    /// Implementace ziskavani informaci z aktivnich oken
    /// </summary>
    public class WindowManager : IWindowManager
    {
        #region Contants

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        #endregion

        #region Fields

        private IntPtr _hhook;
        private WinEventDelegate _winEventProc;

        #endregion

        #region Delegates

        private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType,
            IntPtr hwnd, int idObject, int idChild, uint dwEventThread,
            uint dwmsEventTime);

        #endregion

        #region Events

        public event ActiveWindowChangedHandler ActiveWindowChanged;

        #endregion

        #region Static win32

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax,
            IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc,
            uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        #endregion

        #region Constructors

        public WindowManager()
        {
            // zachyceni sledovani zmen oken
            _winEventProc = new WinEventDelegate(WinEventProc);
            _hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND,
                EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _winEventProc,
                0, 0, WINEVENT_OUTOFCONTEXT);
        }

        #endregion

        #region W32 methods

        /// <summary>
        /// Reakce na zmenu okna
        /// </summary>
        /// <param name="hWinEventHook"></param>
        /// <param name="eventType"></param>
        /// <param name="hwnd"></param>
        /// <param name="idObject"></param>
        /// <param name="idChild"></param>
        /// <param name="dwEventThread"></param>
        /// <param name="dwmsEventTime"></param>
        private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd,
            int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            // pokud jde okno do popredi, tak zavolam udalost
            if (eventType == EVENT_SYSTEM_FOREGROUND)
            {
                RaiseActiveWindowEvent(hwnd);
            }
        }

        /// <summary>
        /// Udalost ziskani informaci o okne
        /// </summary>
        /// <param name="hwnd"></param>
        private void RaiseActiveWindowEvent(IntPtr hwnd)
        {
            try
            {
                uint processId;
                GetWindowThreadProcessId(hwnd, out processId);

                Process p = Process.GetProcessById((int)processId);
                string description = string.Empty;
                var processName = GetProcessName(p);

                // TODO
                if (processName.ToLower().StartsWith("chrome"))
                {
                    // TryGetUrl(p);
                    // description = GetChromeUrl(hwnd);
                    // WithSendkeys();
                }

                if (string.IsNullOrEmpty(description))
                {
                    description = GetActiveWindowTitle(hwnd);
                }

                // vystrelim udalost
                ActiveWindowChanged?.Invoke(this, description, processName, hwnd);
            }
            catch
            {
                // nezajem
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Ziskani informaci o aktivnim okne
        /// </summary>
        public void TryActiveWindow()
        {
            IntPtr hwnd = GetForegroundWindow();
            RaiseActiveWindowEvent(hwnd);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Ziskani nazvu procesu
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string GetProcessName(Process p)
        {
            return p.MainModule.ModuleName;
        }

        /// <summary>
        /// Ziskani nazvu okna
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        private string GetActiveWindowTitle(IntPtr hwnd)
        {
            StringBuilder Buff = new StringBuilder(500);
            GetWindowText(hwnd, Buff, Buff.Capacity);
            return Buff.ToString();
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Uklid
        /// </summary>
        public void Dispose()
        {
            UnhookWinEvent(_hhook);
        }

        ~WindowManager()
        {
            UnhookWinEvent(_hhook);
        }

        #endregion
    }
}
