using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listary.FileAppPlugin.Explzh
{
    public class ExplzhWindow : IFileWindow
    {
        private IFileAppPluginHost _host;

        public IntPtr Handle { get; }

        public ExplzhWindow(IFileAppPluginHost host, IntPtr hWnd)
        {
            _host = host;
            Handle = hWnd;
        }

        public async Task<IFileTab> GetCurrentTab()
        {
            return new ExplzhTab(_host, Handle);
        }
    }
}