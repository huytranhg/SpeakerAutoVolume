// <copyright file="NativeMethods.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Security;
    using SpeakerAutoVolume.Persistence.Enums;

    /// <summary>
    /// Native Methods.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        /// <summary>
        /// Message Handler.
        /// </summary>
        /// <param name="uMsg"> Message.</param>
        /// <param name="wParam"> Integer Pointer of wparam.</param>
        /// <param name="lParam"> Integer Pointer of lparam.</param>
        /// <param name="handled"> Output boolean of handled.</param>
        /// <returns> Integer pointer.</returns>
        public delegate IntPtr MessageHandler(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled);

        /// <summary>
        /// Command Line To ArgvW.
        /// </summary>
        /// <param name="cmdLine"> Command line.</param>
        /// <returns> String value.</returns>
        public static string[] CommandLineToArgvW(string cmdLine)
        {
            IntPtr argv = IntPtr.Zero;
            try
            {
                argv = _CommandLineToArgvW(cmdLine, out int numArgs);
                if (argv == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                var result = new string[numArgs];

                for (int i = 0; i < numArgs; i++)
                {
                    IntPtr currArg = Marshal.ReadIntPtr(argv, i * Marshal.SizeOf(typeof(IntPtr)));
                    result[i] = Marshal.PtrToStringUni(currArg);
                }

                return result;
            }
            finally
            {
                IntPtr p = _LocalFree(argv);

                // Otherwise LocalFree failed.
                // Assert.AreEqual(IntPtr.Zero, p);
            }
        }

        [DllImport("shell32.dll", EntryPoint = "CommandLineToArgvW", CharSet = CharSet.Unicode)]
        private static extern IntPtr _CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmdLine, out int numArgs);

        [DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
        private static extern IntPtr _LocalFree(IntPtr hMem);
    }
}
