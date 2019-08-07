// <copyright file="ISingleInstanceApp.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for MMDeviceEnumerator.
    /// </summary>
    public interface ISingleInstanceApp
    {
        /// <summary>
        /// Signal External Command Line Arguments.
        /// </summary>
        /// <param name="args"> Arguments.</param>
        /// <returns> Boolean value.</returns>
        bool SignalExternalCommandLineArgs(IList<string> args);
    }
}
