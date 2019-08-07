// <copyright file="DeleteScheduleEvent.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.Events
{
    using SpeakerAutoVolume.Domain;

    /// <summary>
    /// Delete Schedule Event.
    /// </summary>
    public class DeleteScheduleEvent
    {
        /// <summary>
        /// Gets or sets a value indicating whether delete is confirmed.
        /// </summary>
        public bool Confirmed { get; set; }

        /// <summary>
        /// Gets or sets schedule model.
        /// </summary>
        public ScheduleModel ScheduleModel { get; set; }
    }
}