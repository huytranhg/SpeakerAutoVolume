// <copyright file="HourModel.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain
{
    /// <summary>
    /// Model class Hour.
    /// </summary>
    public class HourModel
    {
        /// <summary>
        /// Gets or sets SpeakerAutoVolumeHour.
        /// </summary>
        public int SpeakerAutoVolumeHour { get; set; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return this.SpeakerAutoVolumeHour.ToString();
        }
    }
}