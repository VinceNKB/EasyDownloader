using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EasyDownloader
{
    /// <summary>
    /// Provides notifications when the contents of the clipboard is updated.
    /// </summary>
    public sealed class ClipboardNotification
    {
        /// <summary>
        /// Occurs when the contents of the clipboard is updated.
        /// </summary>
        public event Action ClipboardUpdate;

        public ClipboardNotification(Action action)
        {
            this.ClipboardUpdate = action;
            Task.Run(() => Application.Run(new NotificationForm(OnClipboardUpdate)));
        }

        /// <summary>
        /// Raises the <see cref="ClipboardUpdate"/> event.
        /// </summary>
        /// <param name="e">Event arguments for the event.</param>
        private void OnClipboardUpdate()
        {
            var handler = ClipboardUpdate;
            if (handler != null)
                handler();
        }

        /// <summary>
        /// Hidden form to recieve the WM_CLIPBOARDUPDATE message.
        /// </summary>
        private class NotificationForm : Form
        {
            private MethodInvoker onClipboardUpdate;
            private IntPtr hWnd;
            private int lastTickCount;

            public NotificationForm(MethodInvoker onClipboardUpdate)
            {
                this.onClipboardUpdate = onClipboardUpdate;
                this.hWnd = this.Handle;
                //Turn the child window into a message-only window (refer to Microsoft docs)
                NativeMethods.SetParent(this.hWnd, NativeMethods.HWND_MESSAGE);
                //Place window in the system-maintained clipboard format listener list
                NativeMethods.AddClipboardFormatListener(this.hWnd);
                Diagnostics.WriteDebugTrace($"Regist listener");
            }

            ~NotificationForm()
            {
                NativeMethods.RemoveClipboardFormatListener(this.hWnd);
            }

            protected override void WndProc(ref Message m)
            {
                //Diagnostics.WriteDebugTrace(m.ToString());
                if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE && this.onClipboardUpdate != null)
                {
                    if (Environment.TickCount - this.lastTickCount >= 200)
                    {
                        this.onClipboardUpdate();
                    }
                        
                    this.lastTickCount = Environment.TickCount;
                    m.Result = IntPtr.Zero;
                }
                base.WndProc(ref m);
            }
        }

        internal static class NativeMethods
        {
            //Reference https://docs.microsoft.com/en-us/windows/desktop/dataxchg/wm-clipboardupdate
            public const int WM_CLIPBOARDUPDATE = 0x031D;
            //Reference https://www.pinvoke.net/default.aspx/Constants.HWND
            public static IntPtr HWND_MESSAGE = new IntPtr(-3);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool AddClipboardFormatListener(IntPtr hWnd);
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool RemoveClipboardFormatListener(IntPtr hWnd);

            //Reference https://www.pinvoke.net/default.aspx/user32.setparent
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        }
    }
}