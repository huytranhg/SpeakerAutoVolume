// <copyright file="MinuteModel.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain
{
    /// <summary>
    /// Model class Minute.
    /// </summary>
    public class MinuteModel
    {
        /// <summary>
        /// Gets or sets SpeakerAutoVolumeHour.
        /// </summary>
        public int SpeakerAutoVolumeMinute { get; set; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return this.SpeakerAutoVolumeMinute.ToString();
        }
    }
}
