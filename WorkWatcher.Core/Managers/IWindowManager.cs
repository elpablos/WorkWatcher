using System;

namespace Lorenzo.WorkWatcher.Core.Managers
{
    /// <summary>
    /// Delegat zmeny hlavniho okna
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="windowHeader"></param>
    /// <param name="processName"></param>
    /// <param name="hwnd"></param>
    public delegate void ActiveWindowChangedHandler(object sender, string windowHeader, string processName, IntPtr hwnd);

    /// <summary>
    /// Ziskavani informaci z aktivnich oken
    /// </summary>
    public interface IWindowManager : IDisposable
    {
        /// <summary>
        /// Udalost zmeny aktivniho okna
        /// </summary>
        event ActiveWindowChangedHandler ActiveWindowChanged;

        /// <summary>
        /// Rucni pokus o vycteni informaci
        /// vyvola udalost ActiveWindowChanged
        /// </summary>
        void TryActiveWindow();
    }
}
