// <copyright file="IMMDevice.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence.Interfaces
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Interface for MMDevice.
    /// </summary>
    [Guid("D666063F-1587-4E43-81F1-B948E807363F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDevice
    {
        /// <summary>
        /// Activate.
        /// </summary>
        /// <param name="iid"> guid.</param>
        /// <param name="dwClsCtx"> dwClsCtx.</param>
        /// <param name="pActivationParams"> Activation Params.</param>
        /// <param name="ppInterface"> Interface.</param>
        /// <returns> Integer value.</returns>
        int Activate(
            [MarshalAs(UnmanagedType.LPStruct)] Guid iid,
            int dwClsCtx,
            IntPtr pActivationParams,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
    }
}