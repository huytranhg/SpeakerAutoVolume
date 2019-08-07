// <copyright file="ShortcutStartsWithWindows.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence
{
    using System;
    using System.IO;
    using IWshRuntimeLibrary;

    /// <summary>
    /// Create Shortcut Starts With Windows.
    /// </summary>
    public static class ShortcutStartsWithWindows
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Create startup shortcut.
        /// </summary>
        /// <param name="shortcutName"> Shortcut name.</param>
        /// <param name="shortcutPath"> Shortcut apth.</param>
        /// <param name="targetFileLocation"> Target File Location.</param>
        /// <returns>Boolean value if Shortcut successfully created, or not.</returns>
        public static bool CreateStartupShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            try
            {
                string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
                string workingDirectory = Path.GetDirectoryName(targetFileLocation);
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
                shortcut.TargetPath = targetFileLocation;
                shortcut.WorkingDirectory = workingDirectory;
                shortcut.Save();
                return true;
            }
            catch (Exception ex)
            {
                NLogger.Error($"Could not create startup shortcut for {shortcutName} : {ex.Message}.");
                return false;
            }
        }

        /// <summary>
        /// Delete startup shortcut.
        /// </summary>
        /// <param name="shortcutName"> Shortcut name.</param>
        /// <param name="shortcutPath"> Shortcut apth.</param>
        /// <returns>Boolean value if Shortcut successfully deleted, or not.</returns>
        public static bool DeleteStartupShortcut(string shortcutName, string shortcutPath)
        {
            try
            {
                string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
                if (System.IO.File.Exists(shortcutLocation))
                {
                    System.IO.File.Delete(shortcutLocation);
                }

                return true;
            }
            catch (Exception ex)
            {
                NLogger.Error($"Could not delete startup shortcut for {shortcutName} : {ex.Message}.");
                return false;
            }
        }
    }
}
