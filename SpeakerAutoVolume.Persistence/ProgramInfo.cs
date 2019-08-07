// <copyright file="ProgramInfo.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

/*
*   https://www.codeproject.com/Articles/32908/C-Single-Instance-App-With-the-Ability-To-Restore
*   ProgramInfo
*
*   This class is just for getting information about the application.
*   Each assembly has a GUID, and that GUID is useful to us in this application,
*   so the most important thing in this class is the AssemblyGuid property.
*
*   GetEntryAssembly() is used instead of GetExecutingAssembly(), so that you
*   can put this code into a class library and still get the results you expect.
*   (Otherwise it would return info on the DLL assembly instead of your application.)
*/

namespace SpeakerAutoVolume.Persistence
{
    using System.Reflection;

    /// <summary>
    /// Class ProgramInfo.
    /// </summary>
    public static class ProgramInfo
    {
        /// <summary>
        /// Gets Assembly Guid.
        /// </summary>
        /// <returns> String value.</returns>
        public static string AssemblyGuid
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }

                return ((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value;
            }
        }

        /// <summary>
        /// Gets Assembly Title.
        /// </summary>
        /// <returns> String value.</returns>
        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != string.Empty)
                    {
                        return titleAttribute.Title;
                    }
                }

                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }
    }
}
