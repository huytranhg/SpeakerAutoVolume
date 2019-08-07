// <copyright file="MMDeviceEnumeratorFactory.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence
{
    using System;
    using SpeakerAutoVolume.Persistence.Interfaces;

    /// <summary>
    /// Class MMDeviceEnumeratorFactory.
    /// </summary>
    public static class MMDeviceEnumeratorFactory
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Set Speaker Volume.
        /// </summary>
        /// <returns> Integer value.</returns>
        /// <param name="volumeLevel"> Volume level to be changed to (0 - 100).</param>
        public static int SetSpeakerVolume(byte volumeLevel)
        {
            if (PrecheckVolumeRange(volumeLevel))
            {
                try
                {
                    IMMDeviceEnumerator deviceEnumerator = MMDeviceEnumeratorFactory.CreateInstance();
                    const int eRender = 0;
                    const int eMultimedia = 1;
                    deviceEnumerator.GetDefaultAudioEndpoint(eRender, eMultimedia, out IMMDevice speakers);
                    speakers.Activate(typeof(IAudioEndpointVolume).GUID, 0, IntPtr.Zero, out object aepv_obj);
                    IAudioEndpointVolume aepv = (IAudioEndpointVolume)aepv_obj;
                    Guid zeroGuid = default(Guid);
                    int res = aepv.SetMasterVolumeLevelScalar(volumeLevel / 100f, zeroGuid);

                    return res;
                }
                catch (Exception ex)
                {
                    NLogger.Error($"Could not set audio level: {ex.Message}.");
                    return Constants.ErrorCode;
                }
            }
            else
            {
                return Constants.ErrorCode;
            }
        }

        /// <summary>
        /// Get Speaker Volume.
        /// </summary>
        /// <returns> Integer value.</returns>
        /// <param name="byteLevelDB"> Volume level to be returned to (0 - 100).</param>
        public static int GetSpeakerVolume(ref byte byteLevelDB)
        {
            try
            {
                IMMDeviceEnumerator deviceEnumerator = MMDeviceEnumeratorFactory.CreateInstance();
                const int eRender = 0;
                const int eMultimedia = 1;
                deviceEnumerator.GetDefaultAudioEndpoint(eRender, eMultimedia, out IMMDevice speakers);
                speakers.Activate(typeof(IAudioEndpointVolume).GUID, 0, IntPtr.Zero, out object aepv_obj);
                IAudioEndpointVolume aepv = (IAudioEndpointVolume)aepv_obj;
                float pfLevelDB = 0;
                int res = aepv.GetMasterVolumeLevelScalar(ref pfLevelDB);

                byteLevelDB = (byte)Math.Round(pfLevelDB * 100);

                return res;
            }
            catch (Exception ex)
            {
                NLogger.Error($"Could not set audio level: {ex.Message}.");
                return Constants.ErrorCode;
            }
        }

        private static IMMDeviceEnumerator CreateInstance()
        {
            return (IMMDeviceEnumerator)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("BCDE0395-E52F-467C-8E3D-C4579291692E"))); // a MMDeviceEnumerator
        }

        private static bool PrecheckVolumeRange(byte volumeLevel)
        {
            if (volumeLevel > 100)
            {
                NLogger.Error("The speaker's volume to be changed to is higher than 100.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}