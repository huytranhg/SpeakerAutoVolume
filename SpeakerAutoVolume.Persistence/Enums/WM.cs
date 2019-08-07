// <copyright file="WM.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence.Enums
{
    /// <summary>
    /// WM enum.
    /// </summary>
    internal enum WM
    {
        /// <summary>
        /// NULL.
        /// </summary>
        NULL = 0x0000,

        /// <summary>
        /// CREATE.
        /// </summary>
        CREATE = 0x0001,

        /// <summary>
        /// DESTROY.
        /// </summary>
        DESTROY = 0x0002,

        /// <summary>
        /// MOVE.
        /// </summary>
        MOVE = 0x0003,

        /// <summary>
        /// SIZE.
        /// </summary>
        SIZE = 0x0005,

        /// <summary>
        /// ACTIVATE.
        /// </summary>
        ACTIVATE = 0x0006,

        /// <summary>
        /// SETFOCUS.
        /// </summary>
        SETFOCUS = 0x0007,

        /// <summary>
        /// KILLFOCUS.
        /// </summary>
        KILLFOCUS = 0x0008,

        /// <summary>
        /// ENABLE.
        /// </summary>
        ENABLE = 0x000A,

        /// <summary>
        /// SETREDRAW.
        /// </summary>
        SETREDRAW = 0x000B,

        /// <summary>
        /// SETTEXT.
        /// </summary>
        SETTEXT = 0x000C,

        /// <summary>
        /// GETTEXT.
        /// </summary>
        GETTEXT = 0x000D,

        /// <summary>
        /// GETTEXTLENGTH.
        /// </summary>
        GETTEXTLENGTH = 0x000E,

        /// <summary>
        /// PAINT.
        /// </summary>
        PAINT = 0x000F,

        /// <summary>
        /// CLOSE.
        /// </summary>
        CLOSE = 0x0010,

        /// <summary>
        /// QUERYENDSESSION.
        /// </summary>
        QUERYENDSESSION = 0x0011,

        /// <summary>
        /// QUIT.
        /// </summary>
        QUIT = 0x0012,

        /// <summary>
        /// QUERYOPEN.
        /// </summary>
        QUERYOPEN = 0x0013,

        /// <summary>
        /// ERASEBKGND.
        /// </summary>
        ERASEBKGND = 0x0014,

        /// <summary>
        /// SYSCOLORCHANGE.
        /// </summary>
        SYSCOLORCHANGE = 0x0015,

        /// <summary>
        /// SHOWWINDOW.
        /// </summary>
        SHOWWINDOW = 0x0018,

        /// <summary>
        /// ACTIVATEAPP.
        /// </summary>
        ACTIVATEAPP = 0x001C,

        /// <summary>
        /// SETCURSOR.
        /// </summary>
        SETCURSOR = 0x0020,

        /// <summary>
        /// MOUSEACTIVATE.
        /// </summary>
        MOUSEACTIVATE = 0x0021,

        /// <summary>
        /// CHILDACTIVATE.
        /// </summary>
        CHILDACTIVATE = 0x0022,

        /// <summary>
        /// QUEUESYNC.
        /// </summary>
        QUEUESYNC = 0x0023,

        /// <summary>
        /// GETMINMAXINFO.
        /// </summary>
        GETMINMAXINFO = 0x0024,

        /// <summary>
        /// WINDOWPOSCHANGING.
        /// </summary>
        WINDOWPOSCHANGING = 0x0046,

        /// <summary>
        /// WINDOWPOSCHANGED.
        /// </summary>
        WINDOWPOSCHANGED = 0x0047,

        /// <summary>
        /// CONTEXTMENU.
        /// </summary>
        CONTEXTMENU = 0x007B,

        /// <summary>
        /// STYLECHANGING.
        /// </summary>
        STYLECHANGING = 0x007C,

        /// <summary>
        /// STYLECHANGED.
        /// </summary>
        STYLECHANGED = 0x007D,

        /// <summary>
        /// DISPLAYCHANGE.
        /// </summary>
        DISPLAYCHANGE = 0x007E,

        /// <summary>
        /// GETICON.
        /// </summary>
        GETICON = 0x007F,

        /// <summary>
        /// SETICON.
        /// </summary>
        SETICON = 0x0080,

        /// <summary>
        /// NCCREATE.
        /// </summary>
        NCCREATE = 0x0081,

        /// <summary>
        /// NCDESTROY.
        /// </summary>
        NCDESTROY = 0x0082,

        /// <summary>
        /// NCCALCSIZE.
        /// </summary>
        NCCALCSIZE = 0x0083,

        /// <summary>
        /// NCHITTEST.
        /// </summary>
        NCHITTEST = 0x0084,

        /// <summary>
        /// NCPAINT.
        /// </summary>
        NCPAINT = 0x0085,

        /// <summary>
        /// NCACTIVATE.
        /// </summary>
        NCACTIVATE = 0x0086,

        /// <summary>
        /// GETDLGCODE.
        /// </summary>
        GETDLGCODE = 0x0087,

        /// <summary>
        /// SYNCPAINT.
        /// </summary>
        SYNCPAINT = 0x0088,

        /// <summary>
        /// NCMOUSEMOVE.
        /// </summary>
        NCMOUSEMOVE = 0x00A0,

        /// <summary>
        /// NCLBUTTONDOWN.
        /// </summary>
        NCLBUTTONDOWN = 0x00A1,

        /// <summary>
        /// NCLBUTTONUP.
        /// </summary>
        NCLBUTTONUP = 0x00A2,

        /// <summary>
        /// NCLBUTTONDBLCLK.
        /// </summary>
        NCLBUTTONDBLCLK = 0x00A3,

        /// <summary>
        /// NCRBUTTONDOWN.
        /// </summary>
        NCRBUTTONDOWN = 0x00A4,

        /// <summary>
        /// NCRBUTTONUP.
        /// </summary>
        NCRBUTTONUP = 0x00A5,

        /// <summary>
        /// NCRBUTTONDBLCLK.
        /// </summary>
        NCRBUTTONDBLCLK = 0x00A6,

        /// <summary>
        /// NCMBUTTONDOWN.
        /// </summary>
        NCMBUTTONDOWN = 0x00A7,

        /// <summary>
        /// NCMBUTTONUP.
        /// </summary>
        NCMBUTTONUP = 0x00A8,

        /// <summary>
        /// NCMBUTTONDBLCLK.
        /// </summary>
        NCMBUTTONDBLCLK = 0x00A9,

        /// <summary>
        /// SYSKEYDOWN.
        /// </summary>
        SYSKEYDOWN = 0x0104,

        /// <summary>
        /// SYSKEYUP.
        /// </summary>
        SYSKEYUP = 0x0105,

        /// <summary>
        /// SYSCHAR.
        /// </summary>
        SYSCHAR = 0x0106,

        /// <summary>
        /// SYSDEADCHAR.
        /// </summary>
        SYSDEADCHAR = 0x0107,

        /// <summary>
        /// COMMAND.
        /// </summary>
        COMMAND = 0x0111,

        /// <summary>
        /// SYSCOMMAND.
        /// </summary>
        SYSCOMMAND = 0x0112,

        /// <summary>
        /// MOUSEMOVE.
        /// </summary>
        MOUSEMOVE = 0x0200,

        /// <summary>
        /// LBUTTONDOWN.
        /// </summary>
        LBUTTONDOWN = 0x0201,

        /// <summary>
        /// LBUTTONUP.
        /// </summary>
        LBUTTONUP = 0x0202,

        /// <summary>
        /// LBUTTONDBLCLK.
        /// </summary>
        LBUTTONDBLCLK = 0x0203,

        /// <summary>
        /// RBUTTONDOWN.
        /// </summary>
        RBUTTONDOWN = 0x0204,

        /// <summary>
        /// RBUTTONUP.
        /// </summary>
        RBUTTONUP = 0x0205,

        /// <summary>
        /// RBUTTONDBLCLK.
        /// </summary>
        RBUTTONDBLCLK = 0x0206,

        /// <summary>
        /// MBUTTONDOWN.
        /// </summary>
        MBUTTONDOWN = 0x0207,

        /// <summary>
        /// MBUTTONUP.
        /// </summary>
        MBUTTONUP = 0x0208,

        /// <summary>
        /// MBUTTONDBLCLK.
        /// </summary>
        MBUTTONDBLCLK = 0x0209,

        /// <summary>
        /// MOUSEWHEEL.
        /// </summary>
        MOUSEWHEEL = 0x020A,

        /// <summary>
        /// XBUTTONDOWN.
        /// </summary>
        XBUTTONDOWN = 0x020B,

        /// <summary>
        /// XBUTTONUP.
        /// </summary>
        XBUTTONUP = 0x020C,

        /// <summary>
        /// XBUTTONDBLCLK.
        /// </summary>
        XBUTTONDBLCLK = 0x020D,

        /// <summary>
        /// MOUSEHWHEEL.
        /// </summary>
        MOUSEHWHEEL = 0x020E,

        /// <summary>
        /// CAPTURECHANGED.
        /// </summary>
        CAPTURECHANGED = 0x0215,

        /// <summary>
        /// ENTERSIZEMOVE.
        /// </summary>
        ENTERSIZEMOVE = 0x0231,

        /// <summary>
        /// EXITSIZEMOVE.
        /// </summary>
        EXITSIZEMOVE = 0x0232,

        /// <summary>
        /// IME_SETCONTEXT.
        /// </summary>
        IME_SETCONTEXT = 0x0281,

        /// <summary>
        /// IME_NOTIFY.
        /// </summary>
        IME_NOTIFY = 0x0282,

        /// <summary>
        /// IME_CONTROL.
        /// </summary>
        IME_CONTROL = 0x0283,

        /// <summary>
        /// IME_COMPOSITIONFULL.
        /// </summary>
        IME_COMPOSITIONFULL = 0x0284,

        /// <summary>
        /// IME_SELECT.
        /// </summary>
        IME_SELECT = 0x0285,

        /// <summary>
        /// IME_CHAR.
        /// </summary>
        IME_CHAR = 0x0286,

        /// <summary>
        /// IME_REQUEST.
        /// </summary>
        IME_REQUEST = 0x0288,

        /// <summary>
        /// IME_KEYDOWN.
        /// </summary>
        IME_KEYDOWN = 0x0290,

        /// <summary>
        /// IME_KEYUP.
        /// </summary>
        IME_KEYUP = 0x0291,

        /// <summary>
        /// NCMOUSELEAVE.
        /// </summary>
        NCMOUSELEAVE = 0x02A2,

        /// <summary>
        /// DWMCOMPOSITIONCHANGED.
        /// </summary>
        DWMCOMPOSITIONCHANGED = 0x031E,

        /// <summary>
        /// DWMNCRENDERINGCHANGED.
        /// </summary>
        DWMNCRENDERINGCHANGED = 0x031F,

        /// <summary>
        /// DWMCOLORIZATIONCOLORCHANGED.
        /// </summary>
        DWMCOLORIZATIONCOLORCHANGED = 0x0320,

        /// <summary>
        /// DWMWINDOWMAXIMIZEDCHANGE.
        /// </summary>
        DWMWINDOWMAXIMIZEDCHANGE = 0x0321,

        // Windows 7 Enums

        /// <summary>
        /// DWMSENDICONICTHUMBNAIL.
        /// </summary>
        DWMSENDICONICTHUMBNAIL = 0x0323, // Windows 7 Enums

        /// <summary>
        /// DWMSENDICONICLIVEPREVIEWBITMAP.
        /// </summary>
        DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326, // Windows 7 Enums

        /// <summary>
        /// USER.
        /// </summary>
        USER = 0x0400,

        // This is the hard-coded message value used by WinForms for Shell_NotifyIcon.
        // It's relatively safe to reuse.

        /// <summary>
        /// TRAYMOUSEMESSAGE.
        /// </summary>
        TRAYMOUSEMESSAGE = 0x800, // WM_USER + 1024

        /// <summary>
        /// APP.
        /// </summary>
        APP = 0x8000,
    }
}
