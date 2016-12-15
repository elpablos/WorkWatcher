using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lorenzo.WorkWatcher.Common
{
    public class WaitCursor : IDisposable
    {
        private Cursor previous;

        public WaitCursor()
        {
            previous = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            Cursor.Current = previous;
        }
    }
}
