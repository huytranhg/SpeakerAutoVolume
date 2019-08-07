// <copyright file="IAudioEndpointVolume.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence.Interfaces
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Interface for AudioEndpointVolume.
    /// </summary>
    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioEndpointVolume
    {
        // virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE RegisterControlChangeNotify(/* [in] */__in IAudioEndpointVolumeCallback *pNotify) = 0;

        /// <summary>
        /// Register Control Change Notify.
        /// </summary>
        /// <param name="pNotify"> Notify Pointer.</param>
        /// <returns> Integer value.</returns>
        int RegisterControlChangeNotify(IntPtr pNotify);

        // virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE UnregisterControlChangeNotify(/* [in] */ __in IAudioEndpointVolumeCallback *pNotify) = 0;

        /// <summary>
        /// Unregister Control Change Notify.
        /// </summary>
        /// <param name="pNotify"> Notify Pointer.</param>
        /// <returns> Integer value.</returns>
        int UnregisterControlChangeNotify(IntPtr pNotify);

        // virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE GetChannelCount(/* [out] */ __out UINT *pnChannelCount) = 0;

        /// <summary>
        /// Get Channel Count.
        /// </summary>
        /// <param name="pnChannelCount"> Unsigned integer of Channel Count.</param>
        /// <returns> Integer value.</returns>
        int GetChannelCount(ref uint pnChannelCount);

        // virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE SetMasterVolumeLevel( /* [in] */ __in float fLevelDB,/* [unique][in] */ LPCGUID pguidEventContext) = 0;

        /// <summary>
        /// Get Channel Count.
        /// </summary>
        /// <param name="fLevelDB"> Level DB.</param>
        /// <param name="pguidEventContext"> Guid Event Context.</param>
        /// <returns> Integer value.</returns>
        int SetMasterVolumeLevel(float fLevelDB, Guid pguidEventContext);

        // virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE SetMasterVolumeLevelScalar( /* [in] */ __in float fLevel,/* [unique][in] */ LPCGUID pguidEventContext) = 0;

        /// <summary>
        /// Get Channel Count.
        /// </summary>
        /// <param name="fLevel"> Level.</param>
        /// <param name="pguidEventContext"> Guid Event Context.</param>
        /// <returns> Integer value.</returns>
        int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);

        // virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE GetMasterVolumeLevel(/* [out] */ __out float *pfLevelDB) = 0;

        /// <summary>
        /// Get Channel Count.
        /// </summary>
        /// <param name="pfLevelDB"> Level DB.</param>
        /// <returns> Integer value.</returns>
        int GetMasterVolumeLevel(ref float pfLevelDB);

        // virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE GetMasterVolumeLevelScalar( /* [out] */ __out float *pfLevel) = 0;

        /// <summary>
        /// Get Channel Count.
        /// </summary>
        /// <param name="pfLevel"> Level.</param>
        /// <returns> Integer value.</returns>
        int GetMasterVolumeLevelScalar(ref float pfLevel);
    }
}
