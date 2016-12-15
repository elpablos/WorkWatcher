using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Automation;

namespace Lorenzo.WorkWatcher.Common
{
    /// <summary>
    /// Zachytávání aktivního okna
    /// Inspirace:
    /// http://stackoverflow.com/questions/9271855/windows-callback-when-the-active-window-changed
    /// http://www.pinvoke.net/default.aspx/
    /// </summary>
    [Obsolete]
    public class ActiveWindow : IDisposable
    {
        public delegate void ActiveWindowChangedHandler(object sender, string windowHeader, string processName, IntPtr hwnd);
        public event ActiveWindowChangedHandler ActiveWindowChanged;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType,
            IntPtr hwnd, int idObject, int idChild, uint dwEventThread,
            uint dwmsEventTime);

        const uint WINEVENT_OUTOFCONTEXT = 0;
        const uint EVENT_SYSTEM_FOREGROUND = 3;

        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax,
            IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc,
            uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        //For Chrome
        private const int WM_GETTEXTLENGTH = 0Xe;
        private const int WM_GETTEXT = 0Xd;

        [DllImport("user32.dll")]
        private extern static int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private extern static int SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private extern static IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        // Delegate we use to call methods when enumerating child windows.
        private delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindowByCaption(IntPtr zeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, [Out] StringBuilder lParam);

        // Callback method used to collect a list of child windows we need to capture text from.
        private static bool EnumChildWindowsCallback(IntPtr handle, IntPtr pointer)
        {
            // Creates a managed GCHandle object from the pointer representing a handle to the list created in GetChildWindows.
            var gcHandle = GCHandle.FromIntPtr(pointer);

            // Casts the handle back back to a List<IntPtr>
            var list = gcHandle.Target as List<IntPtr>;

            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }

            // Adds the handle to the list.
            list.Add(handle);

