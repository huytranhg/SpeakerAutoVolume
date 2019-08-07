// <copyright file="Constants.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence
{
    /// <summary>
    /// Constants used in Persistence layer.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Default error code for SpeakerAutoVolume.
        /// </summary>
        public const int ErrorCode = -1;

        /// <summary>
        /// HWNDBROADCAST for WinApi class library.
        /// </summary>
        public const int HWNDBROADCAST = 0xffff;

        /// <summary>
        /// SWSHOWNORMAL for WinApi class library.
        /// </summary>
        public const int SWSHOWNORMAL = 1;

        /// <summary>
        /// TimerInterval for Timer class, 1000 miliseconds, or 1 second.
        /// </summary>
        public const int TimerInterval = 1000;
    }
}
