using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listary.FileAppPlugin.Explzh
{
    public class ExplzhPlugin : IFileAppPlugin
    {
        private IFileAppPluginHost _host;

        public bool IsOpenedFolderProvider => false;

        public bool IsQuickSwitchTarget => true;

        public bool IsSharedAcrossApplications => false;

        public SearchBarType SearchBarType => SearchBarType.Fixed;

        public async Task<bool> Initialize(IFileAppPluginHost host)
        {
            _host = host;
            return true;
        }

        public IFileWindow BindFileWindow(IntPtr hWnd)
        {
            // It is a Win32 dialog box?
            if (Win32Utils.GetClassName(hWnd) == "#32770")
            {
                // It is from Explzh?
                if (Win32Utils.GetProcessPathFromHwnd(hWnd).EndsWith("\\Explzh.exe", StringComparison.OrdinalIgnoreCase))
                {
                    return new ExplzhWindow(_host, hWnd);
                }
            }
            return null;
        }
    }
}