            return true;
        }

        IntPtr m_hhook;
        private WinEventDelegate _winEventProc;

        public ActiveWindow()
        {
            _winEventProc = new WinEventDelegate(WinEventProc);
            m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND,
                EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _winEventProc,
                0, 0, WINEVENT_OUTOFCONTEXT);
        }

        public void TryActiveWindow()
        {
            IntPtr hwnd = GetForegroundWindow();
            RaiseActiveWindowEvent(hwnd);
        }

        private void RaiseActiveWindowEvent(IntPtr hwnd)
        {
            try
            {
                uint processId;
                GetWindowThreadProcessId(hwnd, out processId);

                Process p = Process.GetProcessById((int)processId);
                string description = string.Empty;
                var processName = GetProcessName(p);
                if (processName.ToLower().StartsWith("chrome"))
                {
                    // TryGetUrl(p);
                    description = GetChromeUrl(hwnd);
                    // WithSendkeys();
                }

                if (string.IsNullOrEmpty(description))
                {
                    description = GetActiveWindowTitle(hwnd);
                }

                ActiveWindowChanged?.Invoke(this, description, processName, hwnd);
            }
            catch
            {
                // nezajem
            }
        }

        // Returns an IEnumerable<IntPtr> containing the handles of all child windows of the parent window.
        private static IEnumerable<IntPtr> GetChildWindows(IntPtr parent)
        {
            // Create list to store child window handles.
            var result = new List<IntPtr>();

            // Allocate list handle to pass to EnumChildWindows.
            var listHandle = GCHandle.Alloc(result);

            try
            {
                // Enumerates though all the child windows of the parent represented by IntPtr parent, executing EnumChildWindowsCallback for each. 
                EnumChildWindows(parent, EnumChildWindowsCallback, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                // Free the list handle.
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            // Return the list of child window handles.
            return result;
        }

        // Gets text text from a control by it's handle.
        private static string GetText(IntPtr handle)
        {
            const uint WM_GETTEXTLENGTH = 0x000E;
            const uint WM_GETTEXT = 0x000D;

            // Gets the text length.
            var length = (int)SendMessage(handle, WM_GETTEXTLENGTH, IntPtr.Zero, null);

            // Init the string builder to hold the text.
            var sb = new StringBuilder(length + 1);

            // Writes the text from the handle into the StringBuilder
            SendMessage(handle, WM_GETTEXT, (IntPtr)sb.Capacity, sb);

            // Return the text as a string.
            return sb.ToString();
        }

        private string GetChromeUrl(IntPtr winHandle)
        {
            //var sb = new StringBuilder();
            //// Loop though the child windows, and execute the EnumChildWindowsCallback method
            //var childWindows = GetChildWindows(winHandle);

            //// For each child handle, run GetText
            //foreach (var childWindowText in childWindows.Select(GetText))
            //{
            //    // Append the text to the string builder.
            //    sb.Append(childWindowText);
            //}

            //// Return the windows full text.
            //return sb.ToString();

            string browserUrl = null;
            IntPtr urlHandle = FindWindowEx(winHandle, IntPtr.Zero, null, null);
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            int length = SendMessage(urlHandle, WM_GETTEXTLENGTH, 0, 0);
            if (length > 0)
            {
                SendMessage(urlHandle, WM_GETTEXT, nChars, Buff);
                browserUrl = Buff.ToString();

                return browserUrl;
            }
            else
            {
                return browserUrl;
            }

        }

        private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd,
            int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (eventType == EVENT_SYSTEM_FOREGROUND)
            {
                RaiseActiveWindowEvent(hwnd);
            }
        }

        private string GetProcessName(Process p)
        {
            return p.MainModule.ModuleName;
        }

        private string TryGetUrl(Process chrome)
        {
            // find the automation element
            AutomationElement elm = AutomationElement.FromHandle(chrome.MainWindowHandle);

            // manually walk through the tree, searching using TreeScope.Descendants is too slow (even if it's more reliable)
            AutomationElement elmUrlBar = null;
            try
            {
                if (elm == null)
                    return null;
                Condition conditions = new AndCondition(
                    new PropertyCondition(AutomationElement.ProcessIdProperty, chrome.Id),
                    new PropertyCondition(AutomationElement.IsControlElementProperty, true),
                    new PropertyCondition(AutomationElement.IsContentElementProperty, true),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

                AutomationElement elementx = elm.FindFirst(TreeScope.Descendants, conditions);
                return ((ValuePattern)elementx.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;

                //return null;

                // find the automation element
                //elmUrlBar = elm.FindFirst(TreeScope.Descendants,
                //  new PropertyCondition(AutomationElement.NameProperty, "Adresní a vyhledávací řádek"));

                //// if it can be found, get the value from the URL bar
                //if (elmUrlBar != null)
                //{
                //    AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
                //    if (patterns.Length > 0)
                //    {
                //        ValuePattern val = (ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0]);
                //        Console.WriteLine("Chrome URL found: " + val.Current.Value);
                //        return val.Current.Value;
                //    }
                //}
            }
            catch { }
            return null;
        }

        public void WithSendkeys()
        {
            AutomationElement.RootElement
                .FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1"))
                .SetFocus();
            System.Windows.Forms.SendKeys.SendWait("^l");
            var elmUrlBar = AutomationElement.FocusedElement;
            var valuePattern = (ValuePattern)elmUrlBar.GetCurrentPattern(ValuePattern.Pattern);
            Console.WriteLine(valuePattern.Current.Value);
        }

        private string TryGetNewUrl(Process chrome)
        {
            // find the automation element
            AutomationElement elm = AutomationElement.FromHandle(chrome.MainWindowHandle);

            // manually walk through the tree, searching using TreeScope.Descendants is too slow (even if it's more reliable)
            AutomationElement elmUrlBar = null;
            try
            {
                // walking path found using inspect.exe (Windows SDK) for Chrome 31.0.1650.63 m (currently the latest stable)
                var elm1 = elm.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Google Chrome"));
                if (elm1 == null) { return null; } // not the right chrome.exe
                                                   // here, you can optionally check if Incognito is enabled:
                                                   //bool bIncognito = TreeWalker.RawViewWalker.GetFirstChild(TreeWalker.RawViewWalker.GetFirstChild(elm1)) != null;
                var elm2 = TreeWalker.RawViewWalker.GetLastChild(elm1); // I don't know a Condition for this for finding :(
                var elm3 = elm2.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, ""));
                //var elm4 = elm3.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ToolBar));
                //elmUrlBar = elm4.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));

                // find the automation element
                elm = AutomationElement.FromHandle(chrome.MainWindowHandle);
                elmUrlBar = elm.FindFirst(TreeScope.Descendants,
                  new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
            }
            catch
            {
                // Chrome has probably changed something, and above walking needs to be modified. :(
                // put an assertion here or something to make sure you don't miss it
                return null;
            }

            // make sure it's valid
            if (elmUrlBar == null)
            {
                // it's not..
                return null;
            }

            // elmUrlBar is now the URL bar element. we have to make sure that it's out of keyboard focus if we want to get a valid URL
            if ((bool)elmUrlBar.GetCurrentPropertyValue(AutomationElement.HasKeyboardFocusProperty))
            {
                return null;
            }

            // there might not be a valid pattern to use, so we have to make sure we have one
            AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
            if (patterns.Length == 1)
            {
                string ret = "";
                try
                {
                    ret = ((ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0])).Current.Value;
                }
                catch { }
                if (ret != "")
                {
                    // must match a domain name (and possibly "https://" in front)
                    if (Regex.IsMatch(ret, @"^(https:\/\/)?[a-zA-Z0-9\-\.]+(\.[a-zA-Z]{2,4}).*$"))
                    {
                        // prepend http:// to the url, because Chrome hides it if it's not SSL
                        if (!ret.StartsWith("http"))
                        {
                            ret = "http://" + ret;
                        }
                        Console.WriteLine("Open Chrome URL found: '" + ret + "'");
                    }
                }
                return ret;
            }
            return null;
        }

        private string GetActiveWindowTitle(IntPtr hwnd)
        {
            StringBuilder Buff = new StringBuilder(500);
            GetWindowText(hwnd, Buff, Buff.Capacity);
            return Buff.ToString();
        }

        public void Dispose()
        {
            UnhookWinEvent(m_hhook);
        }

        ~ActiveWindow()
        {
            UnhookWinEvent(m_hhook);
        }
    }
}
