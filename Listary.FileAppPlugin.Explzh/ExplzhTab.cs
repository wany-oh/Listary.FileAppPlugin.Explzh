using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listary.FileAppPlugin.Explzh
{
    public class ExplzhTab : IFileTab, IGetFolder, IOpenFolder
    {
        private IFileAppPluginHost _host;
        private IntPtr _handle;

        private IntPtr tab = IntPtr.Zero;
        private bool is_ExtractDialog = false;
        private IntPtr _pathEditor = IntPtr.Zero;
        private IntPtr _displayButton = IntPtr.Zero;

        public ExplzhTab(IFileAppPluginHost host, IntPtr handle)
        {
            _host = host;
            _handle = handle;

            tab = Win32Utils.GetDlgItem(_handle, 0x50C);
            if (Win32Utils.GetClassName(tab) == "SysTabControl32")
            {
                is_ExtractDialog = false;
                _pathEditor = Win32Utils.GetDlgItem(handle, 0x522);
            } 
            else
            {
                is_ExtractDialog = true;
                _displayButton = Win32Utils.GetDlgItem(handle, 0x4CE);
                if (_displayButton == IntPtr.Zero)
                {
                    _host.Logger.LogWarning("Failed to find Display button");
                }
                _pathEditor = Win32Utils.GetDlgItem(handle, 0x3E9);
            }

            if (_pathEditor == IntPtr.Zero)
            {
                _host.Logger.LogError("Failed to find the path editor");
            }
        }

        public async Task<string> GetCurrentFolder()
        {
            if (_pathEditor != IntPtr.Zero)
            {
                return _host.GetWindowText(_pathEditor);
            }
            return string.Empty;
        }

        public async Task<bool> OpenFolder(string path)
        {
            if (_pathEditor != IntPtr.Zero)
            {
                if (_host.SetWindowText(_pathEditor, path))
                {
                    if (is_ExtractDialog) 
                    {
                        _host.PostMessage(_displayButton, Win32Utils.BM_CLICK, IntPtr.Zero, IntPtr.Zero); 
                    }
                    
                    return true;
                }
                else
                {
                    _host.Logger.LogError("Failed to set the text of the path editor");
                }
            }
            return false;
        }
    }
}