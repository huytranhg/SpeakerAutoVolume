// <copyright file="ISpeakerAutoVolumeTimer.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface to be implemented in persistence layer.
    /// </summary>
    public interface ISpeakerAutoVolumeTimer
    {
        /// <summary>
        /// Start SpeakerAutoVolume timer.
        /// </summary>
        /// <param name="listScheduleDetail"> List schedule detail.</param>
        void Start(List<ScheduleDetailModel> listScheduleDetail);

        /// <summary>
        /// Stop SpeakerAutoVolume timer.
        /// </summary>
        void Stop();
    }
}
