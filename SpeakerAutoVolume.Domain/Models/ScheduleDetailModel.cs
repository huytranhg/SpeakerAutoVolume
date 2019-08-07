// <copyright file="ScheduleDetailModel.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain
{
    /// <summary>
    /// Class ScheduleDetail.
    /// </summary>
    public class ScheduleDetailModel
    {
        /// <summary>
        /// Gets or sets ScheduleName.
        /// </summary>
        public string ScheduleName { get; set; }

        /// <summary>
        /// Gets or sets StartHour.
        /// </summary>
        public byte StartHour { get; set; }

        /// <summary>
        /// Gets or sets StartMinute.
        /// </summary>
        public byte StartMinute { get; set; }

        /// <summary>
        /// Gets or sets EndHour.
        /// </summary>
        public byte EndHour { get; set; }

        /// <summary>
        /// Gets or sets EndMinute.
        /// </summary>
        public byte EndMinute { get; set; }

        /// <summary>
        /// Gets or sets Volume.
        /// </summary>
        public byte Volume { get; set; }

        /// <summary>
        /// To String.
        /// </summary>
        /// <returns> String value.</returns>
        public override string ToString() => string.Concat(
            this.StartHour.ToString("D2"),
            ":",
            this.StartMinute.ToString("D2"),
            "-",
            this.EndHour.ToString("D2"),
            ":",
            this.EndMinute.ToString("D2"),
            "   ",
            this.Volume.ToString());
    }
}
