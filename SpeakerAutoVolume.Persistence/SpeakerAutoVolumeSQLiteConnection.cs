// <copyright file="SpeakerAutoVolumeSQLiteConnection.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence
{
    using System.Configuration;

    /// <summary>
    /// Implementation of ISQLiteConnection.
    /// </summary>
    public static class SpeakerAutoVolumeSQLiteConnection
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Start SpeakerAutoVolume timer.
        /// </summary>
        /// <param name="id"> ID of connection string.</param>
        /// <returns> String value.</returns>
        public static string LoadConnectionString(string id = "Default")
        {
            NLogger.Info($"Loading onnection string: {ConfigurationManager.ConnectionStrings[id].ConnectionString}.");
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
