// <copyright file="ScheduleModel.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// Class Schedule.
    /// </summary>
    public class ScheduleModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleModel"/> class.
        /// </summary>
        public ScheduleModel()
        {
            this.ListScheduleDetail = new List<ScheduleDetailModel>();
        }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets ListScheduleDetail.
        /// </summary>
        public List<ScheduleDetailModel> ListScheduleDetail { get; set; }

        /// <summary>
        /// To String.
        /// </summary>
        /// <returns> String value.</returns>
        public override string ToString() => this.Name;
    }
}
