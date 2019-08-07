// <copyright file="ISpeakerAutoVolumeDatabaseAccess.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain.Interfaces
{
    using System.Collections.Generic;
    using SpeakerAutoVolume.Domain.DTOs;

    /// <summary>
    /// Interface to be implemented in persistence layer.
    /// </summary>
    public interface ISpeakerAutoVolumeDatabaseAccess
    {
        /// <summary>
        /// Select all schedules.
        /// </summary>
        /// <returns>List of schedule.</returns>
        List<ScheduleDto> SelectAllSchedules();

        /// <summary>
        /// Select all schedule details.
        /// </summary>
        /// <returns>List of schedule detail.</returns>
        List<ScheduleDetailDto> SelectAllScheduleDetails();

        /// <summary>
        /// Save schedule.
        /// </summary>
        /// <param name="schedule"> Schedule to be saved.</param>
        /// <returns>Affected rows.</returns>
        int InsertSchedule(ScheduleDto schedule);

        /// <summary>
        /// Save schedule detail.
        /// </summary>
        /// <param name="scheduleDetail"> Schedule detail to be saved.</param>
        /// <returns>Affected rows.</returns>
        int InsertScheduleDetail(ScheduleDetailDto scheduleDetail);

        /// <summary>
        /// Select if a schedule detail exists.
        /// </summary>
        /// <param name="scheduleDetail"> Schedule detail to be saved.</param>
        /// <returns>List of integer.</returns>
        List<int> SelectScheduleDetailExist(ScheduleDetailDto scheduleDetail);

        /// <summary>
        /// Delete Schedule.
        /// </summary>
        /// <param name="schedule"> Schedule to be deleted.</param>
        /// <returns>Affected rows.</returns>
        int DeleteSchedule(ScheduleDto schedule);

        /// <summary>
        /// Delete a schedule detail.
        /// </summary>
        /// <param name="scheduleDetail"> Schedule detail to be deleted.</param>
        /// <returns>List of affected row.</returns>
        int DeleteScheduleDetail(ScheduleDetailDto scheduleDetail);

        /// <summary>
        /// Delete all schedule details based on schedule name.
        /// </summary>
        /// <param name="schedule"> Schedule details to be deleted.</param>
        /// <returns>List of affected row.</returns>
        int DeleteScheduleDetails(ScheduleDto schedule);
    }
}
