// <copyright file="ScheduleDetailDto.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain.DTOs
{
    /// <summary>
    /// Data Transfer Object class ScheduleDetailDto.
    /// </summary>
    public class ScheduleDetailDto
    {
        /// <summary>
        /// Gets or sets Name.
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
    }
}
