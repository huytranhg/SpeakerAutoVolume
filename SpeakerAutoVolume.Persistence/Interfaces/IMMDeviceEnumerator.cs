// <copyright file="IMMDeviceEnumerator.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence.Interfaces
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Interface for MMDeviceEnumerator.
    /// </summary>
    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceEnumerator
    {
        /// <summary>
        /// VtblGap1_1.
        /// </summary>
        void VtblGap1_1();

        /// <summary>
        /// Get default audio endpoint.
        /// </summary>
        /// <param name="dataFlow"> data Flow.</param>
        /// <param name="role"> role.</param>
        /// <param name="ppDevice"> pp device.</param>
        /// <returns> Integer value.</returns>
        int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice ppDevice);
    }
}