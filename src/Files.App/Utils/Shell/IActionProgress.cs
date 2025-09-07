// Copyright (c) Files Community
// Licensed under the MIT License.

using System.Runtime.InteropServices;
using Vanara.PInvoke;

namespace Vanara.PInvoke
{
    [ComImport, Guid("359F18E4-1974-4894-82AE-966779549C4A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IActionProgress
    {
        [PreserveSig]
        HRESULT Begin(ACTION_TYPE action, in CONNECT_INFO pConnectInfo);

        [PreserveSig]
        HRESULT UpdateProgress(ulong ulBytesCompleted, ulong ulBytesTotal);

        [PreserveSig]
        HRESULT UpdateText(SPACTIONTEXT sptext, [MarshalAs(UnmanagedType.LPWStr)] string pszText, [MarshalAs(UnmanagedType.Bool)] bool fMayCompact);

        [PreserveSig]
        HRESULT QueryCancel([MarshalAs(UnmanagedType.Bool)] out bool pfCancelled);

        [PreserveSig]
        HRESULT End();
    }

    public enum SPACTIONTEXT
    {
        SPACTIONTEXT_NONE = 0,
        SPACTIONTEXT_ACTION = 1,
        SPACTIONTEXT_FROM = 2,
        SPACTIONTEXT_TO = 3,
        SPACTIONTEXT_HEADER = 4,
        SPACTIONTEXT_BODY = 5,
        SPACTIONTEXT_FOOTER = 6
    }

    public enum ACTION_TYPE
    {
        ACTION_TYPE_CREATE = 0,
        ACTION_TYPE_COPY = 1,
        ACTION_TYPE_MOVE = 2,
        ACTION_TYPE_RECYCLE = 3,
        ACTION_TYPE_APPLYPROPS = 4,
        ACTION_TYPE_DOWNLOAD = 5,
        ACTION_TYPE_SEARCH = 6,
        ACTION_TYPE_COMPRESS = 7,
        ACTION_TYPE_DECOMPRESS = 8
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CONNECT_INFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszSource;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszDest;
    }
}
