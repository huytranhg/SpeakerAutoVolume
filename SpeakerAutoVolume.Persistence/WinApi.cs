// <copyright file="WinApi.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

/*
*   https://www.codeproject.com/Articles/32908/C-Single-Instance-App-With-the-Ability-To-Restore
*   WinApi
*
*   This class is just a wrapper for your various WinApi functions.
*
*   In this sample only the bare essentials are included.
*   In my own WinApi class, I have all the WinApi functions that any
*   of my applications would ever need.
*
*/

namespace SpeakerAutoVolume.Persistence
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Class WinApi.
    /// </summary>
    public static class WinApi
    {
        /// <summary>
        /// Gets Assembly Guid.
        /// </summary>
        /// <returns> String value.</returns>
        /// <param name="message"> Message string.</param>
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        /// <summary>
        /// Gets Register Window Message.
        /// </summary>
        /// <returns> Integer value.</returns>
        /// <param name="format"> Format string.</param>
        /// <param name="args"> Array of parameters.</param>
        public static int RegisterWindowMessage(string format, params object[] args)
        {
            string message = string.Format(format, args);
            return RegisterWindowMessage(message);
        }

        /// <summary>
        /// Post Message.
        /// </summary>
        /// <returns> Boolean value.</returns>
        /// <param name="hWnd"> Integer Pointer.</param>
        /// <param name="msg"> Message integer.</param>
        /// <param name="wparam"> Integer Pointer of wparam.</param>
        /// <param name="lparam"> Integer Pointer of lparam.</param>
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam);

        /// <summary>
        /// Show Window.
        /// </summary>
        /// <returns> Boolean value.</returns>
        /// <param name="hWnd"> Integer Pointer.</param>
        /// <param name="nCmdShow"> Command Show integer.</param>
        [DllImportAttribute("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Set Foreground Window.
        /// </summary>
        /// <returns> Boolean value.</returns>
        /// <param name="hWnd"> Integer Pointer.</param>
        [DllImportAttribute("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Show To Front.
        /// </summary>
        /// <param name="window"> Integer Pointer.</param>
        public static void ShowToFront(IntPtr window)
        {
            ShowWindow(window, Constants.SWSHOWNORMAL);
            SetForegroundWindow(window);
        }
    }
}
